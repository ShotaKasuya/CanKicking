using Interface.OutGame.StageSelect;
using Module.SceneReference;
using TMPro;
using UnityEngine;

namespace View.OutGame.StageSelect
{
    public class SelectedStageView : MonoBehaviour, ISelectedStageView
    {
        [SerializeField] private TextMeshProUGUI sceneText;

        public void Reset()
        {
            gameObject.SetActive(false);
            sceneText.text = string.Empty;
        }

        public void ShowStage(SceneReference sceneReference)
        {
            if (sceneText.text == string.Empty)
            {
                gameObject.SetActive(true);
            }

            sceneText.text = sceneReference.SceneName;
        }
    }
}