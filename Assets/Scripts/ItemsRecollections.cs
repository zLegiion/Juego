using UnityEngine;
using UnityEngine.UI;

public class ItemsRecollections : MonoBehaviour
{
    private ToolbarManager toolbarManager;

    private void Start()
    {
        toolbarManager = FindAnyObjectByType<ToolbarManager>();
        if (toolbarManager == null)
        {
            Debug.LogError("ToolbarManager no encontrado en la escena. Asegúrate de que existe un GameObject con el script ToolbarManager.");
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("FireFlies"))
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                Destroy(other.gameObject);
                Debug.Log("Luciérnaga Recolectada");

                if (toolbarManager != null)
                {
                    toolbarManager.AddQuantityToSlot(1, 5);
                }
            }
        }
    }
}