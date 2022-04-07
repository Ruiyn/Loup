using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Ares
{
    public class HealthUpdater : MonoBehaviour
    {
        Text text;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void TextUpdater()
        {
            text.text
                
                
                
                
               = GetComponent<Actor>().HP.ToString();
        }
    }
}
