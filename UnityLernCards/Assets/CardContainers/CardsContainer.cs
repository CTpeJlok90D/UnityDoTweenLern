using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CardsContainer : MonoBehaviour
{
    [SerializeField] private UnityEvent<Card> _cardAdded;
    [SerializeField] private UnityEvent<Card> _cardRemoved;

    public UnityEvent<Card> CardAdded => _cardAdded;
    public UnityEvent<Card> CardRemoved => _cardRemoved;
    public List<Card> Cards
    {
        get
        {
            List<Card> cards = new();
            foreach (Transform cardTransform in transform)
            {
                if (cardTransform.TryGetComponent(out Card card))
                {
                    cards.Add(card);
                }
            }
            return cards;
        }
    }

    public void AddCard(Card card)
    {
        card.transform.SetParent(transform);
        _cardAdded.Invoke(card);
    }

    public void RemoveCard(Card card)
    {
        card.transform.SetParent(null);
        _cardRemoved.Invoke(card);
    }

    public void RemoveDeadCards()
    {
        foreach (Card card in Cards)
        {
            if (card.Health <= 0)
            {
                RemoveCard(card);
            }
        }
    }
}
