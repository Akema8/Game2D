using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {

    public Vector2 speed = new Vector2(15, 15);

    private Vector2 movement;
    private Rigidbody2D rb;

    void Awake()
    {
            rb = GetComponent<Rigidbody2D>();
    }
    // Update is called once per frame
    void Update () {
        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");

        movement = new Vector2(speed.x * inputX, speed.y * inputY);

        //стрельба
        bool shoot = Input.GetButtonDown("Fire1");
        shoot |= Input.GetButtonDown("Fire2");
        if (shoot)
        {
            WeaponScript weapon = GetComponent<WeaponScript>();
            if (weapon != null)
            {
                weapon.Attack(false); //false так как игрок не враг
            }
        }
		
	}

    void FixedUpdate()
    {
        rb.velocity = movement;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {

        bool damagePlayer = false;

        EnemyScript enemy = collision.gameObject.GetComponent<EnemyScript>();
        if (enemy != null)
        {
            HealthScript enemyHealth = enemy.GetComponent<HealthScript>();
            if (enemyHealth != null)
            {
                enemyHealth.Damage(enemyHealth.hp);

                damagePlayer = true;
            }
        }

        if (damagePlayer)
        {
            HealthScript playerHealth = GetComponent<HealthScript>();
            if (playerHealth != null)
                playerHealth.Damage(1);
        }
    }
   
}
