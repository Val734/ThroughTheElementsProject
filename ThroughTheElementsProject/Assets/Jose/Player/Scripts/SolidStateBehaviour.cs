using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.XR;

public class SolidStateBehaviour : MonoBehaviour
{
    [SerializeField] InputActionReference TransformAction;
    [SerializeField] InputActionReference Hability;

    public bool isOnBallTransformation;
    public bool cannonNear;
    public GameObject cannon;

    private void Awake()
    {
        isOnBallTransformation = false;
    }
    public void OnEnable()
    {
        TransformAction.action.Enable();
        Hability.action.Enable();
    }

    void Update()
    {
        if (TransformAction.action.triggered && cannonNear)
        {
            isOnBallTransformation=true;
            cannon.GetComponent<CannonWorking>().CannonPlayer(gameObject);
            cannonNear = false;
            cannon = null;

        }
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Cannon"))
        {
            cannonNear = true;
            cannon = other.gameObject;

        }


    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Cannon"))
        {
            cannonNear = false;
            cannon = null;

        }
    }
    

}
