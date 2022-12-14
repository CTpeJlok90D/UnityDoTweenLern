using UnityEngine;

public class Hand : CardsContainer
{
    [SerializeField] private CardsContainer _trashDeck;

    public void OnEnable()
    {
        CardRemoved.AddListener(OnCardRemove);
    }

    public void OnCardRemove(Card card)
    {
        _trashDeck.AddCard(card);
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
