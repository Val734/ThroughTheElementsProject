using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeveloperToolLv3 : MonoBehaviour
{
    public GameObject point1;
    public GameObject point2;
    public GameObject Player;

    private bool[] check ={false,false};


    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("p") && !check[0]) 
        {
            Player.SetActive(false);
            Player.transform.position=point1.transform.position;
            Player.SetActive(true);

            check[0] = true;
        }
        else if(Input.GetKeyDown("p") && !check[1])
        {
            Player.SetActive(false);
            Player.transform.position = point2.transform.position;
            Player.SetActive(true);

            check[1] = true;
        }
    }
}
