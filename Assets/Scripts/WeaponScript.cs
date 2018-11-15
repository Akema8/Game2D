using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponScript : MonoBehaviour {

	public Transform shotPrefab;

    public float shotingRate = 0.25f; //время перезарядки в сек

    private float shotCooldown; //перезарядка


    void Start()
    {
        shotCooldown = 0f;
    }

    void Update()
    {
        if (shotCooldown > 0)
        {
            shotCooldown -= Time.deltaTime;
        }
    }

    public void Attack (bool isEnemy)
    {
        if (CanAttack)
        {
            shotCooldown = shotingRate;

            var shotTransform = Instantiate(shotPrefab) as Transform;

            shotTransform.position = transform.position;

            ShotScript shot = shotTransform.gameObject.GetComponent<ShotScript>();
            if (shot != null)
            {
                shot.isEnemyShoot = isEnemy;
            }

            MoveScript move = shotTransform.gameObject.GetComponent<MoveScript>();
            if (move != null)
            {
                move.direction = this.transform.right;
            }

        }
    }

    public bool CanAttack
    {
        get
        {
            return shotCooldown <= 0f;
        }
    }
}
