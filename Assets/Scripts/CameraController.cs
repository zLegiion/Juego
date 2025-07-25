using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform Objetivo;
    public float VelocidadCamara = 0.25f;
    public Vector3 desplazamiento;

    private float yFija;

    private void Start()
    {
        yFija = transform.position.y;
    }

    private void LateUpdate()
    {
        Vector3 posicionDeseada = new Vector3(
            Objetivo.position.x + desplazamiento.x,
            yFija,
            Objetivo.position.z + desplazamiento.z
        );

        Vector3 posicionSuavisada = Vector3.Lerp(transform.position, posicionDeseada, VelocidadCamara);
        transform.position = posicionSuavisada;
    }
}
