using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallFader : MonoBehaviour
{

    public Material baseMaterial;
    public Material transparentMaterial;
    public GameObject wall;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Player has entered the trigger");

        if (other.gameObject.tag == "Player")
        {
            wall.GetComponent<Renderer>().material = transparentMaterial;
        }
    }

    void OnTriggerExit(Collider other)
    {
        Debug.Log("Player has exited the trigger");

        if (other.gameObject.tag == "Player")
        {
            wall.GetComponent<Renderer>().material = baseMaterial;
        }
    }
}
