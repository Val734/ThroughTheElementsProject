using UnityEngine.SceneManagement;
using UnityEngine;
using static UnityEditor.FilePathAttribute;

public class GameNewOrContinue : MonoBehaviour
{
    private string PlayerPrefLocationX;
    private string PlayerPrefLocationY;
    private string PlayerPrefLocationZ;
    private string scene;
    [SerializeField] float defaultvalue = -999;


    private void Awake()
    {
        scene = PlayerPrefs.GetString("NombreDeTuPlayerPref");
        PlayerPrefLocationX = PlayerPrefs.GetString("PlayerPrefLocationX");
        PlayerPrefLocationY = PlayerPrefs.GetString("PlayerPrefLocationY");
        PlayerPrefLocationZ = PlayerPrefs.GetString("PlayerPrefLocationZ");


    }
    public void NewGame()
    {
        PlayerPrefs.SetFloat(PlayerPrefLocationX, defaultvalue);
        PlayerPrefs.SetFloat(PlayerPrefLocationY, defaultvalue);
        PlayerPrefs.SetFloat(PlayerPrefLocationZ, defaultvalue);

        PlayerPrefs.Save();
    }

    public void Continue()
    {
        if (PlayerPrefs.GetFloat(PlayerPrefLocationX) != defaultvalue)
        {
            if (!string.IsNullOrEmpty(scene))
            {
                SceneManager.LoadScene(scene);
            }
            else
            {
                Debug.LogError("El nombre de la escena no está definido en PlayerPrefs.");
            }
        }
            
    }
}