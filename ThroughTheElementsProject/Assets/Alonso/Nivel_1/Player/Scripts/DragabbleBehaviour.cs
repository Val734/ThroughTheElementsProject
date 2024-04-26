using UnityEngine;

public class DragabbleBehaviour : MonoBehaviour
{
    //AQUI TENDRE QUE PILLAR LA DIFERENCIA DE POSICION DE LA OLA Y EL PLAYER 
    //LUEGO MOVER EL TRANSFORM PARA QUE SE MUEVA CON LA OLA USANDO EL c_Controller
    float initialdragTime = 3f;
    [SerializeField] float dragTime;
    bool dragStarted = false;

    Vector3 distanceFromWave;
    Transform wave;

    CharacterController characterController;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    public void ActivateDrag(Transform waveHitted, float time) // AÑADIR EL TIEMPO DE DRAG
    {
        dragStarted = true;
        wave = waveHitted;
        distanceFromWave = transform.position - waveHitted.position;
        dragTime = time;
    }

    public void DesactivateDrag()
    {
        dragStarted = false;
    }

    private void Update()
    {
        if (dragStarted)
        {
            dragTime -= Time.deltaTime;
            if (dragTime > 0)
            {
                Vector3 newPosition = wave.position + distanceFromWave;
                characterController.Move(newPosition - transform.position);
            }
            else
            {
                dragStarted = false;
                dragTime = initialdragTime;
            }
        }
    }
}


