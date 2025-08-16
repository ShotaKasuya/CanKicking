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
            var scene = System.IO.Path.GetFileNameWithoutExtension(Scene);
            GetComponent<TextMeshPro>().text = scene;
        }
    }
}