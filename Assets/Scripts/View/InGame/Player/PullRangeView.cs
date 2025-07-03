using UnityEngine;

namespace View.InGame.Player;

public class PullRangeView : MonoBehaviour
{
    private void DrawCircle(LineRenderer lineRenderer, Vector3 center, float radius, Color color)
    {
        int segments = 64;
        lineRenderer.positionCount = segments + 1;
        lineRenderer.startColor = lineRenderer.endColor = color;

        for (int i = 0; i <= segments; i++)
        {
            float angle = i * 2f * Mathf.PI / segments;
            Vector3 pos = center + new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)) * radius;
            lineRenderer.SetPosition(i, pos);
        }
    }
}