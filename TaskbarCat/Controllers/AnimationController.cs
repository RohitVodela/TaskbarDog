using System;
using System.Collections.Generic;
using System.Windows.Media.Imaging;

namespace TaskbarCat.Controllers
{
    public class AnimationController
    {
        private List<BitmapImage> _frames = new List<BitmapImage>();
        private int _currentFrameIndex = 0;
        private double _frameTime = 0;
        private double _timeSinceLastFrame = 0;

        public BitmapImage? CurrentFrame => _frames.Count > 0 ? _frames[_currentFrameIndex] : null;

        public void SetFrames(List<BitmapImage> frames)
        {
            _frames = frames;
            _currentFrameIndex = 0;
        }

        public void Update(double deltaTime, float cpuUsage)
        {
            if (_frames.Count == 0) return;

            // Adjust frame time based on CPU usage (e.g., 10% CPU -> 0.1s/frame, 100% CPU -> 0.01s/frame)
            _frameTime = 0.1 / (Math.Max(0.1, cpuUsage / 100.0)); 

            _timeSinceLastFrame += deltaTime;
            if (_timeSinceLastFrame >= _frameTime)
            {
                _currentFrameIndex = (_currentFrameIndex + 1) % _frames.Count;
                _timeSinceLastFrame = 0;
            }
        }
    }
}
