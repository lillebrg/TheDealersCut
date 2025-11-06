using System.Collections.Generic;
using UnityEngine;

public class PlayersScript : MonoBehaviour
{
    // This script will manage BOTH players (dealer & player) data in the blackjack game

    public CardScript cardScript;
    public DeckScript deckScript;

    public int handValue = 0;

    public int money = 0;

    //Betting money
    //private int playerMoney = 1000;

    //Array of card objects on table
    public GameObject[] hand;

    //index of next card to be turned over
    public int cardIndex = 0;

    //tracking aces for 1 to 11 convertions
    List<CardScript> aceList = new List<CardScript>();
    public void StartHand()
    {
        GetCard();
        GetCard();
    }

    // Add a hand to the players hands
    public int GetCard()
    {
        //get card
        int cardValue = deckScript.DealCard(hand[cardIndex].GetComponent<CardScript>());
        //show card on game screen
        hand[cardIndex].GetComponent<Renderer>().enabled = true;
        //add card value to running toital of the hand
        handValue += cardValue;
        //check if ace
        if (cardValue == 1)
        {
            aceList.Add(hand[cardIndex].GetComponent<CardScript>());
        }
        //chewck for ace
        AceCheck();
        cardIndex++;
        return handValue;
 
    }

    // Search for needed ace conversions, 1 to 11 or vice versa
    public void AceCheck()
    {
        // for each ace in the lsit check
        foreach (CardScript ace in aceList)
        {
            if (handValue + 10 < 22 && ace.GetCardValue() == 1)
            {
                // if converting, adjust card object value and hand
                ace.SetCardValue(11);
                handValue += 10;
            }
            else if (handValue > 21 && ace.GetCardValue() == 11)
            {
                // if converting, adjust gameobject value and hand value
                ace.SetCardValue(1);
                handValue -= 10;
            }
        }
    }

    // Add or subtract from money, for bets
    public void AdjustMoney(int amount)
    {
        money += amount;
    }

    // Output players current money amount
    public int GetMoney()
    {
        return money;
    }

    // Hides all cards, resets the needed variables
    public void ResetHand()
    {
        for (int i = 0; i < hand.Length; i++)
        {
            hand[i].GetComponent<CardScript>().ResetCard();
            hand[i].GetComponent<Renderer>().enabled = false;
        }
        cardIndex = 0;
        handValue = 0;
        aceList = new List<CardScript>();
    }
}
