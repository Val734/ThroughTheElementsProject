using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class AutoSavedSlider_ForMouseAxisSensibility : AutoSavedSlider
{
    public enum Axis { X, Y };
    [SerializeField] CinemachineFreeLook cinemachineFreeLook;
    [SerializeField] Axis axisToInvert = Axis.X;
    [SerializeField] float minSpeed = 1f;
    [SerializeField] float maxSpeed = 5f;

    protected override void InternalValueChanged(float newValue)
    {
        //Assuming NewValue is in the range of [0f...1f]
        float valueToSet = Mathf.Lerp(minSpeed, maxSpeed, newValue);//lerp; interpolación linear, regla de 3 básicamente

        if (axisToInvert == Axis.X)
        {
            cinemachineFreeLook.m_XAxis.m_MaxSpeed = valueToSet;
        }
        else
        {
            cinemachineFreeLook.m_YAxis.m_MaxSpeed = valueToSet;
        }
    }
}
