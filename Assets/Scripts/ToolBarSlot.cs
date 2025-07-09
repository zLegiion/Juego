using UnityEngine;
using UnityEngine.UI; 
using TMPro;

public class ToolbarSlot : MonoBehaviour
{
    [Header("Referencias de UI")]
    [SerializeField] private Image slotImage; // La imagen de fondo del slot (para el resaltado)
    [SerializeField] private Image itemIcon;  // La imagen del icono del ítem
    [SerializeField] private TextMeshProUGUI slotNumberText; // El texto del número del slot (123....)
    [SerializeField] private TextMeshProUGUI itemQuantityText; // El texto de la cantidad del ítem 

    [Header("Configuración del Slot")]
    public int slotIndex; // ej. 1 para Slot_1, 2 para Slot_2
    [SerializeField] private Color defaultColor = new Color(0.2f, 0.3f, 0.4f, 1f); // Color por defecto del slot
    [SerializeField] private Color highlightColor = new Color(0.9f, 0.7f, 0.1f, 1f); // Color cuando el slot está seleccionado
    private void Awake()
    {
        // referencias nulas.
        if (slotImage == null)
        {
            slotImage = GetComponent<Image>(); // Intenta obtener la Image del propio GameObject
        }
        if (slotNumberText == null)
        {
            // Busca el componente TextMeshProUGUI en los hijos del slot
            slotNumberText = transform.Find("SlotNumberText")?.GetComponent<TextMeshProUGUI>();
        }
        if (itemIcon == null)
        {
            // Busca el icono del item
            itemIcon = transform.Find("ItemIcon")?.GetComponent<Image>();
        }
        if (itemQuantityText == null)
        {
            // Busca el componente TextMeshProUGUI en los hijos del slot
            itemQuantityText = transform.Find("ItemQuantityText")?.GetComponent<TextMeshProUGUI>();
        }

        // Establecer el color por defecto al inicio
        SetSelected(false);
    }

    // Método para establecer si el slot está seleccionado o no
    public void SetSelected(bool isSelected)
    {
        if (slotImage != null)
        {
            slotImage.color = isSelected ? highlightColor : defaultColor;
        }
    }

    // Método para actualizar visualmente el contenido del slot
    public void UpdateSlotContent(Sprite iconSprite, int quantity)
    {
        if (itemIcon != null)
        {
            itemIcon.sprite = iconSprite;
            itemIcon.enabled = (iconSprite != null); // Ocultar el icono si no hay sprite
        }

        if (itemQuantityText != null)
        {
            itemQuantityText.text = (quantity > 0) ? "x" + quantity.ToString() : ""; // Mostrar cantidad solo si es > 0
            itemQuantityText.enabled = (quantity > 0); // Ocultar texto de cantidad si es 0
        }
    }

    // Método para limpiar el slot (vaciarlo)
    public void ClearSlot()
    {
        UpdateSlotContent(null, 0); // Establece el ícono a nulo y la cantidad a 0
    }
}

