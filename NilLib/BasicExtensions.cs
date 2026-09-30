using BepInEx;
using MTM101BaldAPI.AssetTools;
using MTM101BaldAPI.Reflection;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace NilLib.extension
{
/// <summary>
/// Basic extensions
/// </summary>
    static public class BasicExtensions
    {
        
    /// <summary>
    /// Plays a sound once, gets the sound from an assetmanager
    /// </summary>
    /// <param name="AudMan"></param>
    /// <param name="AssetMan"></param>
    /// <param name="key"></param>
        static public void PlaySingleViaAssetMan(this AudioManager AudMan,AssetManager AssetMan, string key)
        {
            AudMan.PlaySingle(AssetMan.Get<SoundObject>(key));
        }
        /// <summary>
        /// Queues the sound, gets the sound from an assetmanager
        /// </summary>
        /// <param name="AudMan"></param>
        /// <param name="AssetMan"></param>
        /// <param name="key"></param>
        /// <param name="playimmediately">Should it play the sound immediately after queued?</param>
        static public void QueueaudioViaAssetMan(this AudioManager AudMan, AssetManager AssetMan, string key, bool playimmediately = false)
        {
            AudMan.QueueAudio(AssetMan.Get<SoundObject>(key), playimmediately);

        }
        /// <summary>
        /// Expanded version of <seealso cref="QueueaudioViaAssetMan(AudioManager, AssetManager, string, bool)"/>
        /// </summary>
        /// <param name="AudMan"></param>
        /// <param name="AssetMan"></param>
        /// <param name="key"></param>
        /// <param name="flushqueue"></param>
        /// <param name="shouldloop"></param>
        /// <param name="playimmediately"></param>
        static public void QueueaudioViaAssetManExt(this AudioManager AudMan, AssetManager AssetMan, string key, bool flushqueue = false, bool shouldloop = false, bool playimmediately = false)
        {
            if (flushqueue) { AudMan.FlushQueue(true); }
            AudMan.QueueAudio(AssetMan.Get<SoundObject>(key), playimmediately);
            if (shouldloop) { AudMan.SetLoop(true); }
        }

        /// <summary>
        /// Add a room function to a room controller ingame, not to the roomasset.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="room"></param>
        static public void AddRoomFunctiontoRoomIngame<T>(this RoomController room) where T : RoomFunction
        {
            var funct = room.functionObject.AddComponent<T>();
            room.functions.AddFunction(funct);
            funct.ReflectionSetVariable("room", room);

        }

        /// <summary>
        /// Converts a standard NPC type to T, useful instead of having to cast
        /// </summary>
        /// <typeparam name="T">Type to convert npc into</typeparam>
        /// <param name="npc">the npc to convert</param>
        /// <returns></returns>

        [Obsolete("Use ConvertType<T> instead")]
        static public T ConvertNpcType<T>(this NPC npc) where T : NPC {
            return (T)npc;
        }

        /// <summary>
        /// Basically casts a type, makes code look less messy
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Type"></param>
        /// <returns></returns>
        static public T ConvertType<T>(this object Type) {
            return (T)Type;
        }

        /// <summary>
        /// This is useful for like HUD elements that shouldnt persists on other levels.
        /// </summary>
        /// <param name="obj"></param>
        [Obsolete("Not made yet",true)]
        static public void AddobjectToBeDeletedOnLevelFinished(this GameObject obj)
        {
           
        }

        /// <summary>
        /// Extension to get a plugin's assets path faster
        /// </summary>
        /// <param name="plug"></param>
        /// <returns></returns>
        static public string GetModPath(this BaseUnityPlugin plug) {
            return AssetLoader.GetModPath(plug);
        }

       
    }
}
