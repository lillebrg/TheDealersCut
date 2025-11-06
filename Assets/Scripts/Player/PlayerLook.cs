using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    public Camera PlayerCamera;
    private float xRotation = 0f;

    public float xSensitivity = 30f;
    public float ySensitivity = 30f;

    private bool canLook = true;

    public void ProcessLook(Vector2 input)
    {
        if (!canLook) return;

        float mouseX = input.x;
        float mouseY = input.y;
        //calculate camera rotation for looking up and down
        xRotation -= (mouseY * Time.deltaTime) * xSensitivity;
        xRotation = Mathf.Clamp(xRotation, -80f, 80f);
        PlayerCamera.transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
        transform.Rotate(Vector3.up * (mouseX * Time.deltaTime) * xSensitivity );
    }

    public void SetCanLook(bool value)
    {
        canLook = value;
    }
}
