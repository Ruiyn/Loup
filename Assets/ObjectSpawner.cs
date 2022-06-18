using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public bool shouldInstantiate;
    public GameObject objectToInstantiate;
    public Transform instantiatePosition;

    // Start is called before the first frame update


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void instantiateObject()
    {
        var newObject = Instantiate(objectToInstantiate, instantiatePosition.position, Quaternion.identity);
        newObject.transform.parent = gameObject.transform;
    }
}
