using NUnit.Framework;
using UnityEngine;
using UnityEngine.UI;

namespace View.InGame.UserInterface.Normal
{
    public class RangeCircleUI : MonoBehaviour
    {
        [SerializeField] private Canvas targetCanvas; // Screen Space - CameraのCanvas
        [SerializeField] private Camera canvasCamera; // Canvasに紐づくCamera

        [SerializeField] private Sprite circleOutlineSprite; // 線だけの円スプライト（2px線幅のものを想定）

        private GameObject minCircleObj;
        private GameObject maxCircleObj;
        private Image minCircleImage;
        private Image maxCircleImage;

        private void Awake()
        {
            CreateCircleObjects();
            var center = new Vector2(Screen.width, Screen.height) / 2;
            ShowRangeCircles(center, 10, 100);
        }

        private void CreateCircleObjects()
        {
            minCircleObj = new GameObject("MinRangeCircle", typeof(RectTransform), typeof(CanvasRenderer),
                typeof(Image));
            maxCircleObj = new GameObject("MaxRangeCircle", typeof(RectTransform), typeof(CanvasRenderer),
                typeof(Image));

            minCircleObj.transform.SetParent(targetCanvas.transform, false);
            maxCircleObj.transform.SetParent(targetCanvas.transform, false);

            minCircleImage = minCircleObj.GetComponent<Image>();
            maxCircleImage = maxCircleObj.GetComponent<Image>();

            // スプライト設定
            minCircleImage.sprite = circleOutlineSprite;
            maxCircleImage.sprite = circleOutlineSprite;

            // 色設定
            minCircleImage.color = Color.gray;
            maxCircleImage.color = Color.red;

            // 画像のタイプはSimpleかSlicedを選択（スプライトに応じて）
            minCircleImage.type = Image.Type.Sliced;
            maxCircleImage.type = Image.Type.Sliced;

            // 線幅はスプライトの9スライス設定によるが、
            // 2px線幅をスプライト側で用意しておく想定

            // ピボットは中心
            minCircleObj.GetComponent<RectTransform>().pivot = new Vector2(0.5f, 0.5f);
            maxCircleObj.GetComponent<RectTransform>().pivot = new Vector2(0.5f, 0.5f);
        }

        /// <summary>
        /// 画面短辺の長さに対する割合で指定された半径をもつ2つの円を表示する
        /// centerScreenPosition: スクリーン座標系での中心位置
        /// minRadiusRatio: 画面短辺に対する最小円の半径割合 (0〜1)
        /// maxRadiusRatio: 画面短辺に対する最大円の半径割合 (0〜1)
        /// </summary>
        public void ShowRangeCircles(Vector2 centerScreenPosition, float minRadiusRatio, float maxRadiusRatio)
        {
            if (!minCircleObj.activeSelf)
            {
                minCircleObj.SetActive(true);
                maxCircleObj.SetActive(true);
            }

            float shortSide = Mathf.Min(Screen.width, Screen.height);
            float minRadius = shortSide * minRadiusRatio;
            float maxRadius = shortSide * maxRadiusRatio;

            // CanvasのRectTransform内のローカル座標を計算
            RectTransform canvasRect = targetCanvas.GetComponent<RectTransform>();

            Vector2 localPos;
            if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRect, centerScreenPosition, canvasCamera,
                    out localPos))
            {
                Debug.LogWarning("ScreenPointToLocalPointInRectangle変換失敗");
                return;
            }

            // RectTransformを設定
            RectTransform minRect = minCircleObj.GetComponent<RectTransform>();
            RectTransform maxRect = maxCircleObj.GetComponent<RectTransform>();

            minRect.anchoredPosition = localPos;
            maxRect.anchoredPosition = localPos;

            minRect.sizeDelta = new Vector2(minRadius * 2f, minRadius * 2f);
            maxRect.sizeDelta = new Vector2(maxRadius * 2f, maxRadius * 2f);
        }

        public void HideRangeCircles()
        {
            minCircleObj.SetActive(false);
            maxCircleObj.SetActive(false);
        }
    }
}