using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryResult : MonoBehaviour
{
    public Card_SO card_SO;
    public CardResult card1;
    public CardResult card2;
    public CardResult card3;
    void OnEnable()
    {
        EventHandler.CloseCardResult += CloseCard;
    }
    void OnDisable()
    {
        EventHandler.CloseCardResult -= CloseCard;
    }
    public void SetCard()
    {
        OpenCard();
        CardData cardData = card_SO.cardDates[Random.Range(0, card_SO.cardDates.Count)];
        card1.Description = cardData.Description;
        card1.SetImage(cardData.sprite);
        card1.ID = cardData.ID;

        cardData = card_SO.cardDates[Random.Range(0, card_SO.cardDates.Count)];
        card2.Description = cardData.Description;
        card2.SetImage(cardData.sprite);
        card2.ID = cardData.ID;

        cardData = card_SO.cardDates[Random.Range(0, card_SO.cardDates.Count)];
        card3.Description = cardData.Description;
        card3.SetImage(cardData.sprite);
        card3.ID = cardData.ID;
    }
    public void CloseCard()
    {
        card1.gameObject.SetActive(false);
        card2.gameObject.SetActive(false);
        card3.gameObject.SetActive(false);
    }
    public void OpenCard()
    {
        card1.gameObject.SetActive(true);
        card2.gameObject.SetActive(true);
        card3.gameObject.SetActive(true);
    }
}
