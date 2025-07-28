using UnityEngine;
using UnityEngine.UI;

public class ItemsRecollections : MonoBehaviour
{
    private ToolbarManager toolbarManager;
    private bool firstFireflyCollected = false;

    private void Start()
    {
        toolbarManager = FindAnyObjectByType<ToolbarManager>();
        if (toolbarManager == null)
        {
            Debug.LogError("ItemsRecollections: ERROR! ToolbarManager NOT FOUND in scene. Item collection will not work.");
        }
        else
        {
            Debug.Log("ItemsRecollections: ToolbarManager FOUND correctly.");
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("FireFlies"))
        {
            FireflyNode fireflyNode = other.gameObject.GetComponent<FireflyNode>();

            if (fireflyNode != null && Input.GetKeyDown(KeyCode.F))
            {
                Debug.Log("ItemsRecollections: Se detect interaccin con Lucirnaga y se presion 'F'.");
                int collectedAmount = fireflyNode.Collect();

                if (collectedAmount > 0)
                {
                    Debug.Log($"ItemsRecollections: Lucirnaga recolectada: {collectedAmount} unidades.");
                    if (toolbarManager != null)
                    {
                        toolbarManager.AddQuantityToSlot(1, collectedAmount);
                        Debug.Log($"ItemsRecollections: Llamada a AddQuantityToSlot con ID 1 y cantidad {collectedAmount}.");

                        if (!firstFireflyCollected)
                        {
                            TutoSignals.Instance.ShowHandLanternHint(); // Corregido: Usando TutoSignals.Instance
                            firstFireflyCollected = true;
                        }

                    }
                    else
                    {
                        Debug.LogError("ItemsRecollections: toolbarManager es NULL al intentar agregar lucirnagas. No se pudo aadir al inventario.");
                    }
                }
                else
                {
                    Debug.Log("ItemsRecollections: La lucirnaga ya fue recolectada o el nodo no estaba activo.");
                }
            }
        }
    }
}