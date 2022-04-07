using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonHider : MonoBehaviour
{
    [SerializeField]Image image;
    [SerializeField]Text text;

    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
        text = GetComponentInChildren<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        /*if (text.text == "DUMMY ABILITY")
        {
            image.enabled = !image.enabled;
            text.enabled = !text.enabled;
        }
        else
        {
            image.enabled = image.enabled;
            text.enabled = text.enabled;
        }*/
    }
}
