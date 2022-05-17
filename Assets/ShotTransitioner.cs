using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ShotTransitioner : MonoBehaviour
{

    public CinemachineVirtualCamera shotCamera;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        shotCamera.Priority = 11;
    }


    private void OnTriggerLeave(Collider other)
    {
        shotCamera.Priority = 9;
    }

}
