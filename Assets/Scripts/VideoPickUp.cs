using UnityEngine;
using UnityEngine.VFX;
using UnityEngine.Video;

[RequireComponent(typeof(Collider2D))]
public class VideoPickUp : MonoBehaviour
{
    private GameObject objActivate;
    private VideoPlayer video;

    public void Init(GameObject activeIObj, VideoPlayer videoInit)
    {
        objActivate = activeIObj;
        video = videoInit;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;

        if (objActivate != null) objActivate.SetActive(true);

        if (video != null) video.Play();

        Destroy(gameObject);
    }

}
