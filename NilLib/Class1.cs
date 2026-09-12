using BepInEx;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NilLib
{
[BepInPlugin("Nil.Library","NilLibrary","1.0.0.0")]
    public class NilLibPlugin : BaseUnityPlugin
    {
        public static NilLibPlugin Instance;
        void Awake() {
            Instance = this;
            MTM101BaldAPI.MTM101BaldiDevAPI.AddWarningScreen("NilLib loaded!", false);
        }

        public IEnumerator DelayCode(Action code, float time)
        {
            yield return new WaitForSeconds(time);
            code();
        }


    }
}
