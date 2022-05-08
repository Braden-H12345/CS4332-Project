using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderAdjust : MonoBehaviour
{
    [SerializeField] Slider stunnedSlider;
    private float timeElapsed = 0f;

    bool resetBool = false;
    // Start is called before the first frame update
    void Start()
    {
        stunnedSlider.value = 1f;
        
    }

    // Update is called once per frame
    void Update()
    {
        if(timeElapsed < 1)
        {
            stunnedSlider.value -= Time.deltaTime;
            timeElapsed += Time.deltaTime;
        }
        if(stunnedSlider.value == 0f)
        {
            resetBool = true;
        }
        if(resetBool)
        {
            timeElapsed = 0f;
            stunnedSlider.value = 1f;
            resetBool = false;
        }
    }
}
