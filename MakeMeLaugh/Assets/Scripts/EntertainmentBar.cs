using UnityEngine;
using UnityEngine.UI;

public class EntertainmentBar : MonoBehaviour
{
    [SerializeField] private RectTransform _barRect;
    [SerializeField] private RectMask2D _mask;

    private float _maxRightMask;
    private float _initialRightMask;

    private void Start()
    {
        _maxRightMask = _barRect.rect.height - _mask.padding.w - _mask.padding.y;
        _initialRightMask = _mask.padding.y;
    }

    private void Update()
    {
        SetValue(GameManager.instance.TaskTimer);
    }

    public void SetValue(float newValue)
    {
        var targetWidth = newValue * _maxRightMask / GameManager.instance.MaxTimer;
        var newRightMask = _maxRightMask + _initialRightMask - targetWidth;
        var padding = _mask.padding;
        padding.z = newRightMask;
        _mask.padding = padding;
    }
}
