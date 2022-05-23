using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class AM_Fan : MonoBehaviour
{
    public GameObject cutsceneDirector;


    private void PlayCutscene()
    {
        cutsceneDirector.SetActive(true);
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
