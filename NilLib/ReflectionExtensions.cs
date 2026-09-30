using HarmonyLib;
using MTM101BaldAPI.Reflection;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace NilLib
{
    /// <summary>
    /// Some extensions for reflections
    /// </summary>
    public static class ReflectionExtensions
    {
        public static Dictionary<(Type type, string field), FieldInfo> CachedFieldInfos = new();

        private static FieldInfo GetORMakeNewFieldInfo(Type type, string field) {
            if (CachedFieldInfos.ContainsKey((type,field))) {
                return CachedFieldInfos[(type, field)];
            } else {
                var fieldinf = AccessTools.Field(type, field);
                CachedFieldInfos.Add((type, field),fieldinf);
                return fieldinf;
            }
        }


        /// <summary>
        /// Sets a privated field of an object to a value (Is cached and wont waste memory)
        /// </summary>
        /// <param name="Obj"></param>
        /// <param name="field"></param>
        /// <param name="value"></param>
        public static void SetCachedReflection(this object Obj, string field, object value)
        {
            var fieldinf = GetORMakeNewFieldInfo(Obj.GetType(), field);
            fieldinf.SetValue(Obj, value);
        }

        /// <summary>
        /// Gets a privated field of an object (Is cached and wont waste memory)
        /// </summary>
        /// <param name="Obj"></param>
        /// <param name="field"></param>
        /// <returns></returns>
        public static object GetCachedReflection(this object Obj, string field)
        {
            var fieldinf = GetORMakeNewFieldInfo(Obj.GetType(), field);
            return fieldinf.GetValue(Obj);
        }
        /// <summary>
        /// Gets a privated field of an object and is casted to the type that you have put (Is cached and wont waste memory)
        /// </summary>
        /// <param name="Obj"></param>
        /// <param name="field"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T GetCachedReflection<T>(this object Obj, string field)
        {
            var fieldinf = GetORMakeNewFieldInfo(Obj.GetType(), field);
            return (T)fieldinf.GetValue(Obj);
        }
    }
}
