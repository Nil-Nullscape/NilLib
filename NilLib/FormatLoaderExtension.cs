using PlusStudioLevelFormat;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace NilLib
{
    /// <summary>
    /// Extensions for studio format or loader
    /// </summary>
    public static class FormatLoaderExtension
    {
        public static BaldiRoomAsset[] LoadFolderRooms(string folderPath)
        {
            var files = Directory.GetFiles(folderPath);
            var d = new List<BaldiRoomAsset>();
            foreach (var item in files)
            {
                BinaryReader binaryReader = new(File.OpenRead(item));
                var a = BaldiRoomAsset.Read(binaryReader);
                d.Add(a);
                binaryReader.Close();

            }
            return d.ToArray();
        }

        public static BaldiRoomAsset LoadRoom(string path)
        {

            BinaryReader binaryReader = new(File.OpenRead(path));
            var a = BaldiRoomAsset.Read(binaryReader);

            binaryReader.Close();


            return a;
        }
    }
}
