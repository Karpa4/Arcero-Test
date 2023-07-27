using System;
using UnityEngine;

public class Health : MonoBehaviour
{
	[SerializeField] private int defaultMaxHealth;
	private int currentMaxHealth;
	private int currentHealth;

	public event Action<float> HealthChanged;
	public event Action IsDead;

    public void Init(float modifier)
	{
		currentMaxHealth = (int)(defaultMaxHealth * modifier);
		currentHealth = currentMaxHealth;
	}

	public void TakeDamage(int damage)
	{
		if (currentHealth > 0)
        {
			currentHealth -= damage;
			HealthChangedInvoke();
			if (currentHealth <= 0)
            {
				IsDead?.Invoke();
            }
		}
	}

	public void FullRestore()
	{
		currentHealth = currentMaxHealth;
		HealthChangedInvoke();
	}

	private void HealthChangedInvoke()
    {
		float ratio = (float)currentHealth / currentMaxHealth;
		HealthChanged?.Invoke(ratio);
	}
}
