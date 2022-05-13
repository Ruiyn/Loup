using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhoneManager : MonoBehaviour
{

    [SerializeField] Material phoneMat;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void turnOn()
    {
        Debug.Log("Activation");
        //phoneMat = this.GetComponent<Renderer>().material;
        phoneMat.SetColor("_EmissionColor", Color.white);
    }
}
