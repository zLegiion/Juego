using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ToolbarSlot : MonoBehaviour
{
    [Header("Referencias de UI")]
    [SerializeField] private Image slotImage;
    [SerializeField] private Image itemIcon;
    [SerializeField] private TextMeshProUGUI slotNumberText;
    [SerializeField] private TextMeshProUGUI itemQuantityText;

    [Header("Configuración del Slot")]
    public int slotIndex;
    [SerializeField] private Color highlightColor = new Color(0.9f, 0.7f, 0.1f, 1f);


    public Sprite ItemIconSprite
    {
        get { return itemIcon != null ? itemIcon.sprite : null; }
    }


    private void Awake()
    {
        if (slotImage == null)
        {
            slotImage = GetComponent<Image>();
        }
        if (slotNumberText == null)
        {
            slotNumberText = transform.Find("SlotNumberText")?.GetComponent<TextMeshProUGUI>();
        }
        if (itemIcon == null)
        {
            itemIcon = transform.Find("ItemIcon")?.GetComponent<Image>();
        }
        if (itemQuantityText == null)
        {
            itemQuantityText = transform.Find("ItemQuantityText")?.GetComponent<TextMeshProUGUI>();
        }
    }

    public void SetSelected(bool isSelected)
    {
        if (slotImage != null)
        {
            if (isSelected)
            {
                slotImage.color = highlightColor;
            }
        }
    }

    public void UpdateSlotContent(Sprite iconSprite, int quantity)
    {
        if (itemIcon != null)
        {
            if (iconSprite != null)
            {
                itemIcon.sprite = iconSprite;
            }
            itemIcon.enabled = true;
        }

        if (itemQuantityText != null)
        {
            itemQuantityText.text = quantity.ToString();
            itemQuantityText.enabled = true;
        }
    }

    public void ClearSlot()
    {
        if (itemIcon != null)
        {
            itemIcon.sprite = null;
        }
        if (itemQuantityText != null)
        {
            itemQuantityText.text = "0";
            itemQuantityText.enabled = true;
        }
    }
}