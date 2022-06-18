using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurtainRemover : MonoBehaviour
{
    public GameObject curtain;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RemoveCurtain()
    {
        curtain.SetActive(false);
    }
}
