using CVVTuber.VRM;
using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class thresholdSlider : MonoBehaviour
{
    Slider SliderButton;

    private void Start()
    {
        SliderButton = GetComponent<Slider>();
        SliderButton.value = 0.85f;
    }


}
