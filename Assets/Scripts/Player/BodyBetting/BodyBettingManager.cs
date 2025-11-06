using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// BodyBettingManager.cs
public class BodyBettingManager : MonoBehaviour
{
    public Button ViewFrontBtn;
    public Button ViewRightBtn;
    public Button ViewLeftBtn;
    public Button ConfirmBtn;

    public int betAmount = 0;

    public GameObject BlackJackCanvas;
    public GameObject BodyBettingCanvas;
    public PlayerHealth playerHealth;

    public MainCameraManager mainCameraManager;

    public List<BodyPart> selectedParts;

    public TextMeshProUGUI betText; // UI text to show total bet
    public TextMeshProUGUI blackjackBetText;
    

    void Start()
    {
        ViewFrontBtn.onClick.AddListener(() => FrontSideClicked());
        ViewRightBtn.onClick.AddListener(() => RightSideClicked());
        ViewLeftBtn.onClick.AddListener(() => LeftSideClicked());
        ConfirmBtn.onClick.AddListener(() => ConfirmClicked());
        UpdateUI();
    }

    private void FrontSideClicked() { mainCameraManager.Teleport(new Vector3(48.23747f, 4.47087f, 22.77152f), Quaternion.Euler(6.875f, 90.4f, 0.105f)); }
    private void RightSideClicked() { mainCameraManager.Teleport(new Vector3(54.84831f, 3.34318f, 25.94207f), Quaternion.Euler(8.078f, 167.752f, 0.105f)); }
    private void LeftSideClicked() { mainCameraManager.Teleport(new Vector3(54.8731f, 3.166935f, 19.80505f), Quaternion.Euler(2.406f, 11.496f, 0.104f)); }
    private void ConfirmClicked() { BlackJackCanvas.SetActive(true); BodyBettingCanvas.SetActive(false); mainCameraManager.Teleport(new Vector3(-17.08963f, 2.877156f, 13.12133f), Quaternion.Euler(62.91f, -89.707f, 0.14f)); }

    public void SelectOrDeselectPart(BodyPart part)
    {
        if (!part.isVisible) return;

        if (selectedParts.Contains(part))
        {
            selectedParts.Remove(part);
            part.Deselect();
        }
        else
        {
            selectedParts.Add(part);
            part.ToggleSelection();
        }

        UpdateUI();
    }

    public void LoseParts()
    {
        playerHealth.HealthLost();
        for (int i = selectedParts.Count - 1; i >= 0; i--)
        {
            var part = selectedParts[i];
            part.LosePart();
            selectedParts.RemoveAt(i);
        }
        UpdateUI();
    }


    private void UpdateUI()
    {
        int bet = 0;
        foreach (var part in selectedParts)
        {
            bet += part.price;
        }
        betAmount = bet;

        betText.text = $"Bet: ${betAmount}";
        blackjackBetText.text = $"Bet: ${betAmount}";
    }
}
