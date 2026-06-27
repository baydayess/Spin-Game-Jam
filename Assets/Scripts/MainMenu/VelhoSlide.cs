using TMPro;
using UnityEngine;

public class VelhoSlide : MonoBehaviour
{
    private RectTransform rect;

    void Start()
    {
        rect = GetComponent<RectTransform>();
    }

    void Update()
    {
        rect.anchoredPosition = Vector2.Lerp(
            rect.anchoredPosition,
            Vector2.zero,
            2f * Time.deltaTime
        );
    }
}
