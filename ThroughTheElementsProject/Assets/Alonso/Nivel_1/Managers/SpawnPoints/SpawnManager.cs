using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events; 

public class SpawnManager : MonoBehaviour
{
    [Header("Spawn Points")]
    [SerializeField] Transform initialSpawnPoint;
    [SerializeField] public Transform currentSpawnPoint;
    [SerializeField] List<Transform> SpawnPoints;

    [Header("Player")]
    [SerializeField] Transform Player; //AQUÍ HABRÁ QUE ARRASTRAR AL PLAYER PARA QUE PODAMOS MOVERLO CADA VEZ


    [Header("PRUEBA")]
    public bool PRUEBAS;

    public string nameOfScene;

    [SerializeField] string playerPrefLocationX;
    [SerializeField] string playerPrefLocationY;
    [SerializeField] string playerPrefLocationZ;
    [SerializeField] string playerPrefWitchScene;

    private float defaultvalue = -999f;

    private void OnValidate()
    {
        if(PRUEBAS) 
        {
            //RespawnPlayer();
        }
    }


    private void Awake()
    {
        if(PlayerPrefs.GetFloat("PlayerPrefLocationX")!=defaultvalue)
        {
            Player.transform.position = new Vector3(PlayerPrefs.GetFloat("PlayerPrefLocationX"), PlayerPrefs.GetFloat("PlayerPrefLocationY"), PlayerPrefs.GetFloat("PlayerPrefLocationZ"));
        }
        for (int i = 0; i < transform.childCount; i++)
        {
            SpawnPoints.Add(transform.GetChild(i));
        }

        initialSpawnPoint = transform.GetChild(0);
        currentSpawnPoint = transform.GetChild(0);

        //slider.value = PlayerPrefs.GetFloat(playerPrefsKey, defaultvalue);
        ViewPlayerPref();
        PlayerPrefs.SetString(playerPrefWitchScene, nameOfScene);
    }

    

    public void ChangeSpawnPoint(Transform newSpawnPoint)
    {
        currentSpawnPoint = newSpawnPoint;
        OnValueChange(currentSpawnPoint);
        

    }


   // public void RespawnPlayer()
   // {
   //     Player.gameObject.SetActive(false);
   //     Player.gameObject.transform.position = currentSpawnPoint.transform.position;
   //     Player.gameObject.GetComponent<PlayerController>().RestorePlayer();
   //     Player.gameObject.SetActive(true);
   // }

    public void DiePlayer()
    {
        Player.gameObject.SetActive(false);
        Player.gameObject.GetComponent<HealthBehaviour>().RestoreHealth();
        Player.gameObject.transform.position = currentSpawnPoint.transform.position;
        Player.gameObject.GetComponent<PlayerController>().RestorePlayer();
        Player.gameObject.SetActive(true);
    }

    public void OnValueChange(Transform location)
    {

        PlayerPrefs.SetFloat(playerPrefLocationX, location.transform.position.x);
        PlayerPrefs.SetFloat(playerPrefLocationY, location.transform.position.y);
        PlayerPrefs.SetFloat(playerPrefLocationZ, location.transform.position.z);
        
        PlayerPrefs.Save();
        ViewPlayerPref();

    }

    private static void ViewPlayerPref()
    {
        if (PlayerPrefs.HasKey("PlayerPrefLocationX"))
        {
            // Obtener el valor de un PlayerPrefs
            float valor = PlayerPrefs.GetFloat("PlayerPrefLocationX");
            Debug.Log("El valor de PlayerPrefLocationX es: " + valor);
        }
        else
        {
            Debug.Log("PlayerPrefLocationX no existe en PlayerPrefs");
        }


        if (PlayerPrefs.HasKey("PlayerPrefLocationY"))
        {
            // Obtener el valor de un PlayerPrefs
            float valor = PlayerPrefs.GetFloat("PlayerPrefLocationY");
            Debug.Log("El valor de PlayerPrefLocationY es: " + valor);
        }
        else
        {
            Debug.Log("PlayerPrefLocationY no existe en PlayerPrefs");
        }


        if (PlayerPrefs.HasKey("PlayerPrefLocationZ"))
        {
            // Obtener el valor de un PlayerPrefs
            float valor = PlayerPrefs.GetFloat("PlayerPrefLocationZ");
            Debug.Log("El valor de PlayerPrefLocationZ es: " + valor);
        }
        else
        {
            Debug.Log("PlayerPrefLocationZ no existe en PlayerPrefs");
        }


        if (PlayerPrefs.HasKey("PlayerPrefWitchScene"))
        {
            // Obtener el valor de un PlayerPrefs
            string valor = PlayerPrefs.GetString("PlayerPrefWitchScene");
            Debug.Log("El valor de PlayerPrefWitchScene es: " + valor);
        }
        else
        {
            Debug.Log("PlayerPrefWitchScene no existe en PlayerPrefs");
        }
    }
}
