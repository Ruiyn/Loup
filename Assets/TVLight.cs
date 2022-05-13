using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TVLight : MonoBehaviour
{
    [SerializeField] Light tvLight;
    [SerializeField] Color color1;
    [SerializeField] Color color2;
    [SerializeField] float duration;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float t = Mathf.PingPong(Time.time, duration) / duration;
        tvLight.color = Color.Lerp(color1, color2, t);
        tvLight.intensity = Mathf.Lerp(1f, 2f, t);
    }

}
