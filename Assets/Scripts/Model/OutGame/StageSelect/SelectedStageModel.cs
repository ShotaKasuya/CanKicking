using System;
using System.Collections.Generic;
using System.Linq;
using Interface.Model.OutGame;
using Module.SceneReference.Runtime;
using UnityEngine;

namespace Model.OutGame.StageSelect
{
    public class SelectedStageModel : ISelectedStageModel
    {
        public string SelectedStage { get; private set; }

        public void SetSelectedStage(string sceneReference)
        {
            SelectedStage = sceneReference;
        }
    }

    [Serializable]
    public class StageScenesModel : IStageScenesModel
    {
        [SerializeField] private List<SceneField> stages;

        public IReadOnlyList<string> SceneList => stages.Select(x => (string)x).ToList();
    }
}