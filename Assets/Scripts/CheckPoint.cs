using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class CheckPoint : MonoBehaviour
{
    [Tooltip("Optional object (flag/light) to enable when checkpoint is activated.")]
    [SerializeField] private GameObject activatedVisual;
    private bool activated;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (activated) return;                 // already used

        other.SendMessage("SetCheckpoint", transform.position, SendMessageOptions.DontRequireReceiver);

        activated = true;
        if (activatedVisual) activatedVisual.SetActive(true);
    }
}
