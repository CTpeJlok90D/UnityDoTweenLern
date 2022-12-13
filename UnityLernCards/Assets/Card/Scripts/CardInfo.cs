using UnityEngine;

[CreateAssetMenu]
public class CardInfo : ScriptableObject
{
    [SerializeField] private string _name;
    [SerializeField] private string _description;
    [SerializeField] private Sprite _picrure;
    [SerializeField] private int _attackStrench;
    [SerializeField] private int _health;
    [SerializeField] private int _mana;

    public string Name => _name;
    public string Description => _description;
    public Sprite Picture => _picrure;
    public int Damage => _attackStrench;
    public int Health => _health;
    public int Mana => _mana;
}
