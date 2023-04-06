using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : GameUnit
{
    float damage;
    Transform target;
    [SerializeField] Transform bulletTf;
    [SerializeField] float speed;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Cache.TAG_ENEMY))
        {
            Cache.GetZombie(other).OnHit(damage);
            OnDespawn();
        }
    }

    public void SetTarget(Transform enemy, float dmg)
    {
        target = enemy;
        damage = dmg;
    }

    private void FixedUpdate()
    {
        if (target != null && target.gameObject.activeInHierarchy == true)
        {
            Vector3 direct = target.position - TF.position;
            float angle = Mathf.Atan2(direct.x, direct.z) * Mathf.Rad2Deg;
            TF.rotation = Quaternion.Euler(90, 0, 180 - angle);

            bulletTf.position += (target.position - bulletTf.position).normalized * speed * Time.fixedDeltaTime;
            //bulletTf.position = new Vector3(bulletTf.position.x, 0.1f, bulletTf.position.z);
        }
        else
        {
            OnDespawn();
        }
    }
    public override void OnDespawn()
    {
        target = null;
        SimplePool.Despawn(this);
    }

    
}
