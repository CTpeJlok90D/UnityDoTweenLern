public class TrashDeckContainer : CardsContainer
{
    private void Awake()
    {
        CardAdded.AddListener(OnCardAdd);
    }
    
    private void OnCardAdd(Card card)
    {
        card.CanDrug = false;
        card.Collider.enabled = false;
    }
}