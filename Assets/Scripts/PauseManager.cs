using UnityEngine;

public class MenuPause : MonoBehaviour
{
    public GameObject PauseMenu;
    public bool Pause = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Pause == false)
            {
                PauseMenu.SetActive(true);
                Pause = true;

                Time.timeScale = 0;
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }
        }
    }
}
