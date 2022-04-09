using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Ares.UI;


namespace Ares.Eligor
{
    public class EligorCharacterUI : MonoBehaviour
    {

        [SerializeField] public Button actButton;
        [SerializeField] public Button skipButton;
        [SerializeField] GameObject actionBox;
        [SerializeField] Button[] actionButtons;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        void ActivateButtons()
        {
            actButton.gameObject.SetActive(true);
            skipButton.gameObject.SetActive(true);
        }

        public void ShowActionBox()
        {
            actionBox.SetActive(true);
        }

        public void DisplayActionButtons(ActionInput actionInput)
        {
            for (int i = 0; i < actionInput.ValidAbilities.Length; i++)
            {
                Ability ability = actionInput.ValidAbilities[i];

                //buttons[i].GetComponent<Animator>().SetTrigger("open");
                actionButtons[i].GetComponentInChildren<Text>().text = ability.Data.DisplayName;


                actionButtons[i].onClick.AddListener(() => {
                    if (actionInput.AbilitySelectCallback(ability))
                    {
                        for (int j = 0; j < actionInput.ValidAbilities.Length; j++)
                        {
                            //buttons[j].GetComponent<Animator>().SetTrigger("close");
                            actionButtons[j].onClick.RemoveAllListeners();
                            actionBox.SetActive(false);
                        }
                    }
                });
            }
        }
    }
}
