using BepInEx;
using BepInEx.Configuration;
using HarmonyLib;
using MTM101BaldAPI;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using UnityEngine;
using UnityEngine.Networking;

namespace NilLib
{
[BepInPlugin("Nil.Library","NilLibrary","1.4.0.0")]
    public class NilLibPlugin : BaseUnityPlugin
    {
        public static NilLibPlugin Instance;
        public string LatestVersion = "none";
        public string LatestUpdateLog = "";
        public ConfigEntry<bool> AutoUpdateMod;
        public bool ModNeedsToUpdate = false;

        internal static List<GameObject> tempObjects = new();

        IEnumerator GetLatestModVersion()
        {
            UnityWebRequest Uwr = UnityWebRequest.Get("api.gamebanana.com/Core/Item/Data?itemtype=Mod&itemid=713906&fields=Updates().aLatestUpdates()");
            yield return Uwr.SendWebRequest();
            if (Uwr.result != UnityWebRequest.Result.Success) {
                UnityEngine.Debug.Log("Cant get latest version");
                yield break;
            }
            string json = Uwr.downloadHandler.text;
            JToken gbResult = JToken.Parse(json);

            LatestVersion = gbResult[0][0]["_sVersion"].Value<string>();
            LatestUpdateLog = $"Update log for NilLib ({gbResult[0][0]["_sVersion"]})\n{gbResult[0][0]["_sTitle"]}";
            foreach (var item in gbResult[0][0]["_aChangeLog"])
            {
                LatestUpdateLog += $"\n{item["text"].Value<string>()}";
            }
            if (new Version(LatestVersion) > Info.Metadata.Version)
            {
                MTM101BaldiDevAPI.AddWarningScreen($"You arent on the latest version ({LatestVersion}), if you enabled the config to automatically update, the update will download after the warning screen and then the game will restart", false);
                ModNeedsToUpdate = true;
                }
            MTM101BaldiDevAPI.AddWarningScreen(LatestUpdateLog, false);
        }
        void Awake() {
            Instance = this;
            StartCoroutine(GetLatestModVersion());
            AutoUpdateMod = Config.Bind<bool>("Library Setting", "AutoUpdate", false);
            MTM101BaldAPI.MTM101BaldiDevAPI.AddWarningScreen("NilLib loaded!", false);
            MTM101BaldAPI.Registers.LoadingEvents.RegisterOnAssetsLoaded(Info, StartLoad(), MTM101BaldAPI.Registers.LoadingEventOrder.Start);
            var h = new Harmony(Info.Metadata.GUID);
            h.PatchAll();
        }
        public IEnumerator StartLoad() {
            yield return 1;
            yield return "Updating Nillib (ignored if no updates or disabled)";
            if (ModNeedsToUpdate && AutoUpdateMod.Value)
            {
                var req = UnityWebRequest.Get("api.gamebanana.com/Core/Item/Data?itemtype=Mod&itemid=713906&fields=Files().aFiles()");
                yield return req.SendWebRequest();
                if (req.result == UnityWebRequest.Result.Success)
                {
                    var it = JToken.Parse(req.downloadHandler.text);
                    var downloadurl = "";
                    long totalBytes = 0;
                    foreach (var item in ((JObject)it[0]).Properties())
                    {
                        downloadurl = item.Value["_sDownloadUrl"].Value<string>();
                        totalBytes = item.Value["_nFilesize"].Value<long>();
                        break;
                    }
                    var downloadReq = UnityWebRequest.Get(downloadurl);
                    downloadReq.SendWebRequest();
                    while (!downloadReq.isDone)
                    {
                        yield return $"NilLib Progress: {downloadReq.downloadHandler.data.Length}/{totalBytes} bytes downloaded";
                        yield return new WaitForSeconds(0.1f);
                    }
                    var tempFile = File.Create(BepInEx.Paths.CachePath + "/Nilib.zip");
                    using (BinaryWriter write = new(tempFile))
                    {
                        write.Write(downloadReq.downloadHandler.data);
                    }
                    File.Delete(BepInEx.Paths.PluginPath + "/NilLib.dll");
                    File.Delete(BepInEx.Paths.PluginPath + "/NilLib.xml");
                    File.Delete(BepInEx.Paths.PluginPath + "/NilLib.pdb");
                    ZipFile.ExtractToDirectory(BepInEx.Paths.CachePath + "/Nilib.zip", BepInEx.Paths.PluginPath);
                    yield return "Updated! The game will close in a few seconds\nOpen it again and Nillib will be updated";
                    yield return new WaitForSeconds(2f);
                    Application.Quit(0);
                }
            }


        }
        public IEnumerator DelayCode(Action code, float time)
        {
            yield return new WaitForSeconds(time);
            code();
        }


    }
}
