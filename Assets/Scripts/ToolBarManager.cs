using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class ToolbarManager : MonoBehaviour
{
    [Header("Referencias de Slots")]
    [SerializeField] private List<ToolbarSlot> toolbarSlots = new List<ToolbarSlot>();

    private Dictionary<int, int> slotQuantities = new Dictionary<int, int>();

    [Header("Iconos de Ítems")]
    [SerializeField] private Sprite fireflyIcon;

    private int currentSelectedSlotIndex = 1;

    private void Awake()
    {
        if (toolbarSlots == null || toolbarSlots.Count == 0)
        {
            Debug.LogError("¡No hay slots de barra de herramientas asignados!");
            return;
        }

        foreach (ToolbarSlot slot in toolbarSlots)
        {
            if (!slotQuantities.ContainsKey(slot.slotIndex))
            {
                slotQuantities.Add(slot.slotIndex, 0);
            }
            slot.UpdateSlotContent(null, 0);
        }

        ToolbarSlot firstSlot = GetSlotByIndex(1);
        if (firstSlot != null && fireflyIcon != null)
        {
            firstSlot.UpdateSlotContent(fireflyIcon, slotQuantities[1]);
        }
    }

    private void Start()
    {
        SelectSlot(currentSelectedSlotIndex);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) { SelectSlot(1); }
        else if (Input.GetKeyDown(KeyCode.Alpha2)) { SelectSlot(2); }
        else if (Input.GetKeyDown(KeyCode.Alpha3)) { SelectSlot(3); }
        else if (Input.GetKeyDown(KeyCode.Alpha4)) { SelectSlot(4); }
        else if (Input.GetKeyDown(KeyCode.Alpha5)) { SelectSlot(5); }
    }

    private void SelectSlot(int newIndex)
    {
        if (newIndex < 1 || newIndex > toolbarSlots.Count)
        {
            Debug.LogWarning($"Slot fuera de rango: {newIndex}.");
            return;
        }

        if (currentSelectedSlotIndex >= 1 && currentSelectedSlotIndex <= toolbarSlots.Count)
        {
            ToolbarSlot previousSlot = GetSlotByIndex(currentSelectedSlotIndex);
            if (previousSlot != null)
            {
                previousSlot.SetSelected(false);
            }
        }

        currentSelectedSlotIndex = newIndex;
        ToolbarSlot selectedSlot = GetSlotByIndex(currentSelectedSlotIndex);
        if (selectedSlot != null)
        {
            selectedSlot.SetSelected(true);
            Debug.Log($"Slot {currentSelectedSlotIndex} seleccionado.");
        }
    }

    public void AddQuantityToSlot(int slotIndex, int amountToAdd)
    {
        if (!slotQuantities.ContainsKey(slotIndex))
        {
            Debug.LogWarning($"Slot {slotIndex} no existe.");
            return;
        }

        slotQuantities[slotIndex] += amountToAdd;
        ToolbarSlot targetSlot = GetSlotByIndex(slotIndex);
        if (targetSlot != null)
        {
            Sprite iconToUse = (slotIndex == 1) ? fireflyIcon : targetSlot.ItemIconSprite;
            targetSlot.UpdateSlotContent(iconToUse, slotQuantities[slotIndex]);
        }
        else
        {
            Debug.LogWarning($"No se encontró ToolbarSlot visual para {slotIndex}.");
        }

        Debug.Log($"Cantidad del Slot {slotIndex} actualizada a: {slotQuantities[slotIndex]}");
    }

    public bool RemoveQuantityFromSlot(int slotIndex, int amountToRemove)
    {
        if (!slotQuantities.ContainsKey(slotIndex))
        {
            Debug.LogWarning($"Slot {slotIndex} no existe.");
            return false;
        }

        if (slotQuantities[slotIndex] >= amountToRemove)
        {
            slotQuantities[slotIndex] -= amountToRemove;
            ToolbarSlot targetSlot = GetSlotByIndex(slotIndex);
            if (targetSlot != null)
            {
                Sprite iconToUse = (slotIndex == 1) ? fireflyIcon : targetSlot.ItemIconSprite;
                targetSlot.UpdateSlotContent(iconToUse, slotQuantities[slotIndex]);
            }
            else
            {
                Debug.LogWarning($"No se encontró ToolbarSlot visual para {slotIndex}.");
            }
            Debug.Log($"Removidas {amountToRemove} del Slot {slotIndex}. Cantidad actual: {slotQuantities[slotIndex]}");
            return true;
        }
        else
        {
            Debug.LogWarning($"No hay suficientes ítems en el Slot {slotIndex}.");
            return false;
        }
    }

    public int GetQuantityInSlot(int slotIndex)
    {
        if (slotQuantities.ContainsKey(slotIndex))
        {
            return slotQuantities[slotIndex];
        }
        Debug.LogWarning($"Slot {slotIndex} no existe. Retornando 0.");
        return 0;
    }

    public void SetQuantityInSlot(int slotIndex, int newQuantity)
    {
        if (!slotQuantities.ContainsKey(slotIndex))
        {
            Debug.LogWarning($"Slot {slotIndex} no existe.");
            return;
        }

        slotQuantities[slotIndex] = newQuantity;
        ToolbarSlot targetSlot = GetSlotByIndex(slotIndex);
        if (targetSlot != null)
        {
            Sprite iconToUse = (slotIndex == 1) ? fireflyIcon : targetSlot.ItemIconSprite;
            targetSlot.UpdateSlotContent(iconToUse, slotQuantities[slotIndex]);
        }
        else
        {
            Debug.LogWarning($"No se encontró ToolbarSlot visual para {slotIndex}.");
        }
        Debug.Log($"Cantidad del Slot {slotIndex} establecida a: {slotQuantities[slotIndex]}");
    }

    private ToolbarSlot GetSlotByIndex(int index)
    {
        if (index >= 1 && index <= toolbarSlots.Count)
        {
            return toolbarSlots.FirstOrDefault(s => s.slotIndex == index);
        }
        return null;
    }
}