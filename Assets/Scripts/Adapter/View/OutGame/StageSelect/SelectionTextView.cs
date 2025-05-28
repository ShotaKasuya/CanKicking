using Adapter.IView.OutGame.StageSelect;
using TMPro;
using UnityEngine;

namespace Adapter.View.OutGame.StageSelect
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class SelectionTextView : MonoBehaviour, ISelectedStageTextView
    {
        private TextMeshProUGUI _sceneText;

        private void Awake()
        {
            _sceneText = GetComponent<TextMeshProUGUI>();
        }

        public void SetStage(string selectedStage)
        {
            if (_sceneText.text == string.Empty)
            {
                gameObject.SetActive(true);
            }

            _sceneText.text = selectedStage;
        }

        public void ResetStage()
        {
            gameObject.SetActive(false);
            _sceneText.text = string.Empty;
        }
    }
}