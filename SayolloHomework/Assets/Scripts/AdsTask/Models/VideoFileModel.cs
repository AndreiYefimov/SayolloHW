using System;
using UnityEngine.AI;

namespace AndriiYefimov.SayolloHW.Models
{
    public class VideoFileModel : FileModel
    {
        private TimeSpan _duration;

        public float Duration => _duration.Seconds;
        
        public VideoFileModel(string fileDirectory, string fileName, TimeSpan duration) : base(fileDirectory, fileName)
        {
            _duration = duration;
        }
    }
}