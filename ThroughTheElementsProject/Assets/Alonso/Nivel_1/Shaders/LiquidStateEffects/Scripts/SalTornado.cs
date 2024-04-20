using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class SalTornado : MonoBehaviour
{
    [SerializeField] VisualEffect _vfx;

    float initialHeight;

    float height;
    float time;

    public bool PROBAR;

    bool Initiated;

    private void Awake()
    {
        if(_vfx == null )
        {
            _vfx = GetComponentInChildren<VisualEffect>();
        }
        time = 1;
    }

    private void OnValidate()
    {
        _vfx.Play();
        PROBAR = false;
    }

    private void Update()
    {
        if(!Initiated)
        {
            height = _vfx.GetFloat("Height");
            initialHeight = height;
            Initiated = true;
        }

        if(height < 1.5)
        {
            height = initialHeight;
        }


        time -= Time.deltaTime;
        if(time < 0.65 ) 
        {
            height--;
            _vfx.SetFloat("Height",height);
            time = 1;
        }
    }
}
