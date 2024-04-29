using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    [SerializeField] Image healthbarSprite;
    [SerializeField] float reduceSpeed = 2f;
    float target = 1f;
    private Camera camera;
    private void Awake()
    {
        camera = Camera.main;
    }
    private void Update()
    {
        transform.rotation = Quaternion.LookRotation(transform.position - camera.transform.position);
        healthbarSprite.fillAmount = Mathf.MoveTowards(healthbarSprite.fillAmount, target, reduceSpeed);
    }
    public void UpdateHealthbar(float maxHealth, float currentHealth)
    {
        target = currentHealth / maxHealth;
    }
}
