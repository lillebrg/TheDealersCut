using UnityEngine;

public class CardScript : MonoBehaviour
{
    // Value of card, 2 of clubs = 2, Ace of spades = 11, etc.
    private int cardValue = 0;

    public int GetCardValue()
    {
        return cardValue;
    }

    public void SetCardValue(int value)
    {
        cardValue = value;
    }

    public string GetMaterialName()
    {
        return GetComponent<Renderer>().material.name;
    }

    public void SetMaterial(Material newMaterial)
    {
        gameObject.GetComponent<Renderer>().material = newMaterial;
    }

    public void ResetCard()
    {
        Material back = GameObject.Find("CardDeck").GetComponent<DeckScript>().GetCardBack();
        gameObject.GetComponent<Renderer>().material = back;
        cardValue = 0;
    }
}
