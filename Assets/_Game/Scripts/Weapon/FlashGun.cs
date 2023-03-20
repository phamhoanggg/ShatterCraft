using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashGun : Weapon
{
    List<Zombie> enemyList = new List<Zombie>();
    [SerializeField] private Transform cannon;
    [SerializeField] private SphereCollider col;

    private float atkCD = 0;
    private float fireCD = 0;
    private Transform target;
    private Vector3 direct;
    private bool firing;
    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.IsState(Enums.GameState.Playing))
        {
            if (target == null)
            {
                target = (GetNearestTarget() != null) ? GetNearestTarget().TF : null;

            }

            if (target != null)
            {
                Vector3 direct = target.position - TF.position;
                float angle = Mathf.Atan2(direct.x, direct.z) * Mathf.Rad2Deg;
                TF.rotation = Quaternion.Euler(90, 0, 180 - angle);
            }
            else
            {
                TF.rotation = TF.rotation;
            }

            if (atkCD > 0)
            {
                atkCD -= Time.deltaTime;
            }
            else
            {
                if (target != null)
                {
                    direct = target.position - TF.position;
                    float angle = Mathf.Atan2(direct.x, direct.z) * Mathf.Rad2Deg;
                    TF.rotation = Quaternion.Euler(90, 0, 180 - angle);
                    firing = true;
                }
            }
        }

        if (firing)
        {
            if (fireCD <= 0)
            {
                Fire(target);
                fireCD = 0.2f;
            }
            else
            {
                fireCD -= Time.deltaTime;
            }
            
            if (target.gameObject.activeInHierarchy == false)
            {
                firing = false;
                atkCD = 1 / ATKSpeed;
                target = null;
            }
        }
    }

    public Zombie GetNearestTarget()
    {
        Zombie target = null;
        float distance = float.PositiveInfinity;

        for (int i = 0; i < enemyList.Count; i++)
        {
            if (enemyList[i].isDead)
            {
                enemyList.RemoveAt(i);
                continue;
            }
            if (enemyList[i] != null && !enemyList[i].isDead && Vector3.Distance(TF.position, enemyList[i].TF.position) <= col.radius)
            {
                float dis = Vector3.Distance(TF.position, enemyList[i].TF.position);

                if (dis < distance)
                {
                    distance = dis;
                    target = enemyList[i];
                }
            }
        }
         
        return target;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Cache.TAG_ENEMY))
        {
            enemyList.Add(Cache.GetZombie(other));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(Cache.TAG_ENEMY))
        {
            enemyList.Remove(Cache.GetZombie(other));
        }
    }

    public void Fire(Transform enemy)
    {
        Bullet bullet = SimplePool.Spawn<Bullet>(PoolType.Bullet, cannon.position, Quaternion.identity);
        bullet.SetTarget(enemy, Damage);
    }

}
