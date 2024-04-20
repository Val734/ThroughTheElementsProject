using UnityEngine;
using UnityEngine.Events;

public class CaveDoor : MonoBehaviour
{
    private int keysCollected = 0;
    [SerializeField] int totalKeys = 2; 
    private Animator[] animators;


    public UnityEvent OnDoorOpen; 

    public void Awake()
    {
        animators = GetComponentsInChildren<Animator>();
    }

    public void CollectKey()
    {
        keysCollected++;

        CheckKeys();
    }

    void CheckKeys()
    {
        if (keysCollected >= totalKeys)
        {
            OpenDoor();
        }
    }

    void OpenDoor()
    {
        foreach (Animator animator in animators)
        {
            animator.SetTrigger("OpenDoorR");
            animator.SetTrigger("OpenDoorL");
        }
    }


}
