using TMPro;
using UnityEngine;

namespace View.OutGame.StageSelect
{
    [RequireComponent(typeof(TextMeshPro))]
    [RequireComponent(typeof(BoxCollider2D))]
    public class StageIconView: StageFactoryView.AbstractStageIconView
    {
        protected override void OnInit()
        {
            GetComponent<TextMeshPro>().text = Scene;
        }
    }
}