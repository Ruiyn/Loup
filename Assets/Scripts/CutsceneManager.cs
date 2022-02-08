using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneManager : MonoBehaviour
{
    public Animator npcMovement;
    public static CutsceneManager cutsceneManager;


    private void Awake()
    {
        if (cutsceneManager == null)
        {
            cutsceneManager = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MoveNPC()
    {
        npcMovement.SetBool("enterKibu", true);
    }
}
