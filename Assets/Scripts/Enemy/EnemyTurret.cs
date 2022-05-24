using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTurret : Enemy
{

    [SerializeField] float projectileForce;
    [SerializeField] float projectileFireRate;
 

    float timeSinceLastFire;
    

    public Transform spawnPointLeft;
    public Transform spawnPointRight;
   

    public Projectile projectilePrefab;


    public override void Start()
    {
        base.Start();

        if (projectileFireRate <= 0)
            projectileFireRate = 2.0f;

        if (projectileForce <= 0)
            projectileForce = 7.0f;
    }

    public override void Death()
    {
        base.Death();

        Destroy(gameObject);
    }

    
    void Update()
    {
        if (!anim.GetBool("Fire"))
        {
            if (Time.time >= timeSinceLastFire + projectileFireRate)
            {
                anim.SetBool("Fire", true);
            }
        }

        if (pc.gameObject.transform.position.x < transform.position.x)
        {
            sr.flipX = false;
        }
        else
        {
            sr.flipX = true;
        }
    }

    public void Fire()
    {
        timeSinceLastFire = Time.time;                                                                               //Time since last fire. If it was 5 seconds ago or 20 minutes ago.

        if (pc.gameObject.transform.position.x < transform.position.x  && -)
        {
            Projectile temp = Instantiate(projectilePrefab, spawnPointLeft.position, spawnPointLeft.rotation);
            temp.speed = -projectileForce;
        }
        else
        {
            Projectile temp = Instantiate(projectilePrefab, spawnPointRight.position, spawnPointRight.rotation);
            temp.speed = projectileForce;
        }

        //{
        //    Instantiate(projectilePrefab, spawnPointLeft.position, spawnPointRight.rotation);
        //    temp.speed = projectileForce;
        //}
    }


    public void ReturnToIdle()
    {
        anim.SetBool("Fire", false);
    }
}
