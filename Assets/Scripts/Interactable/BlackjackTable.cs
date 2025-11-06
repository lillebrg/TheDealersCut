using UnityEngine;


public class BlackjackTable : Interactable
{
    public GameObject PlayerCanvas;
    public GameObject BlackjackCanvas;
    public GameObject PlayerCamera;
    public GameObject MainCamera;
    public PlayerMotor playerMotor;
    public PlayerLook playerLook;

    public void GoBack()
    {
        playerMotor.SetCanMove(true);
        playerLook.SetCanLook(true);
        PlayerCamera.SetActive(true);
        PlayerCanvas.SetActive(true);
        BlackjackCanvas.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;

    }

    protected override void Interact()
    {

        playerMotor.SetCanMove(false);
        playerLook.SetCanLook(false);
        PlayerCamera.SetActive(false);
        PlayerCanvas.SetActive(false);
        BlackjackCanvas.SetActive(true);
        MainCamera.SetActive(true);
 
        Cursor.lockState = CursorLockMode.None;
    }
}
