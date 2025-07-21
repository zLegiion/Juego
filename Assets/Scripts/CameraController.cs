using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform Objetivo;
    public float VelocidadCamara = 0.25f;
    public Vector3 desplazamiento;

    private void LateUpdate()
    {
        Vector3 posicionDeseada = Objetivo.position + desplazamiento;
        Vector3 posicionSuavisada = Vector3.Lerp(transform.position, posicionDeseada, VelocidadCamara);

        transform.position = posicionSuavisada;
    }
}
