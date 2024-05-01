using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class AutoSavedSlider_ForBrightness : AutoSavedSlider
{

    protected override void InternalValueChanged(float newValue)
    {
        Screen.brightness = newValue;
    }

}
