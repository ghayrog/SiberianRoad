using System;
using System.Collections;
using TinyGame;
using UnityEngine;

namespace Player
{

    [Serializable]
    public sealed class PlayerStats : IGameUpdateListener, IGameStartListener
    {
        public event Action OnLowHorsePower;
        public event Action OnLowWagonPower;
        public event Action<float, float> OnStatsChange;

        [field: SerializeField]
        public float horsePower { get; private set; }

        [field: SerializeField]
        public float wagonPower { get; private set; }

        [field: SerializeField]
        public int lastCheckpointId = 0;

        public float ExecutionPriority => (int)LoadingPriority.Low;

        public void OnUpdate(float deltaTime)
        {
            
        }

        public void UpdateCheckpointId(int id)
        {
            if (id > lastCheckpointId)
            { 
                lastCheckpointId = id;
            }
        }

        public void IncreaseHorsePower(float dPower)
        {
            float oldPower = horsePower;
            if (dPower > 0)
            { 
                horsePower += dPower;
            }
            if (horsePower > 1)
            {
                horsePower = 1;
            }
            if (horsePower != oldPower)
            {
                OnStatsChange?.Invoke(horsePower, wagonPower);
            }
        }

        public void DecreaseHorsePower(float dPower)
        {
            float oldPower = horsePower;
            if (dPower > 0)
            {
                horsePower -= dPower;
            }
            if (horsePower < 0)
            {
                horsePower = 0;
            }
            if (horsePower != oldPower)
            {
                OnStatsChange?.Invoke(horsePower, wagonPower);
            }
            if (horsePower == 0)
            {
                OnLowHorsePower?.Invoke();
            }
        }

        public void IncreaseWagonPower(float dPower)
        {
            float oldPower = wagonPower;
            if (dPower > 0)
            {
                wagonPower += dPower;
            }
            if (wagonPower > 1)
            {
                wagonPower = 1;
            }
            if (wagonPower != oldPower)
            {
                OnStatsChange?.Invoke(horsePower, wagonPower);
            }

        }

        public void DecreaseWagonPower(float dPower)
        {
            float oldPower = wagonPower;
            if (dPower > 0)
            {
                wagonPower -= dPower;
            }
            if (wagonPower < 0)
            {
                wagonPower = 0;
            }
            if (wagonPower != oldPower)
            {
                OnStatsChange?.Invoke(horsePower, wagonPower);
            }
            if (wagonPower == 0)
            {
                OnLowWagonPower?.Invoke();
            }
        }

        public void OnGameStart()
        {
            OnStatsChange?.Invoke(horsePower, wagonPower);
            PlayerInstaller gameObject = GameObject.FindFirstObjectByType<PlayerInstaller>();
            gameObject.StartCoroutine(___DelayedChangeStats());
        }

        public IEnumerator ___DelayedChangeStats()
        { 
            yield return new WaitForFixedUpdate();
            OnStatsChange?.Invoke(horsePower, wagonPower);
        }

        public void OnCheckpointRestart()
        {
            horsePower = Mathf.Max(0.2f, horsePower);
            wagonPower = Mathf.Max(0.2f, wagonPower);
            OnStatsChange?.Invoke(horsePower, wagonPower);
        }

        public void SetInitialValues(float horse, float wagon)
        { 
            horsePower = horse;
            wagonPower = wagon;
            OnStatsChange?.Invoke(horsePower, wagonPower);
        }
    }
}
