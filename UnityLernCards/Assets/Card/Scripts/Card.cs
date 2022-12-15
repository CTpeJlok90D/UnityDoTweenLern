using DG.Tweening;
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
    [SerializeField] private UnityEvent _moveStarted = new();
    [SerializeField] private UnityEvent _moveEnded = new();
    [SerializeField] private UnityEvent _drugStarted = new();
    [SerializeField] private UnityEvent _drugEnded = new();
    [SerializeField] private Collider2D _collider;
    [Header("Optionality")]
    [SerializeField] private CardInfo _cardInfo;

    private static Card _lastMouseOnCard;

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
    public static Card LastMouseOnCard => _lastMouseOnCard;
    public Collider2D Collider => _collider;
    public UnityEvent MoveStarted => _moveStarted;
    public UnityEvent DrugEnded => _moveEnded;
    public UnityEvent DrugStarted => _drugStarted;
    #endregion
    public CardInfo info => _cardInfo;

    private void Awake()
    {
        if (_cardInfo != null)
        {
            Init(_cardInfo);
        }
    }

    private void OnEnable()
    {
        _moveStarted.AddListener(OnMoveStart);
        _moveEnded.AddListener(OnMoveEnd);
    }

    private void OnDisable()
    {
        _moveStarted.RemoveListener(OnMoveStart);
        _moveEnded.RemoveListener(OnMoveStart);
    }

    private void OnMoveStart()
    {
        _collider.enabled = false;
    }

    private void OnMoveEnd()
    {
        _collider.enabled = true;
    }

    private void OnMouseEnter()
    {
        _lastMouseOnCard = this;
    }

    private void OnMouseExit() 
    {
        _lastMouseOnCard = null;
    }

    public void DOMove(Vector3 position, float duration) 
    {
        _moveStarted.Invoke();
        transform.DOMove(position, duration).OnComplete(_moveEnded.Invoke);
    }
}
