using UnityEngine;
using UnityEngine.InputSystem;

public class CannonWorking : MonoBehaviour
{
    public bool isPlayerIn;
    public GameObject target;
    public GameObject cannonBallPrefab;
    public Transform firePoint;
    public LineRenderer lineRenderer;
    public GameObject aimingSystem;

    private const int N_TRAJECTORY_POINTS = 10;

    public Camera _cam;
    public GameObject MainCamera;
    private bool _pressingMouse = false;

    private Vector3 _initialVelocity;

    private GameObject Player;

    public bool haveGravity;


    

    void Start()
    {
        MainCamera=Camera.main.gameObject;
        isPlayerIn = false;
        lineRenderer.positionCount = N_TRAJECTORY_POINTS;
        lineRenderer.enabled = false;
        _cam.gameObject.SetActive(false);
        aimingSystem.SetActive(false);
    }

    void Update()
    {
        if(!haveGravity)
        {
            if (isPlayerIn)
            {

                Player.SetActive(false);
                if (Input.GetMouseButtonDown(0))
                {
                    _pressingMouse = true;
                    lineRenderer.enabled = true;
                }
                if (Input.GetMouseButtonUp(0))
                {
                    _pressingMouse = false;
                    lineRenderer.enabled = false;
                    _Fire();
                }

                if (_pressingMouse)
                {
                    // coordinate transform screen > world
                    Vector3 mousePos = _cam.ScreenToWorldPoint(Input.mousePosition);

                    // look at
                    transform.LookAt(target.transform);

                    _initialVelocity = target.transform.position - firePoint.position;

                    _UpdateLineRenderer();
                }
            }
        }
        
        
        
    }

    private void _Fire()
    {
        if (!haveGravity)
        {
            // instantiate a cannon ball
            GameObject cannonBall = Instantiate(cannonBallPrefab, firePoint.position, Quaternion.identity);
            // apply some force
            Rigidbody rb = cannonBall.GetComponent<Rigidbody>();
            rb.AddForce(_initialVelocity, ForceMode.Impulse);
            cannonBall.GetComponent<CannonBall>().Player = Player;
            cannonBall.GetComponent<CannonBall>().Cannon = gameObject;
            aimingSystem.SetActive(false);
            isPlayerIn = false;
            MainCamera.SetActive(true);
        }

            

    }

    private void _UpdateLineRenderer()
    {
        Vector3[] positions = new Vector3[N_TRAJECTORY_POINTS];

        Vector3 currentPosition = firePoint.position;
        Vector3 currentVelocity = _initialVelocity;

        float timeStep = 0.2f; // Tamaño de paso para calcular la trayectoria
        for (int i = 0; i < N_TRAJECTORY_POINTS; i++)
        {
            // Calcular la nueva posición y velocidad utilizando la fórmula de movimiento uniformemente acelerado
            currentPosition += currentVelocity * timeStep;
            currentVelocity += Physics.gravity * timeStep;

            positions[i] = currentPosition;
        }

        // Establecer los puntos en el LineRenderer
        lineRenderer.SetPositions(positions);
    }


    
    public void CannonPlayer(GameObject other)
    {
        if (!haveGravity)
        {
            Debug.Log("Hola");
            MainCamera.SetActive(false);
            Player = other.gameObject;
            _cam.gameObject.SetActive(true);
            aimingSystem.SetActive(true);
            isPlayerIn = true;
        }
        
    }

   
}
