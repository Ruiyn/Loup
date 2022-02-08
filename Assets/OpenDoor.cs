using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    // Start is called before the first frame update

    public Animator doorAnim;
    public bool doorOpening;

    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenSlide()
    {
        StartCoroutine("openDoor");
    }

    IEnumerator openDoor()
    {
        doorOpening = true;
        doorAnim.SetBool("isOpen", true);
        yield return new WaitForSeconds(3f);
        doorAnim.SetBool("isOpen", false);
        doorOpening = false;
    }
}
