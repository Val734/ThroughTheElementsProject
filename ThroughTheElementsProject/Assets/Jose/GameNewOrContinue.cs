using UnityEngine.SceneManagement;
using UnityEngine;

public class GameNewOrContinue : MonoBehaviour
{
    private string scene;
    private float defaultvalue = -999f;


    private void Awake()
    {
        scene = PlayerPrefs.GetString("PlayerPrefWitchScene");


    }
    public void NewGame()
    {
        //Debug.LogError("entra");

        PlayerPrefs.SetFloat("PlayerPrefLocationX", defaultvalue);
        PlayerPrefs.SetFloat("PlayerPrefLocationY", defaultvalue);
        PlayerPrefs.SetFloat("PlayerPrefLocationZ", defaultvalue);
        PlayerPrefs.SetString("PlayerPrefWitchScene", "Ninguna");

        PlayerPrefs.Save();
        ViewPlayerPref();

    }

    public void Continue()
    {
        if(scene!= "Ninguna")
        {
            if (!string.IsNullOrEmpty(scene))
            {
                SceneManager.LoadScene(scene);
            }
            else
            {
                Debug.LogError("El nombre de la escena no está definido en PlayerPrefs.");
            }
            ViewPlayerPref();
        }



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