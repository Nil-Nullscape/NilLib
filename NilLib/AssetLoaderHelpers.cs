using JetBrains.Annotations;
using MTM101BaldAPI.AssetTools;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine;

namespace NilLib
{

/// <summary>
/// Some helpers for asset loading, mostly for loading from folder
/// </summary>
    public static class AssetLoaderHelpers
    {

        /// <summary>
        /// Loads all sprites from a folder
        /// </summary>
        /// <param name="Folderpath">The folder path with all sprites</param>
        /// <param name="pixelPerUnits">Pixel per units for all sprites</param>
        /// <param name="fileprefix">Only load files if the file name starts with</param>
        /// <param name="filesuffix">Only load files if the file name ends with</param>
        /// <returns></returns>
        public static Sprite[] LoadSpritesFromFolder(string Folderpath,float pixelPerUnits, string fileprefix = "spr_", string filesuffix = "") {
            var files = Directory.GetFiles(Folderpath, $"{fileprefix}*{filesuffix}.png");
            List<Sprite> sprites = new();
            foreach (var item in files)
            {
                sprites.Add(AssetLoader.SpriteFromFile(item, Vector2.one / 2, pixelPerUnits));
            }

            return sprites.ToArray();
        }
        /// <summary>
        /// Loads all audios from a folder
        /// </summary>
        /// <param name="FolderPath">The folder path with all sprites</param>
        /// <param name="fileprefix">Only load files if the file name starts with</param>
        /// <param name="filesuffix">Only load files if the file name ends with</param>
        /// <returns></returns>
        public static AudioClip[] LoadAudioFromFolder(string FolderPath, string fileprefix = "aud_", string filesuffix = "")
        {
            string[] Extensions = [".mp3", ".ogg", ".wav"];
            var files = Directory.GetFiles(FolderPath, $"{fileprefix}*{filesuffix}")
            .Where(file => Extensions.Contains(Path.GetExtension(file))).ToArray();
            List<AudioClip> audios = new();
            foreach (var item in files)
            {
                audios.Add(AssetLoader.AudioClipFromFile(item));
            }
            return audios.ToArray();


        }
        /// <summary>
        /// Loads all spritesheets from a folder
        /// </summary>
        /// <param name="FolderPath">The folder path with all sprites</param>
        /// <param name="horz">how many horizontal tiles (for every spritesheets, use <see cref="LoadSpriteSheetsFromFolder(string, Dictionary{string, Vector2}, float, string, string)"/> instead if you want to have individual sizes for each specific spritesheets)</param>
        /// <param name="vert">how many vertical tiles (for every spritesheets, use <see cref="LoadSpriteSheetsFromFolder(string, Dictionary{string, Vector2}, float, string, string)"/> instead if you want to have individual sizes for each specific spritesheets)</param>
        /// <param name="PixelsPerUnits">Pixel per units for all sprites</param>
        /// <param name="fileprefix">Only load files if the file name starts with</param>
        /// <param name="filesuffix">Only load files if the file name ends with</param>
        /// <returns></returns>
        public static List<Sprite[]> LoadSpriteSheetsFromFolder(string FolderPath, int horz, int vert, float PixelsPerUnits, string fileprefix = "sheet_", string filesuffix = "")
        {
            var spritesheets = new List<Sprite[]>();
            var files = Directory.GetFiles(FolderPath, $"{fileprefix}*{filesuffix}.png");

            foreach (var item in files)
            {
                spritesheets.Add(AssetLoader.SpritesFromSpritesheet(horz, vert, PixelsPerUnits, Vector2.one / 2, AssetLoader.TextureFromFile(item)));
            }
            return spritesheets;
        }
        /// <summary>
        /// Loads all spritesheets from a folder with each their different spritesheet size
        /// </summary>
        /// <param name="FolderPath">The folder path with all sprites</param>
        /// <param name="SpritesheetsSize">Each spritesheet's sizes (string is file name vector2 is the sizes (x is horizontal tiles and y is vertical tiles)) if you want to load all spritesheets and they all have the same sizes use <see cref="LoadSpriteSheetsFromFolder(string, int, int, float, string, string)"/> instead</param>
        /// <param name="PixelsPerUnits">Pixel per units for all sprites</param>
        /// <param name="fileprefix">Only load files if the file name starts with</param>
        /// <param name="filesuffix">Only load files if the file name ends with</param>
        /// <returns></returns>
        public static List<Sprite[]> LoadSpriteSheetsFromFolder(string FolderPath, Dictionary<string,Vector2> SpritesheetsSize, float PixelsPerUnits, string fileprefix = "sheet_", string filesuffix = "")
        {
            var spritesheets = new List<Sprite[]>();
            var files = Directory.GetFiles(FolderPath, $"{fileprefix}*{filesuffix}.png");

            foreach (var item in files)
            {
                var horz = 1;
                var vert = 1;
                Vector2 size;
                if ((size = SpritesheetsSize[Path.GetFileName(item)]) != null) {
                    horz = Mathf.RoundToInt(size.x);
                    vert = Mathf.RoundToInt(size.y);
                }
                spritesheets.Add(AssetLoader.SpritesFromSpritesheet(horz, vert, PixelsPerUnits, Vector2.one / 2, AssetLoader.TextureFromFile(item)));
            }
            return spritesheets;
        }
    }
}
