using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Media.Imaging;

namespace StaTobashi.Models
{
    class ImageSelector
    {
        private static readonly string directoryPath = Path.Combine(
            Path.GetDirectoryName(Assembly.GetEntryAssembly().Location),
            "images");

        private Random random;

        private IList<string> files;

        public ImageSelector()
        {
            files = Directory.GetFiles(directoryPath)
                .Where(x => x.Contains(".png") ||
                            x.Contains(".gif") ||
                            x.Contains(".jpg") ||
                            x.Contains(".jpeg") ||
                            x.Contains(".bmp")).ToList<string>();

            if (files.Count == 0) throw new FileNotFoundException();

            random = new Random();
        }

        public BitmapImage GetRandomImage()
        {
            var path = files[random.Next(files.Count)];
            return new BitmapImage(new Uri(path));
        }
    }
}
