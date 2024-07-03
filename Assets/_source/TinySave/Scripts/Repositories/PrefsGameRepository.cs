using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
//using Unity.Plastic.Newtonsoft.Json;

namespace TinySave
{
    public sealed class PrefsGameRepository : IGameRepository
    {
        private string _gameStateKey = "HVKSNTinyGameState";
        private string _key = "private const string KEY = \"private const string KEY = \"private const string KEY = \"what\";\";\";";

        private Dictionary<string, string> gameState = new();

        public void SetKey(string key)
        {
            _key = key;
        }
         
        public void SetGameStateKey(string gameStateKey)
        { 
            _gameStateKey = gameStateKey;
        }


        public bool LoadState()
        {
            if (PlayerPrefs.HasKey(_gameStateKey))
            {
                var serializedState = PlayerPrefs.GetString(_gameStateKey);
                this.gameState = JsonConvert.DeserializeObject<Dictionary<string, string>>(serializedState);
                return true;
            }
            else
            {
                this.gameState = new Dictionary<string, string>();
                return false;
            }
        }

        public void SaveState()
        {
            var serializedState = JsonConvert.SerializeObject(this.gameState);
            PlayerPrefs.SetString(_gameStateKey, serializedState);
        }

        public void DeleteRepository()
        {
            PlayerPrefs.DeleteKey(_gameStateKey);
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
