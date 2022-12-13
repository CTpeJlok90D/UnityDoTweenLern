using UnityEngine;
using UnityEngine.Events;

public class Card : MonoBehaviour
{
    [SerializeField] private int _health;
    [SerializeField] private int _mana;
    [SerializeField] private int _attackStrench;
    [SerializeField] private UnityEvent<int> _healthChanged = new();
    [SerializeField] private UnityEvent<int> _manaChanged = new();
    [SerializeField] private UnityEvent<int> _attackChanged = new();
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
            _health = value;
            _healthChanged.Invoke(_health);
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
            _mana = value;
            _manaChanged.Invoke(_mana);
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
            _attackStrench = value;
            _attackChanged.Invoke(_attackStrench);
        }
    }
    public UnityEvent<int> HealthChanged => _healthChanged;
    public UnityEvent<int> ManaChanged => _manaChanged;
    public UnityEvent<int> AttackStrenchChanged => _attackChanged;
    #endregion
    public CardInfo info => _cardInfo;

    private void Awake()
    {
        if (_cardInfo != null)
        {
            Init(_cardInfo);
        }
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        _healthChanged.Invoke(Health);
        _manaChanged.Invoke(Mana);
        _attackChanged.Invoke(Damage);
    }
#endif
}
