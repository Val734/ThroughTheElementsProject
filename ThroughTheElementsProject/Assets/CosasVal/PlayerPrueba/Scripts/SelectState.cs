using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SelectState : MonoBehaviour
{
    public GasStateBehaviour GasStateBehaviour;
    public LiquidStateBehaviour LiquidStateBehaviour;
    public SolidStateBehaviour SolidStateBehaviour;
    public PlasmaStateBehaviour PlasmaStateBehaviour;

    [SerializeField] InputActionReference GasAction;
    [SerializeField] InputActionReference LiquidAction;
    [SerializeField] InputActionReference SolidAction;
    [SerializeField] InputActionReference PlasmaAction;

    //public MeshFilter GasModel;
    //public MeshFilter LiquidModel;
    //public MeshFilter SolidModel;
    //public MeshFilter PlasmaModel;
    
    public GameObject GasAura;
    public GameObject LiquidAura;
    public GameObject SolidAura;
    public GameObject PlasmaAura;

    //public Material gasMaterial;
    //public Material redMaterial;
    //public Material blueMaterial;
    //public Material yellowMaterial;
    //
    private Animator animator;


    private enum State
    {
        gas,
        liquid,
        solid,
        plasma
    }
    State state;

    private SkinnedMeshRenderer skinnedMeshRenderer;

    public void Awake()
    {
        animator = GetComponentInChildren<Animator>();
    }

    private void OnEnable()
    {
        GasAction.action.Enable();
        LiquidAction.action.Enable();
        SolidAction.action.Enable();
        PlasmaAction.action.Enable();
    }

    private void OnDisable()
    {
        GasAction.action.Disable();
        LiquidAction.action.Disable();
        SolidAction.action.Disable();
        PlasmaAction.action.Disable();
    }

    void Start()
    {
        skinnedMeshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
    }

    void Update()
    {
        if (GasAction.action.triggered)
        {

           ActiveState(true, false, false, false);
            GasAura.SetActive(true);
            LiquidAura.SetActive(false) ;
            SolidAura.SetActive(false);
            PlasmaAura.SetActive(false);

            animator.SetTrigger("ChangeStateTrigger");
            

        }
        else if (LiquidAction.action.triggered)
        {
            //SetState(LiquidMesh, blueMaterial);
            ActiveState(false, true, false, false);
            GasAura.SetActive(false);
            LiquidAura.SetActive(true);
            SolidAura.SetActive(false);
            PlasmaAura.SetActive(false);
            animator.SetTrigger("ChangeStateTrigger");

        }
        else if (SolidAction.action.triggered)
        {
            //SetState(SolidMesh, redMaterial);
            ActiveState(false, false, true, false);
            GasAura.SetActive(false);
            LiquidAura.SetActive(false);
            SolidAura.SetActive(true);
            PlasmaAura.SetActive(false);
            animator.SetTrigger("ChangeStateTrigger");

        }
        else if (PlasmaAction.action.triggered)
        {
            //SetState(PlasmaMesh, yellowMaterial);
            ActiveState(false, false, false, true);
            GasAura.SetActive(false);
            LiquidAura.SetActive(false);
            SolidAura.SetActive(false);
            PlasmaAura.SetActive(true);

            animator.SetTrigger("ChangeStateTrigger");

        }
    }

    //void SetState(Mesh mesh, Material material)
    //{
    //    skinnedMeshRenderer.sharedMesh = mesh;
    //    skinnedMeshRenderer.material = material;
    //}

    public void ActiveState(bool gas, bool liquid, bool solid, bool plasma)
    {
        GasStateBehaviour.enabled = gas;
        LiquidStateBehaviour.enabled = liquid;
        SolidStateBehaviour.enabled = solid;
        PlasmaStateBehaviour.enabled = plasma;
    }
}
