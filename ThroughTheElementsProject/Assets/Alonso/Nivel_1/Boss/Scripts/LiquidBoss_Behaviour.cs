using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiquidBoss_Behaviour : MonoBehaviour
{
    public Transform Player;

    // ------------------------- ATAQUES -------------------------
    [Header("Orbs Setting")]
    public GameObject OrbAttackPrefab;
    public Transform OrbAttackSpawn_Left;
    public Transform OrbAttackSpawn_Right;

    [Header("WavesAttack Setting")]
    public GameObject WavesAttackPrefab;
    public Transform WavesAttackSpawn_1;
    public Transform WavesAttackSpawn_2;
    public Transform WavesAttackSpawn_3;

    [Header("BodyOrb Setting")]
    public GameObject bodyOrbPrefab;
    public Transform bodyOrbSpawn;

    [Header("SlashAttack Setting")]
    public GameObject SlashAttackPrefab;
    public Transform SlashAttackSpawn;

    [Header("SpikesAttack Setting")]
    public GameObject SpikeAttackPrefab;

    [Header("Detection Setting")]
    public Transform DistanceChecker;

    public float detectionPlayerTime = 0f;
    public float maxDetectionPlayerTime = 5f;

    [SerializeField] float detectionDistance = 5f;
    [SerializeField] LayerMask detectionLayerMask = Physics.DefaultRaycastLayers;
    [SerializeField] List<string> detectionTags = new List<string> { "Player" };

    // ------------------------- COMPONENTES -------------------------
    Animator _anim;
    LiquidBoss_HealthBehaviour _bossHealthBehaviour;

    // ------------------------- COMPOROTAMIENTO -------------------------

    [Header("Behaviour Settings")]
    public bool battleStarted; // ESTE ES PARA DETERMINAR CUANDO EMPIEZA EL BOSS A PEGARTE
    public bool canAttack; // ESTE BOOLEANO TENDRÁ QUE DESACTIVARSE CADA VEZ QUE UNA ANIMACIÓN SE TERMINA PARA QUE PUEDA HACER OTRO ATAQUE 
    public bool canHeal; // ESTE BOOLEANO TENDRÁ QUE DESACTIVARSE PARA EVITAR QUE EL BOSS SE CURE DEMASIADAS VECES
    public bool playerOnArea; // ESTE BOOLEANO SE UTILIZA PARA VER QUE EL PLAYER ESTÁ DENTRO DE LA ZONA Y ASÍ CUANDO MUERA PODRÁ VOLVER
    
    public float initialWaitingTime; // ESTE TIEMPO SIRVE PARA PODER EMPEZAR LA PARTIDA UN POCO DESPUÉS DE QUE EL JUGADOR HAYA PASADO AL CAMPO DE BATALLA 
    public float attackIntervalTime = 3f; // ESTE ES PARA QUE HAYA CIERTO TIEMPO ENTRE LOS ATAQUES
    [SerializeField] float currentattackIntervalTime = 0f;
    public int attackType;

    Transform spikeTarget;

    [Header("Recovering Settings")]
    public float recoveryTime = 5f;

    public enum StatesType
    {
        OnBattle,
        WaitingBattle,
        Exploding, 
        Recovering,
        Killed,
        PlayerKilled
    }

    public StatesType state;

    private void Awake()
    {
        _anim = GetComponent<Animator>();
        _bossHealthBehaviour = GetComponent<LiquidBoss_HealthBehaviour>();
        battleStarted = false;
        canAttack = true;

        state = StatesType.WaitingBattle;
        currentattackIntervalTime = attackIntervalTime;
    }

    public void CanAttack()
    {
        canAttack = true;
    }

    public void Update()
    {
        Debug.Log("El boss está "+state);

        if(playerOnArea)
        {
            switch (state)
            {
                case StatesType.WaitingBattle:
                    WaitingBattle();
                    break;
                case StatesType.OnBattle:
                    OnBattle();
                    CheckDistance();
                    break;
                case StatesType.Exploding:
                    Exploding();
                    break;
                case StatesType.Recovering:
                    Recovering();
                    break;
                case StatesType.Killed:
                    Dead();
                    break;
                case StatesType.PlayerKilled:
                    WaitingPlayer();
                    break;
            }
        }
    }

    private void WaitingPlayer()
    {
        playerOnArea = false;
    }

    int attack;
    int lastAttack;

    public void OnBattle()
    {
        currentattackIntervalTime += Time.deltaTime;

        if(currentattackIntervalTime >= attackIntervalTime && canAttack)
        {
            canAttack = false;
            attack = Random.Range(0, 3);

            if(attack == lastAttack)
            {
                while(attack == lastAttack)
                {
                    attack = Random.Range(0, 3);
                }
            }

            lastAttack = attack;

            if(attack == 0)
            {
                _anim.SetTrigger("SlashAttack");
            }
            else if(attack == 1)
            {
                OrbAttack();
            }
            else if(attack == 2 )
            {
                if(Player.GetComponent<CharacterController>().isGrounded)
                {
                    Debug.Log("ESTÁ EN EL SUELO ");
                }

                spikeTarget = Player;
                _anim.SetTrigger("SpikeAttack");
            }
            currentattackIntervalTime = 0;
        }
    }

    public void CheckDistance()
    {
        if(canAttack)
        {
            Collider[] collider = Physics.OverlapSphere(DistanceChecker.position, detectionDistance, detectionLayerMask);
            for (int i = 0; i < collider.Length; i++)
            {
                if (detectionTags.Contains(collider[i].tag))
                {
                    detectionPlayerTime += Time.deltaTime;
                    if (detectionPlayerTime >= maxDetectionPlayerTime && canAttack)
                    {
                        canAttack = false;
                        state = StatesType.Exploding;
                        detectionPlayerTime = 0;
                    }
                }
            }
        }
    }

    public void WaitingBattle()
    {
        if (battleStarted)
        {
            initialWaitingTime -= Time.deltaTime;

            if (initialWaitingTime < 0)
            {
                state = StatesType.OnBattle;
            }
        }
    }

    public void Exploding()
    {   
        if(canAttack)
        {
            state = StatesType.Recovering;
            _anim.SetTrigger("WaveAttack");
            Instantiate(bodyOrbPrefab, bodyOrbSpawn.position, Quaternion.identity);
            canAttack = false;
        }
    }

    public void Recovering()
    {
        if(canAttack)
        {
            _bossHealthBehaviour.Healing();
        }
    }

    public void Dead()
    {
        gameObject.SetActive(false);
    }

    // ------------------------------------ FUNCIONES DE ATAQUES ------------------------------------
    public void OrbAttack()
    {
        GameObject rigthOrb;
        GameObject leftOrb;

        if(_anim != null) 
        { 
            _anim.SetTrigger("OrbAttack");
        }
        rigthOrb = Instantiate(OrbAttackPrefab, OrbAttackSpawn_Right.position, OrbAttackSpawn_Right.rotation, OrbAttackSpawn_Right.transform);
        leftOrb = Instantiate(OrbAttackPrefab, OrbAttackSpawn_Left.position, OrbAttackSpawn_Left.rotation, OrbAttackSpawn_Left.transform);

        rigthOrb.GetComponent<OrbBehaviour>().ThrowOrb(Player.position);
        leftOrb.GetComponent<OrbBehaviour>().ThrowOrb(Player.position);
    }

    public void SlashAttack()//ESTA FUNCION SE EJECUTA DESDE UNA FUNCIÓN PARA QUE FUNCIONE LO MAS EXACTA POSIBLE
    {
        GameObject slash;

        slash = Instantiate(SlashAttackPrefab, SlashAttackSpawn.position, SlashAttackSpawn.rotation, SlashAttackSpawn.transform);
        slash.GetComponent<SlashBehaviour>().ThrowSlash(Player.position);
    }

    public void WaveAttack()
    {
        Instantiate(WavesAttackPrefab, WavesAttackSpawn_1.position, WavesAttackSpawn_1.rotation, WavesAttackSpawn_1.transform);
        Instantiate(WavesAttackPrefab, WavesAttackSpawn_2.position, WavesAttackSpawn_2.rotation, WavesAttackSpawn_2.transform);
        Instantiate(WavesAttackPrefab, WavesAttackSpawn_3.position, WavesAttackSpawn_3.rotation, WavesAttackSpawn_3.transform);
    }

    public void SpikeAttack()
    {
        Instantiate(SpikeAttackPrefab, spikeTarget.position, Quaternion.identity);
    }
}
