using UnityEngine;
using UnityEngine.VFX;
using UnityEngine.Video;

[RequireComponent(typeof(Collider2D))]
public class VideoPickUp : MonoBehaviour
{


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        Destroy(gameObject);
    }

}
