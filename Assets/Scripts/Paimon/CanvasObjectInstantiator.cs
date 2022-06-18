using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Paimon
{

    public class CanvasObjectInstantiator : MonoBehaviour
    {
        public Transform cutsceneOriginPoint;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void StartCutscene(GameObject cutscene)
        {
            var newObject = Instantiate(cutscene, cutsceneOriginPoint.position, Quaternion.identity, this.gameObject.transform);
            //newObject.transform.parent = gameObject.transform;
        }

    }
}
