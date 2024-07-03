using System.IO;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;

namespace TinySave
{
    public sealed class FileGameRepository : IGameRepository
    {
        private string _saveFileName = "MyGameState.bin";
        private string _key = "private const string KEY = \"private const string KEY = \"private const string KEY = \"what\";\";\";";

        private Dictionary<string, string> gameState = new();

        public void SetKey(string key)
        { 
            _key = key;
        }

        public void SetFileName(string fileName)
        { 
            _saveFileName = fileName;
        }

        public bool LoadState()
        {
            string fullPath = Path.Combine(Application.streamingAssetsPath, _saveFileName);
            if (File.Exists(fullPath))
            {
                FileStream fileStream = new FileStream(fullPath, FileMode.Open, FileAccess.Read);
                BinaryReader reader = new BinaryReader(fileStream);
                char[] dataArray = reader.ReadChars((int)fileStream.Length);
                reader.Close();
                fileStream.Close();
                
                var serializedState = StringCipher.Decrypt(new string(dataArray), _key);
                this.gameState = JsonConvert.DeserializeObject<Dictionary<string, string>>(serializedState);
                return true;
            }
            else
            {
                this.gameState = new Dictionary<string, string>();
                return false;
            }
        }

        public void DeleteRepository()
        {
            string fullPath = Path.Combine(Application.streamingAssetsPath, _saveFileName);
            if (File.Exists(fullPath))
            { 
                File.Delete(fullPath);
            }
        }

        public void SaveState()
        {
            var serializedState = StringCipher.Encrypt(JsonConvert.SerializeObject(this.gameState), _key);
            char[] dataArray = serializedState.ToCharArray(0, serializedState.Length);
            string fullPath = Path.Combine(Application.streamingAssetsPath, _saveFileName);

            if (!Directory.Exists(Application.streamingAssetsPath))
            {
                Directory.CreateDirectory(Application.streamingAssetsPath);
            }

            FileStream fileStream = new FileStream(fullPath, FileMode.OpenOrCreate, FileAccess.Write);
            BinaryWriter writer = new BinaryWriter(fileStream);
            writer.Write(dataArray);
            writer.Close();
            fileStream.Close();

            Debug.Log($"Data saved to: {fullPath}");
        }

        public T GetData<T>()
        {
            var serializedData = this.gameState[typeof(T).Name];
            return JsonConvert.DeserializeObject<T>(serializedData);
        }

        public bool TryGetData<T>(out T value)
        {
            if (this.gameState.TryGetValue(typeof(T).Name, out var serializedData))
            {
                value = JsonConvert.DeserializeObject<T>(serializedData);
                return true;
            }

            value = default;
            return false;
        }

        public void SetData<T>(T value)
        {
            var serializedData = JsonConvert.SerializeObject(value);
            this.gameState[typeof(T).Name] = serializedData;
        }
    }
}
