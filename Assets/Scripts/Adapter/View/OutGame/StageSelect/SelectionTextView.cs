using Adapter.IView.OutGame.StageSelect;
using TMPro;
using UnityEngine;

namespace Adapter.View.OutGame.StageSelect
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class SelectionTextView : MonoBehaviour, ISelectedStageTextView
    {
        private GameObject _self;
        private TextMeshProUGUI _sceneText;

        private void Awake()
        {
            _self = gameObject;
            _sceneText = GetComponent<TextMeshProUGUI>();
            ResetStage();
        }

        public void SetStage(string selectedStage)
        {
            if (_sceneText.text == string.Empty)
            {
                _self.SetActive(true);
            }

            _sceneText.text = selectedStage;
        }

        public void ResetStage()
        {
            _self.SetActive(false);
            _sceneText.text = string.Empty;
        }
    }
}