using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class AutoSavedCheckBox_ForMouseAxisInvert : AutoSavedCheckBox
{
    public enum AxisToInvert {X,Y };
    [SerializeField] CinemachineFreeLook cinemachineFreeLook;
    [SerializeField] AxisToInvert  axisToInvert=AxisToInvert.X;
    protected override void InternalValueChanged(bool newValue)
    {
        // AxisState axisStateToChange = (axisToInvert==AxisToInvert.X) ?
        //     cinemachineFreeLook.m_XAxis : cinemachineFreeLook.m_YAxis; 
        // axisStateToChange.m_InvertInput = newValue;
        if (axisToInvert == AxisToInvert.X)
        {
            cinemachineFreeLook.m_XAxis.m_InvertInput = newValue;
        }
        else
        {
            cinemachineFreeLook.m_YAxis.m_InvertInput = newValue;
        }
    }
}
