using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Kino;

public class GlitchController : MonoBehaviour
{

    public DigitalGlitch digitalGlitch;
    public AnalogGlitch analogGlitch;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void IncreaseGlitches()
    {
        analogGlitch.colorDrift = +0.2f;
        analogGlitch.scanLineJitter = +0.2f;
        digitalGlitch.intensity = +0.1f;
    }

    public void DecreaseGlitches()
    {
        analogGlitch.colorDrift = -0.2f;
        analogGlitch.scanLineJitter = -0.2f;
        digitalGlitch.intensity = -0.1f;
    }

    public void ResetGlitches()
    {
        analogGlitch.colorDrift = 0;
        analogGlitch.scanLineJitter = 0;
        digitalGlitch.intensity = 0;
    }

    public void SetGlitchIntensity(float quantity)
    {
        analogGlitch.colorDrift = quantity * 2;
        analogGlitch.scanLineJitter = quantity * 2;
        //digitalGlitch.intensity = quantity;
    }

}
