using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HealthBehaviour : MonoBehaviour
{
    [SerializeField] public UnityEvent OnDie;
    [SerializeField] public UnityEvent GetHurt;
    [SerializeField] public int maxHealth;
    public int health;
    public bool invulnerable;

    HurtCollider hurtCollider;

    private void Awake()
    {
        health = maxHealth;
        invulnerable = false;
        hurtCollider = GetComponent<HurtCollider>();
    }
    private void Update()
    {
        //Debug.Log(health);
    }
    private void OnEnable()
    {
        hurtCollider.onHurt.AddListener(Damage);
    }
    public void Damage(int damage)
    {

        if (health > 0)
        {
            if(!invulnerable)
            {
                health -= damage;
                GetHurt.Invoke();
            }
        }
        else if(health <= 0)
        {
            health = 0;
            OnDie.Invoke();

        }

    }
    public void Heal(int heal)
    {
        health -= heal;
    }
}
