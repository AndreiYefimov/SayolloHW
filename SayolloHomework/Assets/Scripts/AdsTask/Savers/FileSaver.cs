using System.IO;
using AndriiYefimov.SayolloHW.Models;
using UnityEngine;

namespace AndriiYefimov.SayolloHW.Savers
{
    public class FileSaver
    {
        public void SaveToDisk(FileModel fileModel, byte[] fileBytes)
        {
            if(!Directory.Exists(fileModel.FileDirectory))
                Directory.CreateDirectory(fileModel.FileDirectory);
            
            File.WriteAllBytes(fileModel.FileFullPath, fileBytes);
            Debug.Log("File was successfully saved");
        }
    }
}