using DG.Tweening;
using UnityEngine;

public class Deckview : MonoBehaviour
{
    [SerializeField] private Deck _deck;
    [SerializeField] private float _cardMoveAnimationSpeed = 0.5f;

    private void OnEnable()
    {
        _deck.CardAdded.AddListener(OnCardAdd);
    }

    private void Start()
    {
        for (int i = 0; i < _deck.Cards.Count - 1; i++)
        {
            _deck.Cards[i].gameObject.SetActive(false);
        }
    }

    private void OnCardAdd(Card card)
    {
        if (_deck.Cards.Count > 1) 
        {
            _deck.Cards[^2].gameObject.SetActive(false);
        }
        card.transform.DOMove(transform.position, _cardMoveAnimationSpeed);
    }
}