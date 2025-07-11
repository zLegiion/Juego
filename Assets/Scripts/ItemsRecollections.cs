using UnityEngine;
using UnityEngine.UI;
public class ItemsRecollections : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "FireFlies")
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                Destroy(other.gameObject);
                Debug.Log("Luciérnaga Recolectada");
            }
        }
    }
}
