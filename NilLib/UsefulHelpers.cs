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
        /// The layers in bb+
        /// </summary>
        
        public enum Layers
        {
            Default, // Default
            Billboard,
            ClickableEntities,
            StandardEntities,
            CollidableEntities,
            Player,
            ClickableCollidableEntities,
            Map,
            Subtitles,

            Overlay
        }
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
