using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AutoSavedCheckBox : MonoBehaviour

{
    [SerializeField] string playerPrefsKey;
    [SerializeField] bool defaultValue =false;
    //float saveValue;

    //Component Cache
    Toggle toggle; 

    private void Awake()
    {
        toggle = GetComponentInChildren<Toggle>();
        toggle.onValueChanged.AddListener(OnValueChanged);

       //int defaultValueInt = 0;
       //if (defaultValue == true)
       //{
       //    defaultValueInt = 1;
       //}
       //
       //if (PlayerPrefs.GetInt(playerPrefsKey, defaultValueInt) == 1)
       //{
       //    toggle.isOn = true;
       //}
       //else { toggle.isOn = false; } es lo mismo que línea de abajo, con menos lo mismo 

        toggle.isOn = PlayerPrefs.GetInt(playerPrefsKey, defaultValue ? 1:0)==1;//nos devuelve el valor de la key
    }
    private void Start()
    {
        InternalValueChanged(toggle.isOn);
    }

    void OnValueChanged(bool newValue)
    {
        int newValueInt = 0; 
        if(newValue==true)
        {
            newValueInt = 1;
        }
        PlayerPrefs.SetInt(playerPrefsKey, newValueInt);
        PlayerPrefs.Save();//guarda en el disco las preferencias

        InternalValueChanged(newValue);
    }
    protected virtual void InternalValueChanged(bool newValue) { Debug.Log($"HOLII Check Box {newValue}"); }
    //virtual-_> funcion que s ele puede dar un código diferente cuando se hereda
}
