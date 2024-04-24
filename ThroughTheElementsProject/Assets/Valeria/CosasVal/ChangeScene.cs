using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;


public class ChangeScene : MonoBehaviour
{
    public UnityEvent ChangeNextScene;
    public UnityEvent NotChangeNextScene;
    [SerializeField] InputActionReference ChangeSceneAction;
    [SerializeField] GoTo gt;
    [SerializeField] bool playerCanChangeScene;


    public void OnEnable()
    {
        ChangeSceneAction.action.Enable(); 
    }
    private void OnDisable()
    {
        ChangeSceneAction.action.Disable();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            ChangeNextScene.Invoke();
            playerCanChangeScene = true;
        }

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            NotChangeNextScene.Invoke();
            playerCanChangeScene = false;
        }
    }
    private void Update()
    {
        if (ChangeSceneAction.action.triggered && playerCanChangeScene)
        {
            gt.GoToScene();
        }
    }

}
