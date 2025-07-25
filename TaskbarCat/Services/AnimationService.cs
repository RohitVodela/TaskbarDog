using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows.Media.Imaging;

namespace TaskbarCat.Services
{
    public class AnimationService
    {
        private readonly Dictionary<string, List<BitmapImage>> _allFrames = new Dictionary<string, List<BitmapImage>>();

        public AnimationService()
        {
            LoadAllFrames();
        }

        public List<BitmapImage> GetFrames(string animal)
        {
            return _allFrames.ContainsKey(animal) ? _allFrames[animal] : new List<BitmapImage>();
        }

        private void LoadAllFrames()
        {
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string imagesDirectory = Path.Combine(baseDirectory, "Images");

            if (!Directory.Exists(imagesDirectory))
            {
                Debug.WriteLine($"Images directory not found: {imagesDirectory}");
                return;
            }

            foreach (var animalDirectory in Directory.GetDirectories(imagesDirectory))
            {
                string animalName = new DirectoryInfo(animalDirectory).Name;
                var frames = new List<BitmapImage>();
                
                var imageFiles = Directory.GetFiles(animalDirectory, "*.png");
                foreach (var imageFile in imageFiles)
                {
                    var image = new BitmapImage();
                    image.BeginInit();
                    image.UriSource = new Uri(imageFile, UriKind.Absolute);
                    image.EndInit();
                    frames.Add(image);
                }
                _allFrames.Add(animalName, frames);
                Debug.WriteLine($"Loaded {frames.Count} frames for {animalName}");
            }
        }
    }
}
