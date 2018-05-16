using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PayerHealthController : HealthController {

    public Slider healthBar;

	// Use this for initialization
	void Start () {
        currentHealth = maxHealth;
        healthBar.value = CalculatedHealth();
	}

    public override void TookDamage(int damage)
    {
        base.TookDamage(damage);

        healthBar.value = CalculatedHealth();
    }

    public override void GainHealth(int gain)
    {
        base.GainHealth(gain);

        healthBar.value = CalculatedHealth();
    }

    float CalculatedHealth()
    {
        return currentHealth / maxHealth;
    }
}
