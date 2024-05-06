using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] InputActionReference Pause;
    public GameObject pausa;
    public GameObject HUD;
    public GameObject settings;

    private void Start()
    {
        settings.SetActive(false);
        pausa.SetActive(false);
        Time.timeScale = 1f;

    }
    private void OnEnable()
    {
        Pause.action.Enable();
        Pause.action.performed += OpenPauseMenu;
    }

    private void OnDisable()
    {
        Pause.action.Disable();
        Pause.action.performed -= OpenPauseMenu;

    }

    public void OpenPauseMenu(InputAction.CallbackContext context)
    {
        if (!pausa.activeSelf)
        {
            pausa.SetActive(true);
            Time.timeScale = 0f;
        }
        else
        {
            HUD.SetActive(true);
            settings.SetActive(false);
            pausa.SetActive(false);
            Time.timeScale = 1f;
        }
    }

    public void ClosePauseMenu()
    {
        pausa.SetActive(false);
        Time.timeScale = 1f;
    }

    public void OnRestartGame()
    {
        Debug.Log("aqui reseteas");
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void OnResume()
    {
        Time.timeScale = 1f;
    }
}
