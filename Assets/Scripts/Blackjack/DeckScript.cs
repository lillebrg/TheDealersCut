using UnityEngine;

public class DeckScript : MonoBehaviour
{
    public Material[] cardFaces;
    int[] cardValues = new int[53];
    int currentCardIndex = 0;

    void Start()
    {
        GetCardValues();
    }

    void GetCardValues()
    {
        int num = 0;
        // Loop to assign values to the cards
        for (int i = 0; i < cardValues.Length; i++)
        {
            num = i;
            // Count up to the amout of cards, 52
            num %= 13;
            // if there is a remainder after x/13, then remainder
            // is used as the value, unless over 10, the use 10
            if (num > 10 || num == 0)
            {
                num = 10;
            }
            cardValues[i] = num++;
        }
    }

    public void Shuffle()
    {
        // Standard array data swapping technique
        for (int i = cardFaces.Length - 1; i > 0; --i)
        {
            int j = Mathf.FloorToInt(Random.Range(0.0f, 1.0f) * cardFaces.Length - 1) + 1;
            Material face = cardFaces[i];
            cardFaces[i] = cardFaces[j];
            cardFaces[j] = face;

            int value = cardValues[i];
            cardValues[i] = cardValues[j];
            cardValues[j] = value;
        }
        currentCardIndex = 1;
    }

    public int DealCard(CardScript cardScript)
    {
        cardScript.SetMaterial(cardFaces[currentCardIndex]);
        cardScript.SetCardValue(cardValues[currentCardIndex++]);
        return cardScript.GetCardValue();
    }

    public Material GetCardBack()
    {
        return cardFaces[0];
    }
}
