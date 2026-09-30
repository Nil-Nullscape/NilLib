using MTM101BaldAPI.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace NilLib
{
/// <summary>
/// All stuff that can be helpful to use
/// </summary>
    static public class UsefulHelpers
    {

        /// <summary>
        /// Delays code without having to make an IEnumerator
        /// </summary>
        /// <param name="codeToDelay">Code to execute</param>
        /// <param name="time">time in seconds</param>
        static public void delay(Action codeToDelay, float time = 1f)
        {
            NilLibPlugin.Instance.StartCoroutine(NilLibPlugin.Instance.DelayCode(codeToDelay, time));

        }
        
        /// <summary>
        /// All layers in bb+
        /// </summary>
        public enum Layers
        {
            /// <summary>
            /// Default layer
            /// </summary>
            Default, 
            /// <summary>
            /// Billboard layer
            /// </summary>
            Billboard,
            /// <summary>
            /// Layer for all clickable entities
            /// </summary>
            ClickableEntities,
            /// <summary>
            /// Layer for all Standard entities
            /// </summary>
            StandardEntities,
            /// <summary>
            /// Layer for all Collidable entities
            /// </summary>
            CollidableEntities,
            /// <summary>
            /// Layer for player stuff
            /// </summary>
            Player,
            /// <summary>
            /// Layer for all clickable & collidable entities
            /// </summary>
            ClickableCollidableEntities,
            /// <summary>
            /// Map layer
            /// </summary>
            Map,
            /// <summary>
            /// Subtitles layer
            /// </summary>
            Subtitles,
            /// <summary>
            /// Overlay layer
            /// </summary>
            Overlay
        }
        /// <summary>
        /// Sprite assets for textMeshPro
        /// </summary>
        public enum SpriteAssetSelection
        {
            None,
            MathmachineNumbers
        }

        /// <summary>
        /// Easily get a layer
        /// </summary>
        /// <param name="layer"></param>
        /// <returns>the int is the layer, (for like gameobject.Layer)</returns>
        static public int GetLayerFromEnum(Layers layer)
        {
            return LayerMask.NameToLayer(layer.ToString());
        }

        /// <summary>
        /// Kills the player
        /// </summary>
        /// <param name="npc"> The npc that kills the player</param>
        /// <param name="Deathsounds">The jumpscare sounds</param>
        static public void KillPlayer(NPC npc, WeightedSoundObject[] Deathsounds) {
            var bald = npc.gameObject.AddComponent<Baldi>();
            bald.enabled = false;
            bald.loseSounds = Deathsounds;
            bald.CaughtPlayer(Singleton<CoreGameManager>.Instance.GetPlayer(0));
            UnityEngine.Object.Destroy(bald);
        }

        static public Color GetColorFromRgb(float r, float g, float b)
        {
            return new(r / 255, g / 255, b / 255);
        }
        static public TMP_SpriteAsset GetSpriteassetFromEnum(SpriteAssetSelection SpriteAsset) {
            switch (SpriteAsset)
            {
                default:
                    return null;
                case SpriteAssetSelection.MathmachineNumbers:
                    return Resources.FindObjectsOfTypeAll<TMP_SpriteAsset>().First(x => x.name == "NumberFontSheet");

            }
        }
    }
}
