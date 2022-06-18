using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Vapula
{
    public class TeleporterCelestinaRoom : MonoBehaviour
    {
        public GameObject cutsceneObject;
        public UnityEvent startCutscene;

        public delegate void OnEvent();
        public static event OnEvent eventStarted;

        // Start is called before the first frame update
        void Start()
        {
            cutsceneObject = GameObject.FindGameObjectWithTag("Cutscene");
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void enableCutscene()
        {

        }
    }
}
