using UnityEngine;

namespace AndriiYefimov.SayolloHW.Models
{
    public class FileModel
    {
        private readonly string _fileName;

        public string FileFullPath => $"{FileDirectory}/{_fileName}";
        public string FileDirectory { get; private set; }

        protected FileModel(string fileDirectory, string fileName)
        {
            FileDirectory = fileDirectory;
            _fileName = fileName;
            
            Debug.Log($"FileFullPath: {FileFullPath}");
        }
    }
}