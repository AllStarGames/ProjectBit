using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    Slider slider;

    void Start()
    {
        slider = GetComponent<Slider>();
    }
	void Update ()
    {
        slider.value = transform.parent.parent.GetComponent<MainMenuController>().LoadingProgress();
        if(slider.value == 0.9f)
        {
            slider.value = 1.0f;
        }
	}
}
