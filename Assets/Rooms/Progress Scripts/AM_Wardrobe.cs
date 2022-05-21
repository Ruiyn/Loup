using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utage;

public class AM_Wardrobe : MonoBehaviour
{

    public Animator objectAnimator;

    private void OpenAnimation()
    {
        objectAnimator.SetBool("isOpen", true);
    }

    private void CloseAnimation()
    {
        objectAnimator.SetBool("isOpen", false);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
