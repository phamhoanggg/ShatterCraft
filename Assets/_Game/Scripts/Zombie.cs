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
    [SerializeField] private float speed;
    [SerializeField] private float point;
    [SerializeField] private float coin;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private ParticleType hitType;

    public bool isDead;
    public Transform[] Path;
    private float currentHP;
    private int pathIndex;
    public override void OnInit()
    {
        agent.enabled = true;

        pathIndex = 0;
        agent.destination = Path[pathIndex].position;
        isDead = false;
        currentHP = zombieDefaultHP;
    }

    private void Update()
    {
        if (GameManager.instance.IsState(Enums.GameState.Playing))
        {
            DetectOther();
        }
    }

    public override void OnDespawn()
    {
        SimplePool.Despawn(this);    
    }

    private void OnDisable()
    {
        agent.enabled = false;
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
            GameManager.instance.GamePlayObject.PlayAnim(Cache.ANIM_INCREASE);
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

            if (other.CompareTag(Cache.TAG_CHECHPOINT) && pathIndex < Path.Length - 1 && GameManager.instance.IsState(Enums.GameState.Playing)){
                pathIndex++;
                agent.destination = Path[pathIndex].position;
            }
        }
    }

    public void DetectOther()
    {
        if (Physics.Raycast(zombieTf.position + Vector3.up, Quaternion.AngleAxis(zombieTf.eulerAngles.y, Vector3.up) * Vector3.forward * 0.8f, out RaycastHit hit, 1f, GameManager.instance.ZombieLayer))
        {
            if (hit.collider.gameObject != this.gameObject)
            {
                agent.speed = 0.5f;
                zombieRb.velocity = Quaternion.AngleAxis(zombieTf.eulerAngles.y + 30, Vector3.up) * Vector3.forward * 0.5f;
            }

        }
        else if (Physics.Raycast(zombieTf.position + Vector3.up, Quaternion.AngleAxis(zombieTf.eulerAngles.y + 30, Vector3.up) * Vector3.forward * 0.8f, out RaycastHit hit1, 1f, GameManager.instance.ZombieLayer))
        {
            if (hit1.collider.gameObject != this.gameObject)
            {
                agent.speed = 0.5f;
                zombieRb.velocity = Quaternion.AngleAxis(zombieTf.eulerAngles.y - 30, Vector3.up) * Vector3.forward * 0.5f;
            }

        }
        else if (Physics.Raycast(zombieTf.position + Vector3.up, Quaternion.AngleAxis(zombieTf.eulerAngles.y - 30, Vector3.up) * Vector3.forward * 0.8f, out RaycastHit hit2, 1f, GameManager.instance.ZombieLayer))
        {
            if (hit2.collider.gameObject != this.gameObject)
            {
                agent.speed = 0.5f;
                zombieRb.velocity = Quaternion.AngleAxis(zombieTf.eulerAngles.y + 30, Vector3.up) * Vector3.forward * 0.5f;
            }


        }
        else
        {
            agent.speed = speed;
            
        }

        //Debug.DrawRay(zombieTf.position + Vector3.up, Quaternion.AngleAxis(zombieTf.eulerAngles.y - 30, Vector3.up) * Vector3.forward);
        //Debug.DrawRay(zombieTf.position + Vector3.up, Quaternion.AngleAxis(zombieTf.eulerAngles.y + 30, Vector3.up) * Vector3.forward);

    }


}
