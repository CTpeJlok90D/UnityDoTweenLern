using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class HandView : MonoBehaviour
{
    [SerializeField] private CardsContainer _hand;
    [SerializeField] private Vector3 _circleCenterOffcet;
    [SerializeField] private float _circleRadius;
    [SerializeField] private float _zCardsOffcet = -0.1f;
    [SerializeField] private float _animationSpeed = 0.3f;
    private float _maxXCardPosition;
    private float _minXCardPosition;
    private Vector2 _circleCenterPosition => transform.position + _circleCenterOffcet;

    private void Start()
    {
        UpdateCardPositions();
    }

    public void UpdateCardPositions()
    {
        List<Card> cards = _hand.Cards;
        int cardsCount = _hand.Cards.Count;
        float stepSize = (_maxXCardPosition * 2) / cardsCount;
        float currentCord = _minXCardPosition + stepSize/2;
        float zPos = _zCardsOffcet;
        foreach (Card card in cards)
        {
            PlaceCard(card, currentCord, zPos);
            currentCord += stepSize;
            zPos += _zCardsOffcet;
        }
    }

    private void PlaceCard(Card card, float x, float y)
    {
        Vector3 finishPoint = new Vector3(_circleCenterPosition.x + x, _circleCenterPosition.y + GetYByX(x), y);
        Vector3 rotate = finishPoint - (Vector3)_circleCenterPosition;
        card.transform.up = new Vector3(rotate.x, rotate.y, 0);
        card.DOMove(finishPoint, _animationSpeed);
    }

    private float GetYByX(float X)
    {
        return Mathf.Sqrt(Mathf.Pow(_circleRadius, 2) - Mathf.Pow(X, 2));
    }

    private void OnEnable()
    {
        _hand.CardRemoved.AddListener(OnCardRemove);
        _hand.CardAdded.AddListener(OnCardAdd);
    }

    private void OnCardRemove(Card card)
    {
        UpdateCardPositions();
    }
    private void OnCardAdd(Card card)
    {
        UpdateCardPositions();
    }


#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        float step = 0.1f;
        Gizmos.color = Color.red;
        Gizmos.DrawLine(new Vector3(transform.position.x + _maxXCardPosition, transform.position.y, transform.position.z), new Vector3(transform.position.x + _minXCardPosition, transform.position.y, transform.position.z));
        Gizmos.color = Color.cyan;
        for (float i = _minXCardPosition; i < _maxXCardPosition - step; i += step)
        {
            Gizmos.DrawLine(new Vector2(i, GetYByX(i)) + _circleCenterPosition, new Vector2(i + step, GetYByX(i + step)) + _circleCenterPosition);
        }
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(new Vector3(_maxXCardPosition + transform.position.x, transform.position.y, transform.position.z), step);
        Gizmos.DrawSphere(new Vector3(-_maxXCardPosition + transform.position.x, transform.position.y, transform.position.z), step);
    }
    private void OnValidate()
    {
        _maxXCardPosition = GetYByX(_circleCenterOffcet.y);
        _minXCardPosition = -_maxXCardPosition;
    }
#endif
}