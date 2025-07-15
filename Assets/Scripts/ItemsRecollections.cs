using UnityEngine;
using UnityEngine.UI; // Asegúrate de que esto sea necesario, si no, puedes quitarlo.

public class ItemsRecollections : MonoBehaviour
{
    private ToolbarManager toolbarManager;

    // ¡¡¡FALTABA ESTE MÉTODO START!!!
    private void Start()
    {
        toolbarManager = FindAnyObjectByType<ToolbarManager>();
        if (toolbarManager == null)
        {
            Debug.LogError("ItemsRecollections: ¡ERROR! ToolbarManager NO ENCONTRADO en la escena. La recolección de ítems no funcionará.");
        }
        else
        {
            Debug.Log("ItemsRecollections: ToolbarManager ENCONTRADO correctamente.");
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        // Verifica si el objeto con el que colisionó tiene el tag "FireFlies"
        if (other.gameObject.CompareTag("FireFlies")) //Son luciernagas?
        {
            // Intenta obtener el componente FireflyNode del objeto
            FireflyNode fireflyNode = other.gameObject.GetComponent<FireflyNode>(); // Si o No

            // Si se encontró el componente FireflyNode y el jugador presiona "F"
            if (fireflyNode != null && Input.GetKeyDown(KeyCode.F)) //Si la respuesta fue si y el jugador presiona la "F"
            {
                Debug.Log("ItemsRecollections: Se detectó interacción con Luciérnaga y se presionó 'F'.");
                int collectedAmount = fireflyNode.Collect(); // Recolecta las luciernagas

                if (collectedAmount > 0) // Si habia luciernagas, osea no estaba en cooldown
                {
                    Debug.Log($"ItemsRecollections: Luciérnaga recolectada: {collectedAmount} unidades.");
                    if (toolbarManager != null)
                    {
                        toolbarManager.AddQuantityToSlot(1, collectedAmount);
                        Debug.Log($"ItemsRecollections: Llamada a AddQuantityToSlot con ID 1 y cantidad {collectedAmount}.");
                    }
                    else
                    {
                        Debug.LogError("ItemsRecollections: toolbarManager es NULL al intentar agregar luciérnagas. No se pudo añadir al inventario.");
                    }
                }
                else
                {
                    Debug.Log("ItemsRecollections: La luciérnaga ya fue recolectada o el nodo no estaba activo.");
                }
            }
        }
    }
}