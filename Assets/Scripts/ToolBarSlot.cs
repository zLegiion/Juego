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
    [SerializeField] private Color defaultColor = new Color(0.2f, 0.3f, 0.4f, 1f);
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

        SetSelected(false);
    }

    public void SetSelected(bool isSelected)
    {
        if (slotImage != null)
        {
            slotImage.color = isSelected ? highlightColor : defaultColor;
        }
    }

    public void UpdateSlotContent(Sprite iconSprite, int quantity)
    {
        if (itemIcon != null)
        {
            itemIcon.sprite = iconSprite;

            itemIcon.enabled = (iconSprite != null);
        }

        if (itemQuantityText != null)
        {
            itemQuantityText.text = (quantity > 0) ? quantity.ToString() : "";
            itemQuantityText.enabled = (quantity > 0);
        }
    }

    public void ClearSlot()
    {
        UpdateSlotContent(null, 0);
    }
}

