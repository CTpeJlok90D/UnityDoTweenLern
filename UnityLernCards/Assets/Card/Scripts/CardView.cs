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
    [Header("Animations")]
    [SerializeField] private float _onMouseEnterScale = 1.3f;
    [SerializeField] private float _onMouseEnterScaleRiseTime = 0.15f;
    [SerializeField] private Vector3 _onMouseEnterPositionOffcet = new Vector3(0,0,-1);
    [SerializeField] private Color _badCharacteristicColor = Color.red;
    [SerializeField] private Color _goodCharacteristicColor = Color.green;
    [SerializeField] private Color _standartharacteristicColor = Color.white;
    [SerializeField] private Color _changedCharactericticColor = Color.cyan;
    [SerializeField] private float _colorChangeDituration = 0.12f;

    private float _standartScale;
    private Vector3 _oldPosition;
    private Sequence _previewSequice;

    private void Awake()
    {
        _standartScale = transform.localScale.x;
    }

    private void OnEnable()
    {
        _nameField.text = _card.info.Name;
        _descriptionField.text = _card.info.Description;
        _imagePlace.sprite = _card.info.Picture;

        _card.HealthChanged.AddListener(UpdateHealthView);
        _card.AttackStrenchChanged.AddListener(UpdateDamageView);
        _card.ManaChanged.AddListener(UpdateManaView);
        UpdateAll();
    }

    private void UpdateHealthView(int newValue)
    {
        CharacteristicChanged(newValue, _card.info.Health, _healthField);
    }

    private void UpdateDamageView(int newValue)
    {
        CharacteristicChanged(newValue, _card.info.Damage, _attackField);
    }

    private void UpdateManaView(int newValue)
    {
        CharacteristicChanged(newValue, _card.info.Mana, _manaField);
    }

    private void CharacteristicChanged(int newValue, int standartValue, TMP_Text textField)
    {
        _previewSequice?.Kill();
        textField.text = newValue.ToString();
        Sequence sequence = DOTween.Sequence().Append(textField.DOColor(_changedCharactericticColor, _colorChangeDituration));
        if (newValue < standartValue)
        {
            sequence.Append(textField.DOColor(_badCharacteristicColor, _colorChangeDituration));
            return;
        }
        if (newValue > standartValue)
        {
            sequence.Append(textField.DOColor(_goodCharacteristicColor, _colorChangeDituration));
            return;
        }
        if (newValue == standartValue)
        {
            sequence.Append(textField.DOColor(_standartharacteristicColor, _colorChangeDituration));
            return;
        }
        _previewSequice = sequence.Play();
    }

    public void UpdateAll()
    {
        _healthField.text = _card.Health.ToString();
        _attackField.text = _card.Damage.ToString();
        _manaField.text = _card.Mana.ToString();
    }
    public void OnMouseEnter()
    {
        _oldPosition = transform.position;
        transform.DOMove(transform.position + _onMouseEnterPositionOffcet, _onMouseEnterScaleRiseTime);
        transform.DOScale(_onMouseEnterScale, _onMouseEnterScaleRiseTime);
    }
    public void OnMouseExit()
    {
        transform.DOMove(_oldPosition, _onMouseEnterScaleRiseTime);
        transform.DOScale(_standartScale, _onMouseEnterScaleRiseTime);
    }
}
