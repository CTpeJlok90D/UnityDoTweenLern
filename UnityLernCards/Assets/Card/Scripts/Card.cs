using UnityEngine;
using UnityEngine.Events;

public class Card : MonoBehaviour
{
    [SerializeField] private int _attackStrench;
    [SerializeField] private int _mana;
    [SerializeField] private int _health;
    [SerializeField] private UnityEvent<int,int> _healthChanged = new();
    [SerializeField] private UnityEvent<int,int> _manaChanged = new();
    [SerializeField] private UnityEvent<int,int> _attackChanged = new();
    [Header("Optionality")]
    [SerializeField] private CardInfo _cardInfo;

    public Card Init(CardInfo info)
    {
        Health = info.Health;
        Mana = info.Mana;
        Damage = info.Damage;
        _cardInfo = info;
        return this;
    }

    #region Propertys
    public int Health
    {
        get
        {
            return _health;
        }
        set
        {
            int oldValue = _health;
            _health = value;
            _healthChanged.Invoke(oldValue, _health);
        }
    }
    public int Mana
    {
        get 
        {
            return _mana; 
        }
        set
        {
            int oldValue = _mana;
            _mana = value;
            _manaChanged.Invoke(oldValue, _mana);
        }
    }
    public int Damage
    {
        get
        {
            return _attackStrench;
        }
        set
        {
            int oldValue = _attackStrench;
            _attackStrench = value;
            _attackChanged.Invoke(oldValue,_attackStrench);
        }
    }
    public UnityEvent<int, int> HealthChanged => _healthChanged;
    public UnityEvent<int, int> ManaChanged => _manaChanged;
    public UnityEvent<int, int> AttackStrenchChanged => _attackChanged;
    #endregion
    public CardInfo info => _cardInfo;

    private void Awake()
    {
        if (_cardInfo != null)
        {
            Init(_cardInfo);
        }
    }
}
