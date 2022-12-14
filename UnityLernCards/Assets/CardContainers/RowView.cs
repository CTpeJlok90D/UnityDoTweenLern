using DG.Tweening;
using UnityEngine;

public class RowView : MonoBehaviour
{
    [SerializeField] private CardsContainer _container;
    [SerializeField] private CardsContainer _trashDeck;
    [SerializeField] private float _lenght = 3f;
    [SerializeField] private float _spaceBetweenCards = 0.5f;
    [SerializeField] private float _animationDeturation = 0.1f;

    private void OnEnable()
    {
        _container.CardAdded.AddListener(OnCardAdd);
        _container.CardRemoved.AddListener(OnCardRemove);
    }  
    private void OnDisable()
    {
        _container.CardAdded.RemoveListener(OnCardAdd);
        _container.CardRemoved.RemoveListener(OnCardRemove);
    }

    private void OnCardAdd(Card card)
    {
        UpdateCardPositions();
    }

    private void OnCardRemove(Card card)
    {
        UpdateCardPositions();
        _trashDeck.AddCard(card);
    }

    private void Start()
    {
        UpdateCardPositions();
    }

    private void UpdateCardPositions()
    {
        float step = _lenght / _container.Cards.Count + _spaceBetweenCards;
        float currentX = (_lenght - step) / -2;
        foreach (Card card in _container.Cards)
        {
            card.transform.DOMove(new Vector3(transform.position.x + currentX, transform.position.y), _animationDeturation);
            currentX += step;
        }
    }

#if UNITY_EDITOR
    public void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(transform.position + new Vector3(_lenght / 2, 0, 0), transform.position + new Vector3(-_lenght / 2, 0, 0));
    }
#endif
}