using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utage;
using UtageExtensions;


public class StatsManager : MonoBehaviour
{

    public static AdvEngine engine;
    public GameObject advEngine;
    public StatsManager instance;

    private void Awake()
    {



        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }

        engine = FindObjectOfType<AdvEngine>();
    }

    // Start is called before the first frame update
    public void Start()
    {

        AssignStats();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AssignStats()
    {
        StartCoroutine("AssignStatus");
    }

    public static void UpdateStats(string Stat, int Amount)
    {
        engine.Param.GetParameter(Stat);
        engine.Param.SetParameter(Stat, Amount);
    }

    IEnumerator AssignStatus()
    {
        yield return new WaitForSeconds(0.5f);
        engine.Param.GetParameter("strength");

        //engine.Param.SetParameterInt("strength", statStrength);
    }
}
