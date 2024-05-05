using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GODMode : MonoBehaviour
{
    bool godModeOn;
    public LiquidBoss_Behaviour boss;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            Debug.Log("GODMODE ACTIVADO");
            godModeOn = true;
        }

        if(godModeOn)
        {
            if (Input.GetKeyDown(KeyCode.F1))
            {
                Debug.Log("ESTE SERVIRÁ PARA REINICIAR AL PLAYER");
            }

            if (Input.GetKeyDown(KeyCode.F2))
            {
                boss.state = LiquidBoss_Behaviour.StatesType.Killed;
            }
        }
    }
}
