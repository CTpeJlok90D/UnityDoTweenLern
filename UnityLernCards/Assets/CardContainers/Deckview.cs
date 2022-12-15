using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;

public class Deckview : MonoBehaviour
{
    [SerializeField] private CardsContainer _deck;
    [SerializeField] private float _cardMoveAnimationDituration = 0.5f;
    [SerializeField] private int _cardLoadCount = 2;

    private const float _distanceBetweenCards = 0.1f;

    private void OnEnable()
    {
        _deck.CardAdded.AddListener(OnCardAdd);
    }
    private void OnDisable()
    {
        _deck.CardAdded.RemoveListener(OnCardAdd);
    }

    private void ArrangeCards()
    {
        for (int i = 0; i < _deck.Cards.Count; i++)
        {
            _deck.Cards[i].transform.DOKill();
            _deck.Cards[i].transform.position = transform.position + new Vector3(0, 0, _distanceBetweenCards) * (i + 1);
            _deck.Cards[i].transform.up = Vector3.up;
        }
        if (_deck.Cards.Count < _cardLoadCount)
        {
            return;
        }
        foreach (Card card in _deck.Cards)
        {
            card.gameObject.SetActive(false);
        }
        for (int i = 1; i <= _cardLoadCount; i++)
        {
            _deck.Cards[^i].gameObject.SetActive(true);
        }
    }

    private void OnCardAdd(Card card)
    {
        Vector3 oldPosition = card.transform.position;
        ArrangeCards();
        card.transform.position = oldPosition;
        card.transform.DOMove(transform.position, _cardMoveAnimationDituration);
    }
}