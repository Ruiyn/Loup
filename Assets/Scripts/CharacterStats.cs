using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{

    [SerializeField] public static int Str;
    public Hashtable Stats = new Hashtable();


    // Start is called before the first frame update
    void Start()
    {
        Str = 3;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void updateStats(ref int Stat, int Amount)
    {

        Debug.Log("Stat " + Stat + " updated by " + Amount);
    }
}
