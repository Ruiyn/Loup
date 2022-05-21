using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utage;
using MoreMountains.Feedbacks;

public class ActionManagerA : MonoBehaviour
{
    public GameObject cabinetOriginal;
    public GameObject cabinetMoved;
    public MMFeedbacks fade;


    private void Move()
    {
        StartCoroutine("MoveObject");

    }

    IEnumerator MoveObject()
    {
        fade.PlayFeedbacks();
        yield return new WaitForSeconds(1f);
        cabinetOriginal.SetActive(false);
        cabinetMoved.SetActive(true);
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
