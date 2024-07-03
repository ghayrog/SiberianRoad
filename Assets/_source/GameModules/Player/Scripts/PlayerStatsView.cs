using System;
using UnityEngine;
using UnityEngine.UI;

namespace Player
{
    [Serializable]
    public sealed class PlayerStatsView
    {
        [SerializeField]
        private Image _horseProgressBar;

        [SerializeField]
        private Image _wagonProgressBar;

        public void UpdatePlayerStats(float horsePower, float wagonPower)
        {
            float correctHorsePower = Mathf.Max(Mathf.Min(horsePower, 1), 0);
            float correctWagonPower = Mathf.Max(Mathf.Min(wagonPower, 1), 0);
            _horseProgressBar.fillAmount = correctHorsePower;
            _wagonProgressBar.fillAmount = correctWagonPower;
        }
    }
}
