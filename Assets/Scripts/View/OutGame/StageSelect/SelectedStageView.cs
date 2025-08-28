using Interface.View.OutGame;
using Module.Option.Runtime;
using TMPro;
using UnityEngine;

namespace View.OutGame.StageSelect
{
    public class SelectedStageView : MonoBehaviour, ISelectedStageView
    {
        [SerializeField] private GameObject selectedStageObject;
        [SerializeField] private GameObject stageRecordObject;

        [SerializeField] private TextMeshProUGUI stageNameText;
        [SerializeField] private TextMeshProUGUI stageRecordText;

        public void Reset()
        {
            selectedStageObject.SetActive(false);
            stageRecordObject.SetActive(false);
            stageNameText.text = string.Empty;
            stageRecordText.text = string.Empty;
        }

        public void ShowStage(string sceneName, Option<int> clearRecord)
        {
            if (stageNameText.text == string.Empty)
            {
                selectedStageObject.SetActive(true);
            }

            var scene = System.IO.Path.GetFileNameWithoutExtension(sceneName);
            
            stageNameText.text = scene;

            if (clearRecord.TryGetValue(out var record))
            {
                stageRecordObject.SetActive(true);
                stageRecordText.text = record.ToString();
            }
        }
    }
}