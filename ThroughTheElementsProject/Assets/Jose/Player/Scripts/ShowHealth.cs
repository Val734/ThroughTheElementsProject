using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class ShowHealth : MonoBehaviour
{
    public GameObject StaminaText;
    public GameObject Player;

    private void Update()
    {
        StaminaText.GetComponent<TextMeshProUGUI>().text = "Health:" + "" + Mathf.FloorToInt(Player.GetComponent<HealthBehaviour>().health);

    }
}
