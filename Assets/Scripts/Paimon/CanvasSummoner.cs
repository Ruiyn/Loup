using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Paimon
{
    public class CanvasSummoner : MonoBehaviour
    {
        public CanvasObjectInstantiator paimonBrain;
        public GameObject cutscene;

        // Start is called before the first frame update
        void Start()
        {
            paimonBrain = FindObjectOfType<CanvasObjectInstantiator>();
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void CutsceneStart()
        {
            paimonBrain.StartCutscene(cutscene);
        }
    }
}
