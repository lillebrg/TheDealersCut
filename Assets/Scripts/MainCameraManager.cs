using UnityEngine;

public class MainCameraManager : MonoBehaviour
{
    public void Teleport(Vector3 position, Quaternion rotation)
    {
        transform.localPosition = position;
        transform.localRotation = rotation;
    }
}
