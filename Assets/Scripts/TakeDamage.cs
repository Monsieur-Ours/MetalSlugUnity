using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeDamage : MonoBehaviour {

    public string effectorTag;
    public int health;

    protected SpriteRenderer sprite;

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

    public void TookDamage(int damage)
    { 
        health -= damage;
        if (health <= 0)
        {
            gameObject.SendMessage("Dead", health,SendMessageOptions.DontRequireReceiver);
        }
        else
        {
            StartCoroutine(TookDamageCoroutine());
        }
    }

    IEnumerator TookDamageCoroutine()
    {
        sprite.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        sprite.color = Color.white;
    }
}
