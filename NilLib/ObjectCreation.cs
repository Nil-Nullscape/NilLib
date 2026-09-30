using MTM101BaldAPI;
using MTM101BaldAPI.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

namespace NilLib.Creators
{
/// <summary>
/// Functions to create objects easily
/// </summary>
    static public class ObjectCreation
    {
    /// <summary>
    /// Creates a sprite renderer Gameobject
    /// </summary>
    /// <param name="billboarded"></param>
    /// <param name="name">name of gameobject</param>
    /// <returns></returns>
        static public SpriteRenderer MakeRenderer(bool billboarded = true, string name = "") {
            var gameObj = new GameObject(name);
            gameObj.layer = LayerMask.NameToLayer("Billboard");
            var spriterenderer = gameObj.AddComponent<SpriteRenderer>();
            if (billboarded) spriterenderer.material = Resources.FindObjectsOfTypeAll<Material>().First(x => x.name == "SpriteStandard_Billboard");
            return spriterenderer;
        }
        /// <summary>
        /// Creates a sprite renderer gameobject and sets its parent
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="billboarded"></param>
        /// <param name="name">name of gameobject</param>
        /// <returns></returns>
        static public SpriteRenderer MakeRenderer(Transform parent, bool billboarded = true, string name = "") {
            var sr = MakeRenderer(billboarded, name);
            sr.transform.SetParent(parent);
            return sr;
        }
        /// <summary>
        /// Creates a sprite renderer gameobject and its layer, tip: if you dont know any layers use <see cref="UsefulHelpers.GetLayerFromEnum(UsefulHelpers.Layers)"/>
        /// </summary>
        /// <param name="CustomLayer">the layer to set it to</param>
        /// <param name="billboarded"></param>
        /// <param name="name">name of gameobject</param>
        /// <returns></returns>
        static public SpriteRenderer MakeRenderer(LayerMask CustomLayer, bool billboarded = true, string name = "")
        {
            var sr = MakeRenderer(billboarded, name);
            sr.gameObject.layer = CustomLayer;
            return sr;

        }
        /// <summary>
        /// Creates a sprite renderer gameobject and sets its parent and its layer, tip: if you dont know any layers use <see cref="UsefulHelpers.GetLayerFromEnum(UsefulHelpers.Layers)"/>
        /// </summary>
        /// <param name="CustomLayer">the layer to set it to</param>
        /// <param name="parent"></param>
        /// <param name="billboarded"></param>
        /// <param name="name">name of gameobject</param>
        /// <returns></returns>
        static public SpriteRenderer MakeRenderer(LayerMask CustomLayer,Transform parent, bool billboarded = true, string name = "")
        {
            var sr = MakeRenderer(parent,billboarded, name);
            sr.gameObject.layer = CustomLayer;
            return sr;

        }
        
        /// <summary>
        /// Creates a textmeshpro text
        /// </summary>
        /// <param name="billboarded"></param>
        /// <param name="text"></param>
        /// <param name="tmpspriteasset">use to get a spriteasset <see cref="UsefulHelpers.GetSpriteassetFromEnum(UsefulHelpers.SpriteAssetSelection)"/></param>
        /// <returns></returns>
        static public TextMeshPro MakeTMPObject(bool billboarded = true, string text = "", TMP_SpriteAsset tmpspriteasset = null) {
            var gameOj = new GameObject("TMPText");
            var textObj = gameOj.AddComponent<TextMeshPro>();
            textObj.text = text;
            textObj.spriteAsset = tmpspriteasset;
            gameOj.AddComponent<BillboardUpdater>();

            return textObj;
        }
        /// <summary>
        /// Creates a textmeshpro text and sets its layer
        /// </summary>
        /// <param name="billboarded"></param>
        /// <param name="text"></param>
        /// <param name="tmpspriteasset">use <see cref="UsefulHelpers.GetSpriteassetFromEnum(UsefulHelpers.SpriteAssetSelection)"/> to get a spriteasset </param>
        /// <param name="CustomLayer">use <see cref="UsefulHelpers.GetLayerFromEnum(UsefulHelpers.Layers)"/> to get a layer</param>
        /// <returns></returns>
        static public TextMeshPro MakeTMPObject(LayerMask CustomLayer, bool billboarded = true, string text = "", TMP_SpriteAsset tmpspriteasset = null) {
            var txt = MakeTMPObject(billboarded,text,tmpspriteasset);
            txt.gameObject.layer = CustomLayer;
            return txt;
        }

        static public AudioManager CreateAudioManager(float minDistance, float maxDistance, bool Positional = true, GameObject gameobject = null)
        {
            GameObject obj;
            if (gameobject)
            {
                obj = gameobject;
            }
            else
            {
                obj = new GameObject("AudMan");
            }
            obj.SetActive(false);
            var aud = obj.AddComponent<AudioManager>();
            aud.audioDevice = obj.AddComponent<AudioSource>();
            if (Positional)
            {
                aud.audioDevice.spatialBlend = 1;
                aud.positional = true;

                aud.audioDevice.maxDistance = maxDistance;
                aud.audioDevice.minDistance = minDistance;
            }
            else
            {
                aud.positional = false;
                aud.audioDevice.spatialBlend = 0;
            }
            obj.SetActive(true);
            return aud;

        }
        /// <summary>
        /// Creates a activity wall sign
        /// </summary>
        /// <param name="name"></param>
        /// <param name="right"></param>
        /// <param name="left"></param>
        /// <returns></returns>
        public static Animator CreateActivityWallSign(string name, Sprite right, Sprite left)
        {
            Animator doorSign = GameObject.Instantiate(Resources.FindObjectsOfTypeAll<Animator>().First(x => x.name == "ActivityExteriorSign_MathMachine"));
            doorSign.gameObject.ConvertToPrefab(true);
            doorSign.name = name;
            SpriteRenderer[] spriteRenderers = doorSign.GetComponentsInChildren<SpriteRenderer>();
            spriteRenderers[0].sprite = right;
            spriteRenderers[1].sprite = left;
            return doorSign;
        }

        public static void CreateHangingBonusSign(Transform parent, Activity activity, Vector3 offset,float rotation = 90f) {
            Animator sign = UnityEngine.Object.Instantiate(Resources.FindObjectsOfTypeAll<Animator>().First(x=> x.name =="ActivityExteriorSign"));
            sign.transform.SetParent(parent, false);
            sign.transform.localPosition = offset;
            sign.transform.rotation = Quaternion.Euler(0f, rotation, 0f);
            activity.ReflectionSetVariable("exteriorSigns", new List<Animator>() { sign });
        }
    }
}
