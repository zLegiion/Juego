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
            Debug.LogError("¡No hay slots de barra de herramientas asignados en el ToolbarManager! Por favor, arrástralos en el Inspector.");
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
            Debug.LogWarning($"Intento de seleccionar un slot fuera de rango: {newIndex}. Rango válido: 1 a {toolbarSlots.Count}");
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

    private ToolbarSlot GetSlotByIndex(int index)
    {
        foreach (ToolbarSlot slot in toolbarSlots)
        {
            if (slot.slotIndex == index)
            {
                return slot;
            }
        }
        return null;
    }

    public void AddQuantityToSlot(int slotIndex, int amountToAdd)
    {
        if (slotIndex < 1 || slotIndex > toolbarSlots.Count)
        {
            Debug.LogWarning($"Intento de agregar cantidad a un slot fuera de rango: {slotIndex}.");
            return;
        }

        ToolbarSlot targetSlot = GetSlotByIndex(slotIndex);
        if (targetSlot != null)
        {
            if (slotQuantities.ContainsKey(slotIndex))
            {
                slotQuantities[slotIndex] += amountToAdd;
            }
            else
            {
                slotQuantities.Add(slotIndex, amountToAdd);
            }

            if (slotIndex == 1)
            {
                targetSlot.UpdateSlotContent(fireflyIcon, slotQuantities[slotIndex]);
            }
            else
            {

                targetSlot.UpdateSlotContent(targetSlot.ItemIconSprite, slotQuantities[slotIndex]);
            }

            Debug.Log($"Cantidad del Slot {slotIndex} actualizada a: {slotQuantities[slotIndex]}");
        }
    }

    public void SetQuantityInSlot(int slotIndex, int newQuantity)
    {
        if (slotIndex < 1 || slotIndex > toolbarSlots.Count)
        {
            Debug.LogWarning($"Intento de establecer cantidad en un slot fuera de rango: {slotIndex}.");
            return;
        }

        ToolbarSlot targetSlot = GetSlotByIndex(slotIndex);
        if (targetSlot != null)
        {
            slotQuantities[slotIndex] = newQuantity;

            if (slotIndex == 1)
            {
                targetSlot.UpdateSlotContent(fireflyIcon, slotQuantities[slotIndex]);
            }
            else
            {

                targetSlot.UpdateSlotContent(targetSlot.ItemIconSprite, slotQuantities[slotIndex]);
            }

            Debug.Log($"Cantidad del Slot {slotIndex} establecida a: {slotQuantities[slotIndex]}");
        }
    }
}