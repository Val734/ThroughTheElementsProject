using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GODMode : MonoBehaviour
{
    public LiquidBoss_Behaviour boss;
    public GameObject Player;

    int maxPoints;
    int currentPoint;
    public List<Transform> gamePoints;

    private void Awake()
    {
        currentPoint = 0;
        maxPoints = gamePoints.Count;
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            Debug.Log("ESTE SERVIRÁ PARA REINICIAR AL PLAYER");
        }
        if (Input.GetKeyDown(KeyCode.F2))
        {
            Debug.Log("Boss Muerto");
            boss.Dead();
        }
        if(Input.GetKeyDown(KeyCode.F3))
        {
            currentPoint++;
            if(currentPoint >= maxPoints) 
            {
                currentPoint = 0;
            }
            MovePlayer(currentPoint);
        }
        if(Input.GetKeyDown(KeyCode.F4))
        {
            currentPoint--;
            if (currentPoint < 0)
            {
                currentPoint = maxPoints - 1;
            }
            MovePlayer(currentPoint);
        }
    }



    public void MovePlayer(int actualPoint)
    {
        Player.SetActive(false);
        Vector3 newDirection = gamePoints[actualPoint].transform.position;
        Player.transform.position = newDirection;
        Player.SetActive(true);
    }
}
