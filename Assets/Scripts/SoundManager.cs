using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    //Variable para asignar el slider en unity

    [SerializeField] Slider volumeSlider;

    
    void Start()
    {
        //Aquí, comprobamos si ya hemos guardado un valor para "musicVolume" antes.

        if (!PlayerPrefs.HasKey("musicVolume"))
        {
            // Si no hay un valor guardado, establecemos el volumen de la música a 1 (máximo) y luego cargamos ese valor en el Slider.
            PlayerPrefs.SetFloat("musicVolume", 1);
            load();
        }

        // Si ya existe un valor guardado para "musicVolume", simplemente lo cargamos en el Slider.
        else
        {
            load();
        }
    }

    public void ChangeVolume()
    {
        

        AudioListener.volume = volumeSlider.value;
        Save();
        
        /* controla el volumen general de todos los sonidos en el juego.
        Lo igualamos al valor actual del Slider. 
        y lo guardamos*/
    }

    //Metodos privados para cargar y guardar el volumen
    private void load()
    {
        volumeSlider.value = PlayerPrefs.GetFloat("musicVolume");
    }

    private void Save()
    {
        PlayerPrefs.SetFloat("musicVolume", volumeSlider.value);
    }
}
