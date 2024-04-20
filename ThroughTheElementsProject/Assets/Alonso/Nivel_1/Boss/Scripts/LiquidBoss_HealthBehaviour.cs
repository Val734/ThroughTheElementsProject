using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiquidBoss_HealthBehaviour : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] int lives = 15;
    [SerializeField] int maxLives = 15;
    [SerializeField] float initialHealingTime = 5f;
    [SerializeField] float currenthealingTime = 5f;
    [SerializeField] int timeshealed = 0;
    [SerializeField] int maxTimesHealed = 2;

    HurtCollider _hCollider;
    LiquidBoss_Behaviour _boss;

    private void Awake()
    {
        Debug.Log("VIDAS: " + lives);
        _hCollider = GetComponentInChildren<HurtCollider>();
        _boss = GetComponent<LiquidBoss_Behaviour>();
    }

    private void OnEnable()
    {
        if(_hCollider != null)
        {
            _hCollider.onHurt.AddListener(OnHit);
            Debug.Log("HAY HURT");
        }
    }

    private void OnHit(int damage)
    {
        LoseHealth();
    }

    private void LoseHealth()
    {
        lives--;
        Debug.Log("VIDAS: " + lives);
        if(lives <=0 )
        {
            Debug.Log("Se ha muerto");
        }
    }

    public void Healing()
    {
        currenthealingTime -= Time.deltaTime;
        if(currenthealingTime > 0f)
        {
            if(timeshealed < maxTimesHealed)
            {
                int probabilityOfBeingCured = Random.Range(0, 10);

                if (probabilityOfBeingCured < 2)
                {
                    lives++;
                    timeshealed++;
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
        }
    }
}
