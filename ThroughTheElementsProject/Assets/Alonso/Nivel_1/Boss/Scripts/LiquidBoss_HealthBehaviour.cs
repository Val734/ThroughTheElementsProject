using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LiquidBoss_HealthBehaviour : MonoBehaviour
{
    [SerializeField] public UnityEvent OnBossDie;


    [Header("Stats")]
    [SerializeField] int lives;
    [SerializeField] int maxLives;
    [SerializeField] float initialHealingTime;
    [SerializeField] float currenthealingTime;
    [SerializeField] int timeshealed = 0;
    [SerializeField] int maxTimesHealed = 2;

    [SerializeField] Healthbar healthbar;
    [SerializeField] int hitsRecived;

    [Header("Debug Settings")]
    [SerializeField] bool debugHit;

    HurtCollider _hCollider;
    LiquidBoss_Behaviour _boss;

    private void Awake()
    {
        _hCollider = GetComponentInChildren<HurtCollider>();
        _boss = GetComponent<LiquidBoss_Behaviour>();
        healthbar.UpdateHealthbar(maxLives, lives);
    }

    private void OnEnable()
    {
        if(_hCollider != null)
        {
            _hCollider.onHurt.AddListener(OnHit);
        }
    }

    public void OnHit(int damage) //AQUÍ ESTÁ EL PROBLEMA, CON LOS HITS RECIVED Y SU PUTA MADRE
    {
        if(_boss.state != LiquidBoss_Behaviour.StatesType.Exploding || _boss.state != LiquidBoss_Behaviour.StatesType.Recovering) 
        {
            hitsRecived++;
            if (hitsRecived <= 3)
            {
                LoseHealth();
                Debug.Log("El ON HIT");
                if(hitsRecived == 3)
                {
                    _boss.state = LiquidBoss_Behaviour.StatesType.Exploding;
                    hitsRecived = 0;
                }
            }            
        }
    }

    private void LoseHealth()
    {
        if(_boss.state == LiquidBoss_Behaviour.StatesType.OnBattle || _boss.state == LiquidBoss_Behaviour.StatesType.Recovering)
        {
            lives--;
            healthbar.UpdateHealthbar(maxLives, lives);
            if (lives <= 0)
            {
                _boss.state = LiquidBoss_Behaviour.StatesType.Killed;
                OnBossDie.Invoke();
            }
        }
        else
        {
            Debug.Log("No le puedes hacer daño ahora");
        }
    }

    private void Dead()
    {
        gameObject.SetActive(false);    
    }

    public void Healing()
    {
        currenthealingTime -= Time.deltaTime;
        if(currenthealingTime > 0f)
        {
            if(timeshealed < maxTimesHealed)
            {
                int probabilityOfBeingCured = Random.Range(0, 20);

                if (probabilityOfBeingCured < 2)
                {
                    Debug.Log("ME HE CURADO");
                    lives++;
                    timeshealed++;
                    healthbar.UpdateHealthbar(maxLives, lives);
                }
                if (lives > maxLives)
                {
                    lives = maxLives;
                }
            }
        }
        else
        {
            timeshealed = 0;
            currenthealingTime = initialHealingTime;
            _boss.state = LiquidBoss_Behaviour.StatesType.OnBattle;
            //g
        }
    }
}
