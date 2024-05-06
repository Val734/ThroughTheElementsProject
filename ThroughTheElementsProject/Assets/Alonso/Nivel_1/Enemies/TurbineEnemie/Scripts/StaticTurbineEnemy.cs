using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticTurbineEnemy : MonoBehaviour
{
    [Header("Detection Setting")]
    public Transform centerAttackZone;
    [SerializeField] float detectionDistance = 3f;
    [SerializeField] LayerMask detectionLayerMask = Physics.DefaultRaycastLayers;
    [SerializeField] List<string> detectionTags = new List<string> { "Player" };
    [SerializeField] Transform target;


    [Header("Attack Settings")]
    public float attackTime = 3f;
    int attack;
    bool canDoAttack;

    [Header("Prefabs Settings")]
    public Transform blowSpawnPoint;
    public GameObject BlowPrefab;
    public GameObject AbsorbPrefab;

    [Header("Components")]
    Animator _anim;

    [Header("Sounds")]
   public AudioSource blowAttackSound;
   public AudioSource absorbAttackSound;

    public enum State
    {
        Waiting,
        Attacking
    }

    State state = State.Waiting;

    private void Awake()
    {
        _anim = GetComponent<Animator>();
        canDoAttack = true;
    }

    private void Update()
    {
        switch(state)
        {
            case State.Waiting:
                DetectionCheck();
                break;
            case State.Attacking:
                Attacking();
                break;
        }
    }

    public void DetectionCheck()
    {
        Collider[] collider = Physics.OverlapSphere(centerAttackZone.position, detectionDistance, detectionLayerMask);
        for (int i = 0; i < collider.Length; i++)
        {
            if (detectionTags.Contains(collider[i].tag))
            {
                target = collider[i].transform;
                state = State.Attacking;
            }
        }
    }

    public void CanAttackAgain()
    {
        AbsorbPrefab.SetActive(false);
        canDoAttack = true;
        attackTime = 3f;
    }

    public void Attacking()
    {
        if(canDoAttack)
        {
            attackTime -= Time.deltaTime;
            if(attackTime < 0)
            {
                attack = Random.Range(0, 2);
                if (attack == 0)
                {
                    canDoAttack = false;
                    _anim.SetTrigger("Blow_Attack");
                }
                if (attack == 1)
                {
                    canDoAttack = false;
                    _anim.SetTrigger("Absorb_Attack");
                }
            }
        }
    }


    public void BlowAttack()
    {
        blowAttackSound.Play();
        Instantiate(BlowPrefab, blowSpawnPoint, false);
    }

    public void AbsorbAttack()
    {
        absorbAttackSound.Play();
        AbsorbPrefab.SetActive(true);
    }

}
