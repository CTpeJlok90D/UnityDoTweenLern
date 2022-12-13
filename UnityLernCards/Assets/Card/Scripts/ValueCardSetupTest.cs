using UnityEngine;

public class ValueCardSetupTest : MonoBehaviour
{
    [SerializeField] private Hand _hand;
    [SerializeField] private Vector2Int _manaDiapasone = new Vector2Int(-2, 9);
    [SerializeField] private Vector2Int _healthDiapasone = new Vector2Int(-2, 9);
    [SerializeField] private Vector2Int _damageDiapasone = new Vector2Int(-2, 9);

    private void OnEnable()
    {
        foreach (Card card in _hand.Cards)
        {
            card.Mana = Random.Range(_manaDiapasone[0], _manaDiapasone[1] + 1);
            card.Health = Random.Range(_healthDiapasone[0], _healthDiapasone[1] + 1);
            card.Damage = Random.Range(_damageDiapasone[0], _damageDiapasone[1] + 1);
        }
        _hand.RemoveDeadCards();
    }
}