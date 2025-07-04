using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private RawImage[] hearts = new RawImage[4];
    private int currentLives;
    void Start()
    {
        currentLives = hearts.Length;

        for(int i = 0; i < hearts.Length; i++)
            hearts[i].enabled = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
            TakeDamage();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "TearsFrog")
        {
            Destroy(collision.gameObject); //destruye el objeto contra el que choque
            IncreaseHearts(); 
        }
    }

    private void TakeDamage()
    {
        currentLives--; //current lives - 1

        hearts[currentLives].enabled = false;

        if (currentLives <= 0)
            Debug.Log("te moriste xD");
    }

    private void IncreaseHearts()
    {               
        if(currentLives <= 4)
        {
            hearts[currentLives].enabled = true;
            currentLives++;            
        }   
    }
}
