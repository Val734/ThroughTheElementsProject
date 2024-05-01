using UnityEngine;

public class DraggBehaviour : MonoBehaviour
{
    [SerializeField] float lifeTime = 10f;
    [SerializeField] float speed = 5f;
    [SerializeField] float dragTime;
    Rigidbody rb;

    // ESTE BOOLEANO ES PARA PODER DEFINIR QUE OBJETOS SE DESTRUIRAN AL TERMINAR EL TIEMPO Y CUALES SIMPLEMENTE SE DESACTIVARÁN 
    [SerializeField] bool destructible;

    GameObject player;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = transform.forward * speed;
    }

    private void Update()
    {
        lifeTime -= Time.deltaTime;
        if (lifeTime < 0)
        {
            if(player != null) 
            {
                player.GetComponent<DragabbleBehaviour>().DesactivateDrag();    
            }
            if(destructible)
            {
                Destroy(gameObject);
            }
            else//(!destructible)
            {
                gameObject.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            player = other.gameObject;
            player.GetComponent<DragabbleBehaviour>().ActivateDrag(transform, dragTime);

        }
    }
}