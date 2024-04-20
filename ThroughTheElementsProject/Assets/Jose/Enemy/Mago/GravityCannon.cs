using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityCannon : MonoBehaviour
{
    public GameObject Cannon;
    private void Start()
    {
        gravityCannon();
    }
    private void gravityCannon()
    {
        Cannon.GetComponentInChildren<CannonWorking>().haveGravity = true;
        Debug.Log("ENtra");
    }
    public void RestoreCannon()
    {
        Cannon.GetComponentInChildren<CannonWorking>().haveGravity = false;
    }
   //private void Update()
   //{
   //    Debug.Log(Cannon.GetComponentInChildren<CannonWorking>().haveGravity);
   //}

}
