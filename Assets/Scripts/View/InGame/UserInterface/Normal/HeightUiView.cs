﻿using Cysharp.Text;
using Interface.InGame.UserInterface;
using TMPro;
using UnityEngine;

namespace View.InGame.UserInterface.Normal
{
    public class HeightUiView : MonoBehaviour, IHeightUiView
    {
        [SerializeField] private TextMeshProUGUI heightText;

        public void SetHeight(float height)
        {
            heightText.text = ZString.Format("{0:F2}m", height);
        }
    }
}