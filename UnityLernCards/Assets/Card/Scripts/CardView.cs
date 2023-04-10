using DG.Tweening;
using TMPro;
using UnityEngine;

public class CardView : MonoBehaviour
{
    [SerializeField] private Card _card;
    [SerializeField] private TMP_Text _nameField;
    [SerializeField] private TMP_Text _descriptionField;
    [SerializeField] private TMP_Text _manaField;
    [SerializeField] private TMP_Text _healthField;
    [SerializeField] private TMP_Text _attackField;
    [SerializeField] private SpriteRenderer _imagePlace;
    [SerializeField] private SpriteRenderer _frameSpriteRenderer;
    [Header("Animations")]
    [SerializeField] private float _onMouseEnterScale = 1.3f;
    [SerializeField] private float _onMouseEnterScaleRiseTime = 0.15f;
    [SerializeField] private Vector3 _onMouseEnterPositionOffcet = new Vector3(0,0,-1);
    [SerializeField] private Color _badCharacteristicColor = Color.red;
    [SerializeField] private Color _goodCharacteristicColor = Color.green;
    [SerializeField] private Color _standartharacteristicColor = Color.white;
    [SerializeField] private Color _changedCharactericticColor = Color.cyan;
    [SerializeField] private Color _drugedColor = Color.yellow;
    [SerializeField] private float _colorChangeDituration = 0.5f;

    private float _standartScale;
    private Color _standartFrameColor;

    private void Awake()
    {
        _standartFrameColor = _frameSpriteRenderer.color;
        _standartScale = transform.localScale.x;
    }

    private void OnEnable()
    {
        _card.HealthChanged.AddListener(UpdateHealthView);
        _card.AttackStrenchChanged.AddListener(UpdateDamageView);
        _card.ManaChanged.AddListener(UpdateManaView);
        _card.DrugStarted.AddListener(OnDrugStart);
        _card.DrugEnded.AddListener(OnDrugEnd);
    }

    private void OnDisable()
    {
        _card.HealthChanged.RemoveListener(UpdateHealthView);
        _card.AttackStrenchChanged.RemoveListener(UpdateDamageView);
        _card.ManaChanged.RemoveListener(UpdateManaView);
        _card.DrugStarted.RemoveListener(OnDrugStart);
        _card.DrugEnded.RemoveListener(OnDrugEnd);
    }

    private void Start()
    {
        _nameField.text = _card.info.Name;
        _descriptionField.text = _card.info.Description;
        _imagePlace.sprite = _card.info.Picture;
        UpdateAll();
    }

    private void UpdateHealthView(int oldValue,int newValue)
    {
        CharacteristicChanged(oldValue, newValue, _card.info.Health, _healthField);
    }

    private void UpdateDamageView(int oldValue, int newValue)
    {
        CharacteristicChanged(oldValue, newValue, _card.info.Damage, _attackField);
    }

    private void UpdateManaView(int oldValue, int newValue)
    {
        CharacteristicChanged(oldValue, newValue, _card.info.Mana, _manaField);
    }

    private void OnDrugStart()
    {
        _frameSpriteRenderer.color = _drugedColor;
    }

    private void OnDrugEnd()
    {
        _frameSpriteRenderer.color = _standartFrameColor;
    }

    private void CharacteristicChanged(int oldValue, int newValue, int standartValue, TMP_Text textField)
    {
        Color newColor;
        if (newValue < standartValue)
        {
            newColor = _badCharacteristicColor;
        }
        else if (newValue > standartValue)
        {
            newColor = _goodCharacteristicColor;
        }
        else
        {
            newColor = _standartharacteristicColor;
        }
        Sequence sequence = DOTween.Sequence();
        for (int i = oldValue; i != newValue; i = i + (i < newValue ? 1 : -1))
        {
            int newI = i;
            sequence.Append(textField.DOColor(_changedCharactericticColor, _colorChangeDituration).OnComplete(() => textField.text = newI.ToString()));
            sequence.Append(textField.DOColor(newColor, _colorChangeDituration));
        }
        sequence.Append(textField.DOColor(_changedCharactericticColor, _colorChangeDituration).OnComplete(() => textField.text = newValue.ToString()));
        sequence.Append(textField.DOColor(newColor, _colorChangeDituration));
        sequence.Play();
    }

    public void UpdateAll()
    {
        _healthField.text = _card.Health.ToString();
        _attackField.text = _card.Damage.ToString();
        _manaField.text = _card.Mana.ToString();
    }

    public void OnMouseEnter()
    {
        transform.position += _onMouseEnterPositionOffcet;
        transform.DOScale(_onMouseEnterScale, _onMouseEnterScaleRiseTime);
    }

    public void OnMouseExit()
    {
        transform.position -= _onMouseEnterPositionOffcet;
        transform.DOScale(_standartScale, _onMouseEnterScaleRiseTime);
    }
}
