using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Zombie : GameUnit
{
    [SerializeField] private Transform zombieTf;
    [SerializeField] private Rigidbody zombieRb;
    [SerializeField] private Animator animator;
    [SerializeField] private float zombieDefaultHP;
    [SerializeField] private float point;
    [SerializeField] private float coin;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private ParticleType hitType;
    
    public bool isDead;
    private float currentHP;
    private int desIndex;
    public override void OnInit()
    {
        agent.enabled = true;

        desIndex = 0;
        agent.destination = LevelController.instance.CurrentLevel.DestinationList[desIndex].position;
        
        isDead = false;
        currentHP = zombieDefaultHP;
    }

    private void Update()
    {
        if (GameManager.instance.IsState(Enums.GameState.Playing))
        {
            if (Vector3.Distance(zombieTf.position, LevelController.instance.CurrentLevel.DestinationList[desIndex].position) < 2f)
            {
                desIndex++;
                agent.destination = LevelController.instance.CurrentLevel.DestinationList[desIndex].position;
            }
            else
            {
                agent.destination = LevelController.instance.CurrentLevel.DestinationList[desIndex].position;
            }
        }
    }

    public override void OnDespawn()
    {
        agent.enabled = false;
        SimplePool.Despawn(this);    
    }

    public void OnHit(float dmg)
    {
        if (!isDead)
        {
            currentHP -= dmg;
            ParticlePool.Play(hitType, zombieTf.position + Vector3.up, Quaternion.Euler(-90, 0, 0));
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
                ParticlePool.Play(hitType, zombieTf.position + Vector3.up, Quaternion.Euler(-90, 0, 0));
                LevelController.instance.CurrentLevel.WeaponProgressValue += 5;
                PointText text = SimplePool.Spawn<PointText>(PoolType.PointText, zombieTf.position, Quaternion.identity);        
                text.OnInit();
                text.SetText(5);
                Vibrator.Vibrate(100);
            }
        }
    }


    
}
