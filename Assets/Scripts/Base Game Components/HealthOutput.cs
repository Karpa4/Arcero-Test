using UnityEngine;
using UnityEngine.UI;

public class HealthOutput : MonoBehaviour
{
    [SerializeField] private Health health;
    [SerializeField] private Image hpBar;

    private void Start()
    {
        health.HealthChanged += ShowNewHp;
        health.IsDead += ShowZeroHp;
    }

    private void ShowNewHp(float ratio)
    {
        hpBar.fillAmount = ratio;
    }

    private void ShowZeroHp()
    {
        hpBar.fillAmount = 0;
    }

    private void OnDestroy()
    {
        health.HealthChanged -= ShowNewHp;
        health.IsDead -= ShowZeroHp;
    }
}
