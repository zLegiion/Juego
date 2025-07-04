using UnityEngine;
using UnityEngine.Video;

public class EnemyDrop : MonoBehaviour
{
    [SerializeField] private GameObject itemPrefab;   
    [Range(0, 1)] public float percentProbability = 0.08f;   

    [SerializeField] private GameObject activeVideo;
    [SerializeField] private VideoPlayer video;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            TryDrop();
            Destroy(gameObject);
        }
    }
    public void TryDrop()
    {
        if (Random.value <= percentProbability)
        {
            GameObject itemGO = Instantiate(itemPrefab, transform.position, Quaternion.identity);
            VideoPickUp item = itemGO.GetComponent<VideoPickUp>();
            item.Init(activeVideo, video);
        }
    }
}
