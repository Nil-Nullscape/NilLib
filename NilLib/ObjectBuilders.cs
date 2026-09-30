using MTM101BaldAPI;
using MTM101BaldAPI.Reflection;
using MTM101BaldAPI.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using UnityEngine;

namespace NilLib.Builders
{
    
    /// <summary>
    /// Room function container easy builder, start with CreateBaseObject function to start
    /// </summary>
    public class RoomFunctionContainerBuilder {
        private List<RoomFunction> functions = new();
        private GameObject baseObject;
        private string Name = "RoomFunctionContainer";

        private RoomFunctionContainerBuilder() {

        }

        public static RoomFunctionContainerBuilder CreateBaseObject() {
            var Builder = new RoomFunctionContainerBuilder();
            Builder.baseObject = new GameObject();
            Builder.baseObject.ConvertToPrefab(true);
            Builder.baseObject.AddComponent<RoomFunctionContainer>();
            Builder.baseObject.GetComponent<RoomFunctionContainer>().SetCachedReflection("functions", new List<RoomFunction>());
            return Builder;
        }

        


        /// <summary>
        /// Adds a function to container
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public RoomFunctionContainerBuilder AddFunction<T>(out T Function) where T : RoomFunction
        {
            var comp = baseObject.AddComponent<T>();
            baseObject.GetComponent<RoomFunctionContainer>().AddFunction(comp);
            Function = comp;
            return this;
        }
        /// <summary>
        /// Sets the name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public RoomFunctionContainerBuilder SetName(string name) {
            Name = name;
            return this;
        }

        public RoomFunctionContainerBuilder AddCustomChalkboardWithText(string titleKey, string descriptionKey) {
            var poster = ObjectCreators.CreatePosterObject(Resources.FindObjectsOfTypeAll<Texture2D>().First(x => x.name == "chk_blank"), [
                new() {
                    alignment = TMPro.TextAlignmentOptions.Center,
                    color = Color.white,
                    font = BaldiFonts.ComicSans24.FontAsset(),
                    fontSize = 24,
                    position = new(0,184),
                    size = new(256,32),
                    style = TMPro.FontStyles.Normal,
                    textKey = titleKey
                },
                new() {
                    alignment = TMPro.TextAlignmentOptions.Center,
                    color = Color.white,
                    font = BaldiFonts.ComicSans12.FontAsset(),
                    fontSize = 12,
                    position = new(24,88),
                    size = new(208,96),
                    style = TMPro.FontStyles.Normal,
                    textKey = descriptionKey
                }
            ]);

            AddFunction<ChalkboardBuilderFunction>(out var chalkboard);

            chalkboard.SetCachedReflection("chalkBoards", new WeightedPosterObject[] {
                new() {
                selection = poster,
                weight = 100 
                }
            });

            return this;
        }

        public RoomFunctionContainer Build() {
            var baseRoomContainer = baseObject.GetComponent<RoomFunctionContainer>();

            baseRoomContainer.name = Name;

            return baseRoomContainer;
        }
    }
}
