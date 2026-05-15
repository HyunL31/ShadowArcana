using UnityEngine;
using UnityEngine.UI;

public class HPBar : UIBase
{
    [SerializeField] private Slider _hpBar;
    [SerializeField] private Image _fillImage;

    private GameObject _target;
    private float _yOffset = 2f;

    private void Start()
    {
        _hpBar.onValueChanged.AddListener(ChangeColor);
        ChangeColor(_hpBar.value);
    }

    private void LateUpdate()
    {
        FollowTarget();
    }

    private void ChangeColor(float value)
    {
        Color color = Color.green;

        if (_hpBar.value <= _hpBar.maxValue * 0.3)
        {
            color = Color.red;
        }
        else if (_hpBar.value <= _hpBar.maxValue * 0.7)
        {
            color = Color.yellow;
        }
        else if (_hpBar.value > _hpBar.maxValue * 0.7)
        {
            color = Color.green;
        }

        _fillImage.color = color;
    }

    public void SetValue(int min, int max)
    {
        _hpBar.minValue = min;
        _hpBar.maxValue = max;

        _hpBar.value = max;
    }

    public Slider GetHPBar()
    {
        return _hpBar;
    }

    public void SetTarget(GameObject gameObject)
    {
        _target = gameObject;
    }

    private void FollowTarget()
    {
        if (_target == null)
        {
            return;
        }

        Vector2 targetPos = new Vector2(_target.transform.position.x, _target.transform.position.y + _yOffset);
        targetPos = Camera.main.WorldToScreenPoint(targetPos);

        gameObject.transform.position = targetPos;
    }

    public void UpdateValue(int currentHP)
    {
        _hpBar.value = currentHP;
    }
}