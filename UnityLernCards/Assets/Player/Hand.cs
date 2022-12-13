using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Hand : MonoBehaviour
{
    [SerializeField] private Deck _trashDeck;
    [SerializeField] private List<Card> _cards = new();
    [SerializeField] private UnityEvent<Card> _cardRemoved = new();

    public List<Card> Cards => _cards;
    public UnityEvent<Card> CardRemoved => _cardRemoved;

    public void RemoveCard(Card card)
    {
        _cards.Remove(card);
        _trashDeck.AddCard(card);
        _cardRemoved.Invoke(card);
    }

    public void RemoveDeadCards()
    {
        foreach (Card card in new List<Card>(_cards))
        {
            if (card.Health <= 0)
            {
                RemoveCard(card);
            }
        }
    }
}
