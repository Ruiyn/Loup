using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogOfWar : MonoBehaviour
{

    public Animator fogOfWar;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("A");
        hideFog();
    }

    public void hideFog()
    {
        StartCoroutine("fadeFog");
    }

    IEnumerator fadeFog()
    {
        this.fogOfWar.SetBool("isVisible", true);
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
