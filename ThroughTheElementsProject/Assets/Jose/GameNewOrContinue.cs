/*using UnityEngine.SceneManagement;
using UnityEngine;
using System.ComponentModel;

public class GameNewOrContinue : MonoBehaviour
{
    private string scene;
    private float defaultValue = -999f;

    private void Awake()
    {
        scene = PlayerPrefs.GetString("PlayerPrefWitchScene");
    }

    public void NewGame()
    {
        PlayerPrefs.SetFloat("PlayerPrefLocationX", defaultValue);
        PlayerPrefs.SetFloat("PlayerPrefLocationY", defaultValue);
        PlayerPrefs.SetFloat("PlayerPrefLocationZ", defaultValue);
        PlayerPrefs.SetString("PlayerPrefWitchScene", "Ninguna");

        PlayerPrefs.Save();
        ViewPlayerPref();
    }

    public void Continue()
    {
        if (scene != "Ninguna")
        {
            if (!string.IsNullOrEmpty(scene))
            {
                SceneManager.sceneLoaded += OnSceneLoaded;
                SceneManager.LoadScene(scene);
            }
            else
            {
                Debug.LogError("El nombre de la escena no está definido en PlayerPrefs.");
            }
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        GameObject spawnManager = GameObject.Find("SpawnManager");
        if (spawnManager != null)
        {
            SpawnManager spawnManagerComponent = spawnManager.GetComponent<SpawnManager>();
            if (spawnManagerComponent != null)
            {
               // spawnManagerComponent.SetPlayerPosition();
            }
            else
            {
                Debug.LogWarning("El componente SpawnManager no está adjunto al GameObject 'SpawnManager'.");
            }
        }
        else
        {
            Debug.LogWarning("No se encontró el GameObject 'SpawnManager' en la nueva escena.");
        }

        SceneManager.sceneLoaded -= OnSceneLoaded;
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
}*/