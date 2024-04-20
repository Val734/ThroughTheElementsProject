using UnityEngine;

public class WaveBehaviour : MonoBehaviour
{
    [SerializeField] float lifeTime = 10f;
    [SerializeField] float speed = 5f;
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
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            player = other.gameObject;
            player.GetComponent<WaveDragBehaviour>().ActivateWaveDrag(transform);
            Debug.Log("Player Tiggered");
        }
    }
}