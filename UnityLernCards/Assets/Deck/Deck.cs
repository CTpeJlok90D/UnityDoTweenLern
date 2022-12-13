using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Deck : MonoBehaviour
{
    [SerializeField] private List<Card> _cards;
    [SerializeField] private UnityEvent<Card> _cardAdded;

    public UnityEvent<Card> CardAdded => _cardAdded;
    public List<Card> Cards => new(_cards);

    public void AddCard(Card card)
    {
        _cards.Add(card);
        _cardAdded.Invoke(card);
    }
}
