
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image fillColor;
    [SerializeField] private Slider slider;
    [SerializeField] private Gradient healthGradient;

    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
    }

    public void SetHealth(int health)
    {
        slider.value = health;
        fillColor.color = healthGradient.Evaluate(slider.normalizedValue);
    }
}
