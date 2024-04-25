using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using static PlayerController;
using static UnityEngine.UI.Image;

public class BossBehaviour : MonoBehaviour
{
    public GameObject BossPlatform;
    public GameObject Player;
    [SerializeField] float angularSpeed = 360f;
    private bool isFightingPlayer;
    private Rigidbody rb;
    private float velocidad = 6f;
    private bool isAtaccking;
    public GameObject Visuals;
    private Animator animator;
    private int attackCount;
    private int maxattackCount;
    private bool isDoingSpecialAttack;
    public GameObject[] Point;
    public GameObject Rock;

    private bool secondFase;
    private bool reachFirstPoint;

    public GameObject Timeline2;
    public GameObject Timeline3;
    public GameObject Timeline4;

    public GameObject particleSystem;


    private void Awake()
    {
        attackCount = 0;
        maxattackCount = 4;
        isDoingSpecialAttack = false;
    }
    public enum BossFase
    {
        waiting, fase0, fase1, fase2, fase3
    }
    BossFase fase = BossFase.fase0;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = Visuals.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        //1 fase

        if (BossPlatform.GetComponent<BossPlatform>().playerIsInPlatform && !isFightingPlayer)
        {
            
            StartCoroutine(WaitForFase1() );
        }

        if (fase == BossFase.fase1 && isFightingPlayer && Vector3.Distance(Player.transform.position, transform.position) <= 8 && !isAtaccking && attackCount < maxattackCount && !isDoingSpecialAttack)
        {
            attackCount++;
            Attack();
            isAtaccking = true;



        }

        if (fase == BossFase.fase1 && (isFightingPlayer && Vector3.Distance(Player.transform.position, transform.position) > 8) && !isAtaccking && attackCount < maxattackCount && !isDoingSpecialAttack)
        {
            Debug.Log("Entra");
            Vector3 direccion = (Player.transform.position - transform.position).normalized;

            Vector3 movimiento = direccion * velocidad * Time.deltaTime;

            rb.MovePosition(rb.position + movimiento);
            animator.SetTrigger("RunTrigger");


        }


        if (attackCount >= maxattackCount && !isAtaccking)
        {
            rb.isKinematic = true;
            isDoingSpecialAttack = true;
            SpecialAttack();
            attackCount = 0;

        }
        if (isDoingSpecialAttack)
        {

            fase = BossFase.fase1;

            Vector3 direccion = (Point[0].transform.position - transform.position).normalized;

            Vector3 movimiento = direccion * velocidad * Time.deltaTime;

            transform.position += movimiento;

        }


        if(gameObject.GetComponent<HealthBehaviour>().health<=10)
        {
            secondFase = true;
            Player.GetComponent<PlayerController>().orientationMode = OrientationMode.CameraForward;
        }
        if (secondFase)
        {
            fase = BossFase.fase2;
            BossPlatform.gameObject.transform.GetChild(0).gameObject.SetActive(false);
            BossPlatform.gameObject.transform.GetChild(1).gameObject.SetActive(true);

            Timeline3.SetActive(true);
            animator.SetTrigger("FlyingTrigger");
        }

        //2 fase
        if (fase == BossFase.fase2 && Vector3.Distance(transform.position,Point[1].transform.position)>1)
        {
            rb.isKinematic = true;
            Vector3 direccion = (Point[1].transform.position - transform.position).normalized;

            Vector3 movimiento = direccion * (velocidad*5) * Time.deltaTime;

            transform.position += movimiento;
            particleSystem.SetActive(true);
        }
        else if(fase == BossFase.fase2 && Vector3.Distance(transform.position, Point[1].transform.position) < 1)
        {
            particleSystem.SetActive(false);
            fase=BossFase.fase3;
        }
        UpdateOrientation();
    }

    private void UpdateOrientation()
    {
        Vector3 desiredOrientation = transform.forward;

        switch (fase)
        {
            case BossFase.fase0:
                desiredOrientation = transform.forward;
                break;
            case BossFase.fase1:
                desiredOrientation = Player.transform.position - transform.position;
                break;
            case BossFase.fase2:
                desiredOrientation = Point[1].transform.position - transform.position;
                break;
            case BossFase.fase3:
                desiredOrientation = Player.transform.position - transform.position;
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

    private void Attack()
    {

        
        StartCoroutine(AttackCoroutine());
    }
    IEnumerator AttackCoroutine()
    {
        yield return new WaitForSeconds(0.4f);
        int random = Random.Range(1, 3);
        fase = BossFase.fase0;
        if (random == 1)
        {
            animator.SetTrigger("UpperCutTrigger");
        }
        else if (random == 2)
        {
            animator.SetTrigger("JumpAttackTrigger");

        }
        
        yield return new WaitForSeconds(0.4f);
        rb.AddForce((Player.transform.position - gameObject.transform.position) * 50*5, ForceMode.Impulse);
        yield return new WaitForSeconds(0.4f);
        rb.constraints = RigidbodyConstraints.FreezePositionX |
                         RigidbodyConstraints.FreezePositionZ |
                         RigidbodyConstraints.FreezeRotationX |
                         RigidbodyConstraints.FreezeRotationY |
                         RigidbodyConstraints.FreezeRotationZ;
        yield return new WaitForSeconds(2f);
        
        yield return new WaitForSeconds(1.8f);
        fase = BossFase.fase1;
        isAtaccking = false;
        rb.constraints = RigidbodyConstraints.FreezeRotationX |
                         RigidbodyConstraints.FreezeRotationY |
                         RigidbodyConstraints.FreezeRotationZ;

    }

    private void SpecialAttack()
    {
        Timeline2.SetActive(true);
        animator.SetTrigger("HangingTrigger");
        Player.GetComponent<PlayerController>().orientationMode = OrientationMode.CameraForward;
        StartCoroutine(ChargeSpecialAttack());

    }
    IEnumerator ChargeSpecialAttack()
    {
        particleSystem.SetActive(true);
        yield return new WaitForSeconds(8f);
        animator.SetTrigger("ChargeTrigger");
        GameObject rock = Instantiate(Rock, new Vector3(Point[0].transform.position.x, Point[0].transform.position.y + 5, Point[0].transform.position.z), Quaternion.identity);
        rock.GetComponent<Fracture>().Boss = gameObject;
        
        StartCoroutine(RestoreBoss(15,rock));


    }

    public void StopSpecialAttack()
    {
        rb.isKinematic = false;     
        isDoingSpecialAttack = false;
        gameObject.GetComponent<HealthBehaviour>().Damage(5);
        particleSystem.SetActive(false);



    }
    IEnumerator RestoreBoss(int seconds,GameObject rock)
    {
        yield return new WaitForSeconds(seconds);
        if(rock != null)
        {
            Destroy(rock);
            gameObject.GetComponent<HealthBehaviour>().Heal(15);
            StopSpecialAttack();

        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("CannonBall") && fase == BossFase.fase2 || fase == BossFase.fase3)
        {
            rb.isKinematic = false;
            gameObject.GetComponent<HealthBehaviour>().Damage(500);
            animator.SetTrigger("DieTrigger");
            Timeline4.SetActive(true);


        }
    }


    IEnumerator WaitForFase1()
    {
        yield return new WaitForSeconds(2f);
        fase = BossFase.fase1;
        isFightingPlayer = true;
    }
}
