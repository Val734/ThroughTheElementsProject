using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleDragBehaviour : MonoBehaviour
{
    Transform bubble;
    public bool dragStarted;
    public float dragTime;
    public float initialDragTime;

    CharacterController characterController;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    public void ActivateBubbleDragged(Transform originalBubble, float originalDragTime)
    {
        bubble = originalBubble;
        dragTime = originalDragTime;
        initialDragTime = originalDragTime;
        dragStarted = true;
    }

    private void Update()
    {
        if(dragStarted) 
        {
            dragTime -= Time.deltaTime;
            if(dragTime > 0 ) 
            {
                // Calculamos la direcci�n hacia la burbuja
                Vector3 direction = (bubble.position - transform.position).normalized;

                // Calculamos el desplazamiento en la direcci�n calculada
                Vector3 displacement = direction * Time.deltaTime;

                // Movemos el objeto
                characterController.Move(displacement);
            }
            else
            {
                dragStarted = false;
                dragTime = initialDragTime;
            }
        }
    }
}
