using System.Collections.Generic;
using DG.Tweening;
using TMPro.SpriteAssetUtilities;
using UnityEngine;

public class HandView : MonoBehaviour
{
    [SerializeField] private Hand _hand;
    [SerializeField] private Vector3 _circleCenterOffcet;
    [SerializeField] private float _circleRadius;
    [SerializeField] private float _yOffcet = 0.1f;
    [SerializeField] private float _animationSpeed = 0.3f;
    private float _maxXCardPosition;
    private float _minXCardPosition;
    private Vector2 _circleCenterPosition => transform.position + _circleCenterOffcet;

    private void Start()
    {
        PlaceCards();
    }

    public void PlaceCards()
    {
        List<Card> cards = _hand.Cards;
        int cardsCount = _hand.Cards.Count;
        float stepSize = (_maxXCardPosition * 2) / cardsCount;
        float currentCord = _minXCardPosition + stepSize/2;
        float yPos = 0;
        foreach (Card card in cards)
        {
            PlaceCard(card, currentCord, yPos);
            currentCord += stepSize;
            yPos += _yOffcet;
        }
    }

    private void PlaceCard(Card card, float x, float y)
    {
        card.transform.DOMove(new Vector3(_circleCenterPosition.x + x, _circleCenterPosition.y + GetYByX(x), y), _animationSpeed);
        card.transform.up = -_circleCenterPosition*10;
    }

    private float GetYByX(float X)
    {
        return Mathf.Sqrt(Mathf.Pow(_circleRadius, 2) - Mathf.Pow(X, 2));
    }

    private void OnEnable()
    {
        _hand.CardRemoved.AddListener(OnCardRemove);
    }

    private void OnCardRemove(Card card)
    {
        PlaceCards();
    }


#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(_circleCenterOffcet + transform.position, _circleRadius);
        Gizmos.color = Color.red;
        Gizmos.DrawLine(new Vector3(transform.position.x + _maxXCardPosition, transform.position.y, transform.position.z), new Vector3(transform.position.x + _minXCardPosition, transform.position.y, transform.position.z));
        Gizmos.DrawSphere(new Vector3(_maxXCardPosition + transform.position.x, transform.position.y, transform.position.z), 0.1f);
        Gizmos.DrawSphere(new Vector3(-_maxXCardPosition + transform.position.x, transform.position.y, transform.position.z), 0.1f);
    }
    private void OnValidate()
    {
        _maxXCardPosition = GetYByX(_circleCenterOffcet.y);
        _minXCardPosition = -_maxXCardPosition;
    }
#endif
}