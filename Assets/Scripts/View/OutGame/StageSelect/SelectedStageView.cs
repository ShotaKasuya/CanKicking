using Interface.OutGame.StageSelect;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

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

        public void ShowStage(string sceneName)
        {
            if (sceneText.text == string.Empty)
            {
                gameObject.SetActive(true);
            }

            var scene = SceneManager.GetSceneByName(sceneName);

            sceneText.text = scene.name;
        }
    }
}