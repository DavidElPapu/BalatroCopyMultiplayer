using System.Collections.Generic;
using UnityEngine;
using Mirror;
using System;

/*
	Documentation: https://mirror-networking.gitbook.io/docs/guides/networkbehaviour
	API Reference: https://mirror-networking.com/docs/api/Mirror.NetworkBehaviour.html
*/

public class CardCreator : NetworkBehaviour
{
    public static CardCreator singleton;

    [SerializeField] private GameObject baseCardPrefab;
    [SerializeField] private Sprite[] clubSuitRankSprites = new Sprite[14];
    [SerializeField] private Sprite[] diamondSuitRankSprites = new Sprite[14];
    [SerializeField] private Sprite[] heartSuitRankSprites = new Sprite[14];
    [SerializeField] private Sprite[] spadeSuitRankSprites = new Sprite[14];
    [SerializeField] private Sprite[] enhancementSprites = new Sprite[9];
    [SerializeField] private Sprite[] editionSprites = new Sprite[5];
    [SerializeField] private Sprite[] sealSprites = new Sprite[5];

    private void Awake()
    {
        if (singleton != null && singleton != this) { Destroy(this); } else { singleton = this; }
        if (baseCardPrefab == null) { Debug.LogError("Missing Card Prefab"); }
    }

    public GameObject GetNewCard(CardData data)
    {
        GameObject newCard = Instantiate(baseCardPrefab);

        newCard.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = enhancementSprites[((int)data.enhancement)];
        if (data.enhancement == CardData.CardEnhancement.Stone)
            newCard.transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = clubSuitRankSprites[0];
        else
        {
            switch (data.suit)
            {
                case CardData.CardSuit.Club:
                    newCard.transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = clubSuitRankSprites[data.rank];
                    break;
                case CardData.CardSuit.Diamond:
                    newCard.transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = diamondSuitRankSprites[data.rank];
                    break;
                case CardData.CardSuit.Heart:
                    newCard.transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = heartSuitRankSprites[data.rank];
                    break;
                case CardData.CardSuit.Spade:
                    newCard.transform.GetChild(1).GetComponent<SpriteRenderer>().sprite = spadeSuitRankSprites[data.rank];
                    break;
                default:
                    Debug.LogError("Not valid suit");
                    break;
            }
        }
        newCard.transform.GetChild(2).GetComponent<SpriteRenderer>().sprite = editionSprites[((int)data.edition)];
        newCard.transform.GetChild(3).GetComponent<SpriteRenderer>().sprite = sealSprites[((int)data.edition)];
        return null;
    }

    void Start()
    {

    }
}

[Serializable]
public struct CardData
{
    public int rank;
    public enum CardSuit
    {
        Club,
        Diamond,
        Heart,
        Spade
    }
    public CardSuit suit;
    public enum CardEnhancement
    {
        None,
        Bonus,
        Mult,
        Wild,
        Glass,
        Steel,
        Stone,
        Gold,
        Lucky
    }
    public CardEnhancement enhancement;
    public enum CardEdition
    {
        Base,
        Foil,
        Holographic,
        Polychrome,
        Negative
    }
    public CardEdition edition;
    public enum CardSeal
    {
        None,
        Gold,
        Red,
        Blue,
        Purple
    }
    public CardSeal seal;

    public CardData(int rank, CardSuit suit, CardEnhancement enhancement, CardEdition edition, CardSeal seal)
    {
        this.rank = rank;
        this.suit = suit;
        this.enhancement = enhancement;
        this.edition = edition;
        this.seal = seal;
    }
}
