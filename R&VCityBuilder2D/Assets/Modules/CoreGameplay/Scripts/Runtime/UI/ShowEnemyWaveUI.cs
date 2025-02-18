using System;
using Modules.CoreGameplay.Scripts.Runtime.WavesSystem;
using Modules.GameEngine.Runtime.Scripts;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Modules.CoreGameplay.Scripts.Runtime.UI
{
    public class ShowEnemyWaveUI : MonoBehaviour
    {
        [SerializeField] private WavesController _wavesController;
        [SerializeField] private TextMeshProUGUI _waveNumberText;
        [SerializeField] private TextMeshProUGUI _waveMessageText;
        [FormerlySerializedAs("_waveEnemyImageSpawnPosition")] [SerializeField] private RectTransform _waveEnemySpawnPositionIndicator;

        private void Start()
        {
            _wavesController.OnWaveNumberChanged += WaveController_OnWaveNumberChanged;
            SetWaveNumberText("Wave " + _wavesController.GetWaveNumber());
        }

        private void WaveController_OnWaveNumberChanged(object sender, System.EventArgs e)
        {
            SetWaveNumberText("Wave " + _wavesController.GetWaveNumber());
        }

        private void Update()
        {
            var nextWaveSpawnTimer = _wavesController.GetNextWaveSpawnTimer();
            if (nextWaveSpawnTimer <= 0)
            {
                SetMessageText("");
            }
            else
            {
                SetMessageText("Next Wave in " + nextWaveSpawnTimer.ToString("F1") + " seconds");
            }
            
            var dirToNextSpawnPosition = (_wavesController.GetSpawnPosition() - 
                                          GameMotor.Instance.MainCamera.transform.position).normalized;
            _waveEnemySpawnPositionIndicator.anchoredPosition = dirToNextSpawnPosition * 300f;
            _waveEnemySpawnPositionIndicator.eulerAngles = new Vector3(
                0,0, UtilityClass.GetAngleFromVector(dirToNextSpawnPosition));
        }


        private void SetMessageText(string message)
        {
            _waveMessageText.SetText(message);
        }
        
        private void SetWaveNumberText(string text)
        {
            _waveNumberText.SetText(text);
        }
        
    }
}