using NaughtyAttributes;

namespace Gameplay
{
    using UnityEngine;

    [RequireComponent(typeof(LineRenderer))]
    public class CircleRenderer : MonoBehaviour
    {
        public float radius = 5f;
        public int pointCount = 100; // Количество точек на окружности
        public bool loop = true; // Замыкать ли линию в круг

        public LineRenderer lineRenderer;

        public void DrawCircle()
        {
            lineRenderer.positionCount = pointCount + (loop ? 1 : 0);

            for (int i = 0; i < pointCount; i++)
            {
                float angle = 0.5f * Mathf.PI * i / pointCount;
                float x = Mathf.Cos(angle) * radius;
                float y = Mathf.Sin(angle) * radius;
                lineRenderer.SetPosition(i, new Vector3(x, y, 0f));
            }

            // Если нужно замкнуть круг, добавим первую точку в конец
            if (loop)
            {
                lineRenderer.SetPosition(pointCount, lineRenderer.GetPosition(0));
            }
        }

        // Публичный метод, если хочешь вызвать перестроение окружности вручную
        [Button]
        public void UpdateCircle()
        {
            DrawCircle();
        }
    }
}
