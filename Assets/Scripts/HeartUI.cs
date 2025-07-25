using UnityEngine;
using UnityEngine.UI;

public class FearHeartUI : MonoBehaviour
{
    [SerializeField] private Image outlineImage;
    [SerializeField] private Image fillImage;

    void Awake()
    {
        if (outlineImage == null)
        {
            outlineImage = GetComponent<Image>();
        }
        if (fillImage == null)
        {
            fillImage = transform.Find("Fill_Image")?.GetComponent<Image>();
        }
    }

    public void UpdateHeart(float fillPercentage)
    {
        if (fillImage == null) return;

        fillImage.fillAmount = fillPercentage;
    }
}