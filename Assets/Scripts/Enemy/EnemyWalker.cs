using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;


[RequireComponent (typeof(Rigidbody2D))]
public class EnemyWalker : Enemy
{

    Rigidbody2D rb;
    [SerializeField] float speed;
    public AudioClip deathSFX;
    public AudioClip headBopSFX;
    public AudioMixerGroup soundFXMixer;


    public override void Start()
    {
        base.Start();

        rb = GetComponent<Rigidbody2D>();

        if (speed <= 0)
            speed = 3;
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);

        Debug.Log("Enemy Walker took " + damage + " damage");
    }


    void Update()
    {
        if (!anim.GetBool("Death") && !anim.GetBool("Squish"))
        {
            if (sr.flipX)
            {
                rb.velocity = new Vector2(speed, rb.velocity.y);
            }
            else
            {
                rb.velocity = new Vector2(-speed, rb.velocity.y);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Barrier")
        {
            sr.flipX = !sr.flipX;
        }
    }

    public override void Death()
    {
        base.Death();
        anim.SetBool("Death", true);
        rb.velocity = Vector2.zero;
        Destroy(transform.parent.gameObject, 1.0f);
        GameManager.instance.playerInstance.GetComponent<ObjectSounds>().Play(deathSFX, soundFXMixer);
    }

    public void IsSquished()
    {
        anim.SetBool("Squish", true);
        rb.velocity = Vector2.zero;
        Destroy(transform.parent.gameObject, 1.0f);
        GameManager.instance.playerInstance.GetComponent<ObjectSounds>().Play(headBopSFX, soundFXMixer);
    }
}
