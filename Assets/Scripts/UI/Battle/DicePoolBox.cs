using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using Ares.Extensions;


namespace Ares
{
    public class DicePoolBox : MonoBehaviour
    {

        public PlayerActor thisActor;

        public Text[] diceText;


        // Start is called before the first frame update
        void Start()
        {
            thisActor = GetComponentInParent<PlayerActor>();
        }

        // Update is called once per frame
        void Update()
        {

        }
        public void TextUpdater()
        {
            /*int counter = 0;
            foreach (Text text in diceText)
            {
                text.text = thisActor.SpiritDice[counter].ToString();
                counter++;
            }*/
        }

    }
}
