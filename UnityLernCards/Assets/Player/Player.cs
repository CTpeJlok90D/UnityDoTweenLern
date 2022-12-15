using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] private CardsContainer _hand;
    [SerializeField] private float _drugedCardZPosotion = 1;

    private Card _dragedCard;

    public void OnMouseClick(InputAction.CallbackContext context)
    {    
        if (context.started)
        {
            StartDrugCard(Card.LastMouseOnCard);
            return;
        }
        if (context.canceled)
        {
            StopDrugCard();
        }
    }

    private void StartDrugCard(Card card)
    {
        if (card == null)
        {
            return;
        }
        _dragedCard = card;
        _dragedCard.transform.rotation = Quaternion.identity;
        _dragedCard.Collider.enabled = false;
        if (_dragedCard.transform.parent.TryGetComponent(out CardsContainer dragedCardContainer))
        {
            dragedCardContainer.RemoveCard(card);
        }
        _dragedCard.DrugStarted.Invoke();
        StartCoroutine(DrugCorrutine());
    }

    private void StopDrugCard()
    {
        if (_dragedCard == null) 
        {
            return;
        }
        _dragedCard.Collider.enabled = true;
        if (CardsContainer.CardContainerMouseOn != null)
        {
            CardsContainer.CardContainerMouseOn.AddCard(_dragedCard);
        }
        else
        {
            _hand.AddCard(_dragedCard);
        }
        _dragedCard.DrugEnded.Invoke();
        _dragedCard = null;
    }

    private IEnumerator DrugCorrutine()
    {
        while (_dragedCard != null)
        {
            Vector2 cursorPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 newCardPosition = new Vector3(cursorPosition.x, cursorPosition.y, _drugedCardZPosotion);
            _dragedCard.transform.position = newCardPosition;
            yield return null;
        }
    }
}