using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class AM_Fan : MonoBehaviour
{
    public PlayableDirector cutsceneDirector;


    private void PlayCutscene()
    {
        cutsceneDirector.Play();
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
