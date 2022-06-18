using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputReader : MonoBehaviour
{
    public bool hasReadInput = false;
    public GameObject cutscene;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasReadInput)
        {
            if (Input.anyKey)
            {
                cutscene.SetActive(true);
                hasReadInput = true;
            }
        }
    }
}
