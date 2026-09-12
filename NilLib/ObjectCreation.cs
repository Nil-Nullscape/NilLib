using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TMPro;
using UnityEngine;

namespace NilLib.Creators
{
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
        

        static public TextMeshPro MakeTMPObject(bool billboarded = true, string text = "", TMP_SpriteAsset tmpspriteasset = null) {
            var gameOj = new GameObject("TMPText");
            var textObj = gameOj.AddComponent<TextMeshPro>();
            textObj.text = text;
            textObj.spriteAsset = tmpspriteasset;
            gameOj.AddComponent<BillboardUpdater>();

            return textObj;
        }

        static public TextMeshPro MakeTMPObject(LayerMask CustomLayer, bool billboarded = true, string text = "", TMP_SpriteAsset tmpspriteasset = null) {
            var txt = MakeTMPObject(billboarded,text,tmpspriteasset);
            txt.gameObject.layer = CustomLayer;
            return txt;
        }
    }
}
