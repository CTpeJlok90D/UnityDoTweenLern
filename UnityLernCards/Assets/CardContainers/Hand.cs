using UnityEngine;

public class Hand : CardsContainer
{
    [SerializeField] private CardsContainer _trashDeck;

    public void OnEnable()
    {
        CardRemoved.AddListener(OnCardRemove);
    } 
    public void OnDisable()
    {
        CardRemoved.RemoveListener(OnCardRemove);
    }

    public void OnCardRemove(Card card)
    {
        _trashDeck.AddCard(card);
    }
}
