using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour {

    public string effectorTag;
    public float maxHealth;

    protected float currentHealth;
    protected SpriteRenderer sprite;

    private void Start()
    {
        currentHealth = maxHealth;
    }

    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    private void TakeDamageEffect( DamageEffector effector)
    {

        Debug.Log(effector);
        if (effector.CompareTag(effectorTag))
        {
            TookDamage(effector.damage);
        }
    }

    virtual public void TookDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            gameObject.SendMessage("Dead", currentHealth, SendMessageOptions.DontRequireReceiver);
        }
        else
        {
            StartCoroutine(TookDamageCoroutine());
        }
    }

    virtual public void GainHealth(int gain)
    {
        if (currentHealth < maxHealth)
        {
            currentHealth += gain;
        }
        else
        {
            currentHealth = maxHealth;
        }

    }

    IEnumerator TookDamageCoroutine()
    {
        sprite.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        sprite.color = Color.white;
    }

}
