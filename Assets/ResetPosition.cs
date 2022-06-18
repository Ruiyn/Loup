using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetPosition : MonoBehaviour
{
    public Vector3 originalPos;

    void Start()
    {
        originalPos = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);
        //alternatively, just: originalPos = gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ResetObjectPosition()
    {
        gameObject.transform.position = originalPos;
        gameObject.SetActive(false);
    }
}
