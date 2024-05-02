using System.Collections;
using UnityEngine;

public class EnemyController_Freeze : EnemyController
{
    [Header("Atacking Settings")]
    [SerializeField] GameObject hitCollider;

    GameObject player;
    float localSpeed = 2f;
    private Animator animator;
    private bool isAlive;
    private float disappear = 5f;
    private bool goToPlayerForAttack;
    [SerializeField] ParticleSystem frost;

    [SerializeField] GameObject soundManager;


    enum Look
    {
        normal,player
    }

    Look look=Look.normal;

    protected override void ChildAwake()
    {
        animator = GetComponentInChildren<Animator>();
        isAlive= true;
        goToPlayerForAttack = true;
        frost = GetComponentInChildren<ParticleSystem>();
        frost.Stop();
    }

    protected override void ChildUpdate()
    {
        if (player != null) 
        {
            if(player.GetComponent<PlayerController>().IsFreezed() )
            {
                state = State.Wandering;
                playerFreezed = true;

            }
            else
            {
                playerFreezed = false;

            }
        }


        if (isAlive==false)
        {
            state= State.Dead;
        }

        if (!isAlive)
        {
            disappear -= Time.deltaTime;
            if (disappear < 0)
            {
                Debug.Log("activar particulas para que la palme y se vaya alv");
                Destroy(gameObject);
            }
        }
        UpdateOrientation();

       // Debug.Log("el sevaaaaaaaaaaaaaaaaaaa" + goToPlayerForAttack); 
    }

    protected override float ReturnSpeed()
    {
        return localSpeed;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            player = other.gameObject;

            if(goToPlayerForAttack)
            {
                goToPlayerForAttack = false;

                StartCoroutine(waitToAttack());
            }
        }
    }

    IEnumerator waitToAttack()
    {
        localSpeed = 0;
        // hacer que mire al player para atacar!!

        look = Look.player;
        //yield return new WaitForSeconds(1);
        
        animator.SetTrigger("FrozenAttack");
        frost.Play();

        hitCollider.gameObject.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        hitCollider.gameObject.SetActive(false);
        look= Look.normal;
        localSpeed = 2;
        yield return new WaitForSeconds(2f);

        goToPlayerForAttack = true;

        frost.Stop();


    }
    public void Hurt()
    {
        animator.SetTrigger("Hurt");
    }
    public void Die()
    {
        animator.SetTrigger("Die");
        isAlive= false;

    }
    private void UpdateOrientation()
    {
        Vector3 desiredOrientation = Vector3.forward;

        switch (look)
        {
            case Look.normal:
                desiredOrientation = transform.forward;
                break;
            case Look.player:
                desiredOrientation =player.transform.position - transform.position;
                break;
        }

        desiredOrientation.y = 0f;
        float angularDistance =
            Vector3.SignedAngle(transform.forward, desiredOrientation,
        Vector3.up);

        float angleBecauseSpeed = angularSpeed * Time.deltaTime;
        float remainingAngle = Mathf.Abs(angularDistance);

        float angleToApply =
        Mathf.Sign(angularDistance) * Mathf.Min(angleBecauseSpeed,
        remainingAngle);


        Quaternion rotationToApply =
        Quaternion.AngleAxis(angleToApply, Vector3.up);

        transform.rotation = rotationToApply * transform.rotation;
    }

}
