using System;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.UI;

public class BlackjackManager : MonoBehaviour
{
    //Game Buttons
    public Button DealBtn;
    public Button HitBtn;
    public Button StandBtn;
    public Button ChangeBetBtn;
    public Button BackBtn;
    public Button Add100Btn;
    public Button Add10Btn;
    public Button Minus100Btn;
    public Button Minus10Btn;

    public GameObject BlackJackCanvas;
    public GameObject BodyBettingCanvas;

    public MainCameraManager mainCameraManager;
    public GameObject Player;

    public PlayerMotor playerMotor;
    public BlackjackTable blackjackTable;

    public PlayersScript playerScript;
    public PlayersScript dealerScript;

    //HUD TextFields
    public TextMeshProUGUI dealersHandText;
    public TextMeshProUGUI playersHandText;
    public TextMeshProUGUI betsText;
    public TextMeshProUGUI cashText;
    public TextMeshProUGUI mainText;
    public TextMeshProUGUI standBtnText;

    public BodyBettingManager bodyBettingManager;

    //card hiding dealers 2nd card
    public GameObject hideCard;

    private Boolean alreadyPayed = false;
    //how much bet is

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    { 
        // ad on click listners to buttons

        DealBtn.onClick.AddListener(() => DealClicked());
        HitBtn.onClick.AddListener(() => HitClicked());
        StandBtn.onClick.AddListener(() => StandClicked());
        ChangeBetBtn.onClick.AddListener(() => ChangeBetClicked());
        Add100Btn.onClick.AddListener(() => Add100());
        Add10Btn.onClick.AddListener(() => Add10());
        Minus100Btn.onClick.AddListener(() => Minus100());
        Minus10Btn.onClick.AddListener(() => Minus10());
        BackBtn.onClick.AddListener(() => GoBack());
    }

    private void GoBack()
    {
        Player.transform.localScale = new Vector3(1, 1, 1);
        playerMotor.Teleport(new Vector3(-1.906f, 0.919f, 12.779f), Quaternion.Euler(0f, -90f, 0f));
        blackjackTable.GoBack();
    }

    void Update()
    {
        DisplayExtraBetButtons();
    }
    private void DealClicked()
    {
        if(bodyBettingManager.betAmount <= 0)
            {
            mainText.text = "Place a bet first!";
            mainText.gameObject.SetActive(true);
            return;
        }
        // Reset round, hide text, prep for new hand
        playerScript.ResetHand();
        dealerScript.ResetHand();
        // Hide deal hand score at start of deal
        dealersHandText.gameObject.SetActive(false);
        mainText.gameObject.SetActive(false);
        dealersHandText.gameObject.SetActive(false);
        GameObject.Find("CardDeck").GetComponent<DeckScript>().Shuffle();
        playerScript.StartHand();
        dealerScript.StartHand();
        // Update the scores displayed
        playersHandText.text = "Players Hand: " + playerScript.handValue.ToString();
        dealersHandText.text = "Dealers Hand: " + dealerScript.handValue.ToString();
        // Place card back on dealer card, hide card
        hideCard.GetComponent<Renderer>().enabled = true;
        // Adjust buttons visibility
        ChangeBetBtn.gameObject.SetActive(false);
        DealBtn.gameObject.SetActive(false);
        BackBtn.gameObject.SetActive(false);
        HitBtn.gameObject.SetActive(true);
        StandBtn.gameObject.SetActive(true);
        standBtnText.text = "Stand";
        // Set standard pot size
        if(playerScript.GetMoney() > bodyBettingManager.betAmount)
        {
            alreadyPayed = true;
            playerScript.AdjustMoney(-bodyBettingManager.betAmount);
        }
        cashText.text = "$" + playerScript.GetMoney().ToString();

    }

    private void HitClicked()
    {
        // Check that there is still room on the table
        if (playerScript.cardIndex <= 10)
        {
            playerScript.GetCard();
            playersHandText.text = "Players Hand: " + playerScript.handValue.ToString();
            if (playerScript.handValue > 20) RoundOver();
        }
    }

    private void StandClicked()
    {
        HitDealer();
    }

    private void HitDealer()
    {
        bool roundOver = false;
        while (dealerScript.handValue <= 16 && dealerScript.cardIndex < 10)
        {
            dealerScript.GetCard();
            dealersHandText.text = "Dealers Hand: " + dealerScript.handValue.ToString();
            if (dealerScript.handValue >= 17) {
                roundOver = true;
                RoundOver();
            } 
        }
        if (!roundOver) RoundOver();
    }

    // Check for winnner and loser, hand is over
    void RoundOver()
    {
        // Booleans (true/false) for bust and blackjack/21
        bool playerBust = playerScript.handValue > 21;
        bool dealerBust = dealerScript.handValue > 21;
        bool player21 = playerScript.handValue == 21;
        bool dealer21 = dealerScript.handValue == 21;
        // If stand has been clicked less than twice, no 21s or busts, quit function
        bool roundOver = true;
        // All bust, bets returned
        if (playerBust && dealerBust)
        {
            mainText.text = "All Bust: Bets returned";
            if (alreadyPayed)
                playerScript.AdjustMoney(bodyBettingManager.betAmount);
        }
        // if player busts, dealer didnt, or if dealer has more points, dealer wins
        else if (playerBust || (!dealerBust && dealerScript.handValue > playerScript.handValue))
        {
            mainText.text = "Dealer wins!";
            if(!alreadyPayed)
                bodyBettingManager.LoseParts();
        }
        // if dealer busts, player didnt, or player has more points, player wins
        else if (dealerBust || playerScript.handValue > dealerScript.handValue)
        {
            mainText.text = "You win!";
                playerScript.AdjustMoney(bodyBettingManager.betAmount + bodyBettingManager.betAmount);
        }
        //Check for tie, return bets
        else if (playerScript.handValue == dealerScript.handValue)
        {
            mainText.text = "Push: Bets returned";
            if (alreadyPayed)
                playerScript.AdjustMoney(bodyBettingManager.betAmount);
        }
        else
        {
            roundOver = false;
        }
        // Set ui up for next move / hand / turn
        if (roundOver)
        {
            HitBtn.gameObject.SetActive(false);
            StandBtn.gameObject.SetActive(false);
            DealBtn.gameObject.SetActive(true);
            BackBtn.gameObject.SetActive(false);
            ChangeBetBtn.gameObject.SetActive(true);
            mainText.gameObject.SetActive(true);
            dealersHandText.gameObject.SetActive(true);
            hideCard.GetComponent<Renderer>().enabled = false;
            cashText.text = "$" + playerScript.GetMoney().ToString();
        }
    }

    // Add money to pot if bet clicked
    void ChangeBetClicked()
    {
        Player.transform.localScale = new Vector3(4, 4, 4);
        playerMotor.Teleport(new Vector3(54.3f, 0, 22.77f), Quaternion.Euler(0f, -90f, 0f));
        mainCameraManager.Teleport(new Vector3(48.23747f, 4.47087f, 22.77152f), Quaternion.Euler(6.875f, 90.4f, 0.105f));
        BlackJackCanvas.SetActive(false);
        BodyBettingCanvas.SetActive(true);
    }

    private void DisplayExtraBetButtons()
    {
        int money = playerScript.GetMoney();
        Boolean addhundred = money >= 100 && bodyBettingManager.betAmount+100 <= money;
        Boolean addten = money >= 10 && bodyBettingManager.betAmount + 10 <= money;
        Boolean minushundred = money >= 100 && bodyBettingManager.betAmount - 100 >= 0;
        Boolean minusten = money >= 10 && bodyBettingManager.betAmount - 10 >= 0;

        Add100Btn.gameObject.SetActive(addhundred);
        Add10Btn.gameObject.SetActive(addten);
        Minus100Btn.gameObject.SetActive(minushundred);
        Minus10Btn.gameObject.SetActive(minusten);

        betsText.text = "Bet: $" + bodyBettingManager.betAmount.ToString();
    }

    void Add100()
    {
        bodyBettingManager.betAmount += 100;
    }
    void Add10()
    {
        bodyBettingManager.betAmount += 10;
    }
    void Minus100()
    {
        bodyBettingManager.betAmount -= 100;
    }
    void Minus10()
    {
        bodyBettingManager.betAmount -= 10;
    }
}
