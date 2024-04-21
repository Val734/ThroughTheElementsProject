using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PortalBehavior : MonoBehaviour
{

    private void OnTriggerEnter(Collider collision)
    {
            if (collision.gameObject.CompareTag("Player") && gameObject.CompareTag("PortalOne"))
            {
               Debug.Log("TP");
               GoTo.GoToLevel1(); 
               Debug.Log("Ha colisionado con el Portal One");
            }
            else if (collision.gameObject.CompareTag("Player") && gameObject.CompareTag("PortalFive"))
            {
                Debug.Log("TP");
                GoTo.GoToLevel2();
                Debug.Log("Ha colisionado con el Portal Cinco");
            }
            else if (collision.gameObject.CompareTag("Player") && gameObject.CompareTag("PortalEight"))
            {
                Debug.Log("TP");
                GoTo.GoToLevel3();
                Debug.Log("Ha colisionado con el Portal Ocho");
            }
            else if (collision.gameObject.CompareTag("Player") && gameObject.CompareTag("PortalTen"))
            {
                Debug.Log("TP");
                GoTo.GoToLevel4();
                Debug.Log("Ha colisionado con el Portal Diez");
            }
    }


}
