using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;
using System.Xml.Serialization;

namespace StaTobashi.Models
{
    [Serializable]
    public class Config
    {
        public static readonly double AreaHeightMin = 300;
        public static readonly int IntervalRangeMin = 1;
        public static readonly int IntervalRangeMax = 300;
        public static readonly int IntervalStep = 1;
        public static readonly int DurationRangeMin = 100;
        public static readonly int DurationRangeMax = 10000;
        public static readonly int DurationStep = 100;
        public static readonly double ScaleRangeMin = 0.1;
        public static readonly double ScaleRangeMax = 3.0;
        public static readonly double ScaleStep = 0.1;

        private static readonly string filePath = Path.Combine(
            Path.GetDirectoryName(Assembly.GetEntryAssembly().Location),
            "Config.xml");

        public static Config Current { get; set; }

        public double Left { get; set; }
        public double Top { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        public bool Topmost { get; set; }
        public int LaunchIntervalSeconds { get; set; }
        public int LaunchDurationMillisecondsMin { get; set; }
        public int LaunchDurationMillisecondsMax { get; set; }
        public double Scale { get; set; }

        public static void Load()
        {
            try
            {
                if (filePath == null || !File.Exists(filePath))
                {
                    throw new FileNotFoundException(filePath);
                }

                var serializer = new XmlSerializer(typeof(Config));

                using (var sr = new StreamReader(filePath, new UTF8Encoding(false)))
                {
                    Current = (Config)serializer.Deserialize(sr);
                }
            }
            catch (Exception e)
            {
                Current = GetInitialSettings();
                Debug.WriteLine(e);
            }
        }

        public static Config GetInitialSettings()
        {
            return new Config
            {
                Left = 50,
                Top = 50,
                Width = 800,
                Height = 800,
                Topmost = true,
                LaunchIntervalSeconds = 15,
                LaunchDurationMillisecondsMin = 500,
                LaunchDurationMillisecondsMax = 3000,
                Scale = 0.5,
            };
        }

        public void Save()
        {
            var serializer = new XmlSerializer(typeof(Config));

            using (var sw = new StreamWriter(filePath, false, new UTF8Encoding(false)))
            {
                try
                {
                    serializer.Serialize(sw, this);
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e);
                }
            }
        }
    }
}
