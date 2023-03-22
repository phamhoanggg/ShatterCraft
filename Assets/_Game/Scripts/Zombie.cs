using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : GameUnit
{
    [SerializeField] private Transform zombieTf;
    [SerializeField] private Rigidbody zombieRb;
    [SerializeField] private Animator animator;
    [SerializeField] private float zombieDefaultHP;
    [SerializeField] private float point;
    [SerializeField] private float coin;

    public bool isDead;
    private float currentHP;


    public override void OnInit()
    {
        isDead = false;
        currentHP = zombieDefaultHP;
    }

    public override void OnDespawn()
    {
        SimplePool.Despawn(this);
        //        ParticlePool.Play(ParticleType.Hit, transform.position, Quaternion.identity);
        
    }

    public void OnHit(float dmg)
    {
        if (!isDead)
        {
            currentHP -= dmg;
            ParticlePool.Play(ParticleType.Hit, zombieTf.position + Vector3.up * 2, Quaternion.Euler(-90, 0, 0));
            Vibrator.Vibrate(100);
            if (currentHP <= 0)
            {
                Death();
                LevelController.instance.CurrentLevel.WeaponProgressValue += point;
                PointText text = SimplePool.Spawn<PointText>(PoolType.PointText, zombieTf.position, Quaternion.identity);
                text.OnInit();
                text.SetText(point);
            }
        }
    }

    public void Death()
    {
        if (!isDead)
        {
            isDead = true;
            GameManager.instance.SetCoin(GameManager.instance.CoinAmount + coin);
            LevelController.instance.CurrentLevel.LevelProgressValue++;
            OnDespawn();
        } 
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isDead)
        {
            if (other.CompareTag(Cache.TAG_WEAPON))
            {
                OnHit(other.GetComponent<Weapon>().Damage);
            }

            if (other.CompareTag(Cache.TAG_DEATHZONE))
            {
                Death();
                ParticlePool.Play(ParticleType.Hit, zombieTf.position, Quaternion.Euler(-90, 0, 0));
                LevelController.instance.CurrentLevel.WeaponProgressValue += 5;
                PointText text = SimplePool.Spawn<PointText>(PoolType.PointText, zombieTf.position, Quaternion.identity);
                Vibrator.Vibrate(250);
                text.OnInit();
                text.SetText(5);
            }
        }
    }
}
