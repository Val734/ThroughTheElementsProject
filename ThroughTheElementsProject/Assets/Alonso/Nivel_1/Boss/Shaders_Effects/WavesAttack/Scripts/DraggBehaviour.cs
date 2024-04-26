using UnityEngine;

public class DraggBehaviour : MonoBehaviour
{
    [SerializeField] float lifeTime = 10f;
    [SerializeField] float speed = 5f;
    [SerializeField] float dragTime;
    Rigidbody rb;

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
            player.GetComponent<DragabbleBehaviour>().DesactivateDrag();    
            Destroy(gameObject);
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