using UnityEngine;
using System.Collections.Generic;

public class ToolbarManager : MonoBehaviour
{
    [Header("Referencias de Slots")]
    //todos los scripts ToolbarSlot en la barra de herramientas
    [SerializeField] private List<ToolbarSlot> toolbarSlots = new List<ToolbarSlot>();

    private int currentSelectedSlotIndex = 1; // El índice del slot actualmente seleccionado (empieza en 1)

    private void Start()
    {
        // Asegurarse de que tenemos slots asignados
        if (toolbarSlots == null || toolbarSlots.Count == 0)
        {
            Debug.LogError("¡No hay slots de barra de herramientas asignados en el ToolbarManager! Por favor, arrástralos en el Inspector.");
            return;
        }

        // Iniciar el primer slot como seleccionado
        SelectSlot(currentSelectedSlotIndex);
    }

    private void Update()
    {
        // Detectar la entrada de las teclas numéricas (1 a 5)
        if (Input.GetKeyDown(KeyCode.Alpha1)) { SelectSlot(1); }
        else if (Input.GetKeyDown(KeyCode.Alpha2)) { SelectSlot(2); }
        else if (Input.GetKeyDown(KeyCode.Alpha3)) { SelectSlot(3); }
        else if (Input.GetKeyDown(KeyCode.Alpha4)) { SelectSlot(4); }
        else if (Input.GetKeyDown(KeyCode.Alpha5)) { SelectSlot(5); }
        // Pueden añadir mas teclas 
    }

    // Método para seleccionar un slot específico por su numero
    private void SelectSlot(int newIndex)
    {
        // Asegurarse de que el numero esté dentro del rango válido de slots
        if (newIndex < 1 || newIndex > toolbarSlots.Count)
        {
            Debug.LogWarning($"Intento de seleccionar un slot fuera de rango: {newIndex}. Rango válido: 1 a {toolbarSlots.Count}");
            return;
        }

        // Deseleccionar el slot anterior (si hay uno y es diferente al nuevo)
        if (currentSelectedSlotIndex >= 1 && currentSelectedSlotIndex <= toolbarSlots.Count)
        {
            ToolbarSlot previousSlot = GetSlotByIndex(currentSelectedSlotIndex);
            if (previousSlot != null)
            {
                previousSlot.SetSelected(false);
            }
        }

        // Seleccionar el nuevo slot
        currentSelectedSlotIndex = newIndex;
        ToolbarSlot selectedSlot = GetSlotByIndex(currentSelectedSlotIndex);
        if (selectedSlot != null)
        {
            selectedSlot.SetSelected(true);
            Debug.Log($"Slot {currentSelectedSlotIndex} seleccionado.");
            // Aquí se ´puede activar la lógica para usar el ítem en este slot --> UseItemInSlot(currentSelectedSlotIndex);
        }
    }

    // Método para obtener un slot por su índice (el índice de la UI, no el de la lista)
     
    private ToolbarSlot GetSlotByIndex(int index)
    {
        // los slots están ordenados en la lista por su slotIndex 1, 2, 3..
        foreach (ToolbarSlot slot in toolbarSlots)
        {
            if (slot.slotIndex == index)
            {
                return slot;
            }
        }
        return null; 
    }


    /* Este método es un ejemplo de cómo podrías actualizar el contenido de un slot desde otro script
     
    public void UpdateSlot(int slotIndexToUpdate, Sprite newIcon, int newQuantity)
    {
        ToolbarSlot slotToUpdate = GetSlotByIndex(slotIndexToUpdate);
        if (slotToUpdate != null)
        {
            slotToUpdate.UpdateSlotContent(newIcon, newQuantity);
        }
        else
        {
            Debug.LogWarning($"No se encontró el slot con índice {slotIndexToUpdate} para actualizar.");
        }
    }

     un ejemplo de cómo se puede "usar" el ítem del slot seleccionado
     
    public void UseSelectedItem()
    {
        ToolbarSlot selectedSlot = GetSlotByIndex(currentSelectedSlotIndex);
        if (selectedSlot != null)
        {
            Debug.Log($"Usando el ítem del Slot: {selectedSlot.slotIndex}");
            // Aquí iría la lógica real para usar el ítem,
            // por ejemplo, activar una habilidad, y así.
        }
    }
    */
}