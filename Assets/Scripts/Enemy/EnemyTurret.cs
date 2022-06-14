using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class EnemyTurret : Enemy
{
    [SerializeField] float projectileForce;
    [SerializeField] float projectileFireRate;
    [SerializeField] float turretFireDistance;

    float timeSinceLastFire;

    public Transform spawnPointLeft;
    public Transform spawnPointRight;
    public Projectile projectilePrefab;
    public AudioClip fireSFX;
    public AudioClip deathSFX;
    public AudioMixerGroup soundFXMixer;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        if (projectileFireRate <= 0)
            projectileFireRate = 2.0f;

        if (projectileForce <= 0)
            projectileForce = 7.0f;

        if (turretFireDistance <= 0)
            turretFireDistance = 5.0f;
    }

    public override void Death()
    {
        base.Death();
        Destroy(gameObject);
        GameManager.instance.playerInstance.GetComponent<ObjectSounds>().Play(deathSFX, soundFXMixer);
    }
    // Update is called once per frame
    void Update()
    {
        if (!anim.GetBool("Fire"))
        {
            if (GameManager.instance.playerInstance)
            {
                if (GameManager.instance.playerInstance.gameObject.transform.position.x < transform.position.x)              //shoot player if within distance (5m)
                {
                    sr.flipX = false;
                }
                else
                {
                    sr.flipX = true;
                }
            }

            float distance = Vector2.Distance(GameManager.instance.playerInstance.gameObject.transform.position, transform.position);         //distance player is from turret. Fire if near by

            if (distance <= turretFireDistance)
            {
                sr.color = Color.red;
                if (Time.time >= timeSinceLastFire + projectileFireRate)
                {
                    anim.SetBool("Fire", true);
                    GameManager.instance.playerInstance.GetComponent<ObjectSounds>().Play(fireSFX, soundFXMixer);
                }
            }
            else
            {
                sr.color = Color.white;
            }
        }

    }

    public void Fire()
    {
        timeSinceLastFire = Time.time;
        if (!sr.flipX)
        {
            Projectile temp = Instantiate(projectilePrefab, spawnPointLeft.position, spawnPointLeft.rotation);
            temp.speed = -projectileForce;
        }
        else
        {
            Projectile temp = Instantiate(projectilePrefab, spawnPointRight.position, spawnPointRight.rotation);
            temp.speed = projectileForce;
        }
    }
    public void ReturnToIdle()
    {
        anim.SetBool("Fire", false);
    }
}