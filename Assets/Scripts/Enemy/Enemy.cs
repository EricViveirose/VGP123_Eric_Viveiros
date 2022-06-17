using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(SpriteRenderer), typeof(Animator))]
public class Enemy : MonoBehaviour
{

    protected SpriteRenderer sr;
    protected Animator anim;
    protected PlayerController pc;

    protected int _health;
    [SerializeField] protected int maxHealth;

    public int Health
    {
        get { return _health; }
        set
        {
            _health = value;

            if (_health >maxHealth)
            {
                _health = maxHealth;
            }

            if (_health <= 0)
                Death();
            
        }
    }

    public virtual void Death ()
    {
        //Nothing needed here
    }

    public virtual void TakeDamage (int damage)
    {
        Health -= damage;
    }

    public virtual void Start()
    {
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        pc = FindObjectOfType<PlayerController>();

        if (maxHealth <= 0)
            maxHealth = 10;

        Health = maxHealth;


    }
}
