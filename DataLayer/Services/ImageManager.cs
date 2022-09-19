using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DataLayer.Services
{
    /// <summary>
    /// This is used to manage all the images in the application.
    /// 
    /// This is a bit hacky way to manage images in this application
    /// Reasoning for why I went this route is to keep the DataLayer models clean from custom frontend requirements (eg. images).
    /// </summary>
    public class ImageManager
    {
        private const string ImageMappingsFile = @"../../../DataLayer/Images/image_mappings.txt";
        private const string DefaultImagePath = @"../../../DataLayer/Images/giga_chad.jpg";
        private const char Delimiter = ';';

        private static readonly Lazy<ImageManager> _lazy =
            new Lazy<ImageManager>(() => new ImageManager());

        public static ImageManager Instance =>
            _lazy.Value; // using lazy initialization to have a sort of singleton instance of this object. 

        private ImageManager()
        {
        }

        public void SaveImageForPlayer(string player, string imagePath)
        {
            using var sw = System.IO.File.AppendText(ImageMappingsFile);
            sw.WriteLine($"{player}{Delimiter}{imagePath}");
        }

        public string GetUriForPlayerImage(string player)
        {
            if (!System.IO.File.Exists(ImageMappingsFile))
            {
                return GetFullPathForDefaultImage();
            }

            var mappings = System.IO.File.ReadAllLines(ImageMappingsFile)
                .Reverse() // use last set image.
                .Distinct(new ImageMappingComparer()) //avoid duplicates
                .ToDictionary(
                    (mapping) => mapping.Split(Delimiter)[0],
                    (mapping) => mapping.Split(Delimiter)[1]
                );
            mappings.TryGetValue(player, out var uri);
            if (!System.IO.File.Exists(uri))
            {
                return GetFullPathForDefaultImage();
            }
            return uri ?? GetFullPathForDefaultImage();
        }

        private string GetFullPathForDefaultImage()
        {
                return System.IO.Path.GetFullPath(DefaultImagePath);
        }

        /// <summary>
        /// This method is used to group same image entries so that we can filter the same ones out in the Distinct call
        /// </summary>
        private class ImageMappingComparer : IEqualityComparer<string>
        {
            public bool Equals(string s1, string s2)
            {
                if (s1 == null || s2 == null)
                {
                    return false;
                }
                
                // Format of mappings is: {Player Name};{Path to file}
                // we want two entries to be equal if {Player Name} is equal
                return s1.Split(Delimiter)[0] == s2.Split(Delimiter)[0];
            }

            public int GetHashCode(string obj)
            {
                return obj.Split(Delimiter)[0].Length;
            }
        }
    }
}