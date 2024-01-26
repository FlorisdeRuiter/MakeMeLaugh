using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    [SerializeField] Slider _entertainmentSlider;

    [SerializeField] private float _happyThreshold, _neutralThreshold;
    [SerializeField] private Sprite _happySprite, _neutralSprite, _angrySprite;
    [SerializeField] private Image FaceSprite;

    private void Update()
    {
        SetSliderValue();
        SetFacialSprite();
    }

    private void SetSliderValue()
    {
        _entertainmentSlider.value = Mathf.Lerp(_entertainmentSlider.value, GameManager.instance.GetNormalizedTime(), Time.deltaTime * 2);
    }

    private void SetFacialSprite()
    {
        float value = GameManager.instance.GetNormalizedTime();

        if (value > _happyThreshold && FaceSprite.sprite != _happySprite)
        {
            FaceSprite.sprite = _happySprite;
        }
        else if (value < _happyThreshold && value > _neutralThreshold && FaceSprite.sprite != _neutralSprite)
        {
            FaceSprite.sprite = _neutralSprite;
        }
        else if (value < _neutralThreshold && FaceSprite.sprite != _angrySprite)
        {
            FaceSprite.sprite = _angrySprite;
        }
    }
}