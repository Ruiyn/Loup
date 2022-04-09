using UnityEngine;
using UnityEngine.UI;
using Ares.ActorComponents;
using Ares.UI;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace Ares.Eligor {

    public class EligorBattleManager : MonoBehaviour{
        [SerializeField, Header("Battle")] BattleRules rules;
        [SerializeField] List<Actor> friendlyActors;
        [SerializeField] List<Actor> enemyActors;
        [SerializeField] Button[] buttons;
        [SerializeField] GameObject actionMenu;

        [SerializeField, Header("UI References")] Text[] eventTexts;
        [SerializeField, Header("UI Controls")] float eventTextscrollSpeed;
        [SerializeField] float eventTextHoldTime;

        float textOffsetBottom;
        float textOffsetDisplay;
        float textOffsetTop;
        int currentEventText;
        int currentEventTextMargin;
        int currentEventTextsActive;

        public GameObject diceBox;

        Battle battle;

        // Start is called before the first frame update
        void Start()
        {
            // Set up battle
            battle = new Battle(rules);

            // Set up the required battle delegates and events
            battle.OnActorNeedsActionInput.AddListener(ShowActionInput);

			SetUpStatusTextCallbacks(battle);
			//battle.OnActorHasGivenAllNeededInput.AddListener(HideInput);
			//battle.OnBattleEnd.AddListener(OnBattleEnd);
			//battle.OnTurnStart.AddListener(OnTurnStart);

			// Set up groups and win conditions
			BattleGroup group1 = battle.AddGroup("Heroes");
			BattleGroup group2 = battle.AddGroup("Enemies");

			//group1.OnDefeat.AddListener(() => EndBattle(false));
			//group2.OnDefeat.AddListener(() => EndBattle(true));



			// Add all actors to their respective groups

			foreach (Actor actor in friendlyActors)
			{
				group1.AddActor(actor, true);
				SetUpStatusTextCallbacks(actor);
			}

			foreach (Actor actor in enemyActors)
			{
				group2.AddActor(actor, true);
				SetUpStatusTextCallbacks(actor);
			}


			// Start the battle and get it initialized
			battle.Start(true);

			// If we'd started the battle with `progressAutomatically = false`, we could wait a while here to open menus etc.
			// before manually progressing to the first round by calling `battle.ProgressBattle()`.
		}

		// Update is called once per frame
		void Update()
        {

        }

        void ShowActionInput(Actor actor, ActionInput actionInput)
        {

			//Activate actor animation
			actor.gameObject.GetComponent<ActorAnimation>().animator.SetTrigger("openWindow");

			//Find the Actor's Skip and Act buttons
			EligorCharacterUI uiManager = actor.gameObject.GetComponentInChildren<EligorCharacterUI>();
			Button actButton = uiManager.actButton.GetComponent<Button>();
			//Button skipButton = uiManager.skipButton;

			Debug.Log(uiManager.ToString());

			//Make Skip and Act buttons work
			actButton.onClick.AddListener(delegate { uiManager.ShowActionBox(); uiManager.DisplayActionButtons(actionInput); });
;
        }

		//Event text callbacks and setup
		void SetUpStatusTextCallbacks(Actor actor)
		{
			actor.OnItemStart.AddListener((item, targets) => ShowEventText(string.Format("{0} used {1} on {2}!", actor.DisplayName, WithIndefiniteArticle(item.Data.DisplayName), string.Join(", ", targets.Select(t => t.DisplayName).ToArray()))));
			actor.OnAbilityActionMiss.AddListener((target, abilityInfo, action) => ShowEventText(string.Format("{0} attack missed!", AsPossessive(actor.DisplayName))));
			actor.OnAfflictionObtain.AddListener((affliction) => ShowEventText(string.Format("{0} has been afflicted with {1}!", actor.DisplayName, WithIndefiniteArticle(affliction.Data.DisplayName))));
			actor.OnAfflictionActionProcess.AddListener((affliction, afflictionAction) => ShowEventText(string.Format("{0} was affected by their {1}!", actor.DisplayName, affliction.Data.DisplayName)));
			actor.OnStatBuff.AddListener((stat, stage) => ShowEventText(string.Format("{0} {1} has increased!", AsPossessive(actor.DisplayName), stat.Data.DisplayName)));
			actor.OnHPDeplete.AddListener(() => ShowEventText(string.Format("{0} has been defeated!", actor.DisplayName)));
			actor.OnAbilityPreparationStart.AddListener((ability, message) => ShowEventText(message));
			actor.OnAbilityPreparationUpdate.AddListener((ability, turnsRemaining, message) => ShowEventText(message));
			actor.OnAbilityPreparationEnd.AddListener((ability, interrupted) => { if (interrupted) { ShowEventText(string.Format("{0} was interrupted!!", ability.Data.DisplayName)); } });
			//			actor.OnAbilityRecoveryStart.AddListener((ability, message) => ShowEventText(message));
			actor.OnAbilityRecoveryUpdate.AddListener((ability, turnsRemaining, message) => ShowEventText(message));
			actor.OnAbilityRecoveryEnd.AddListener((ability, interrupted) => { if (interrupted) { ShowEventText(string.Format("{0} was interrupted!!", ability.Data.DisplayName)); } });
			actor.OnItemPreparationStart.AddListener((item, message) => ShowEventText(message));
			actor.OnItemPreparationUpdate.AddListener((item, turnsRemaining, message) => ShowEventText(message));
			actor.OnItemPreparationEnd.AddListener((item, interrupted) => { if (interrupted) { ShowEventText(string.Format("{0} was interrupted!!", item.Data.DisplayName)); } });
			//			actor.OnItemRecoveryStart.AddListener((item, message) => ShowEventText(message));
			actor.OnItemRecoveryUpdate.AddListener((item, turnsRemaining, message) => ShowEventText(message));
			actor.OnItemRecoveryEnd.AddListener((item, interrupted) => { if (interrupted) { ShowEventText(string.Format("{0} was interrupted!!", item.Data.DisplayName)); } });

			actor.OnHPChange.AddListener((newHP) =>
			ShowEventText(
				newHP < actor.HP ?
				string.Format("{0} took {1} damage!", actor.DisplayName, actor.HP - newHP) :
				newHP > actor.HP ?
				string.Format("{0} restored {1} health!", actor.DisplayName, newHP - actor.HP) :
				string.Format("{0} was unaffected!", actor.DisplayName)
			));
		}

		void SetUpStatusTextCallbacks(Battle battle)
		{
			battle.OnTurnSkip.AddListener((actor, voluntarySkip) => ShowEventText(string.Format("{0} {1} their turn!", actor.DisplayName, voluntarySkip ? "skipped" : "was forced to skip")));

			foreach (BattleGroup group in battle.Groups)
			{
				group.OnDefeat.AddListener(() => ShowEventText(string.Format("{0} were defeated!", group.Name)));
			}
		}

		void ShowEventText(string text)
		{
			StartCoroutine(CRShowEventText(text));
		}

		IEnumerator CRShowEventText(string text)
		{
			Text eventText = eventTexts[currentEventText];
			//float goalOffset = textOffsetDisplay - eventText.GetPixelAdjustedRect().height * 1.2f * currentEventTextMargin;
			/*float offset;
			float speedMultiplier;*/

			eventText.text = text;
			/*eventText.rectTransform.offsetMin = new Vector2(eventText.rectTransform.offsetMin.x, textOffsetBottom);
			eventText.rectTransform.offsetMax = new Vector2(eventText.rectTransform.offsetMax.x, textOffsetBottom);*/

			currentEventText = (currentEventText + 1) % eventTexts.Length;
			//currentEventTextMargin++;
			currentEventTextsActive++;


			/*for (int i = 0; i < text.Length; i++)
			{
				eventText.text = string.Concat(eventText.text, text[i]);
				//Wait a certain amount of time, then continue with the for loop
				yield return new WaitForSeconds(eventTextHoldTime);
			}*/

			yield return new WaitForSeconds(eventTextHoldTime);

			eventText.text = "";

			currentEventTextsActive--;

			if (currentEventTextsActive == 0)
			{
				currentEventTextMargin = 0;
			}



			/*while (textOffsetTop - eventText.rectTransform.offsetMin.y > .02f)
			{
				speedMultiplier = Mathf.Lerp(.03f, 1f, Mathf.Pow(Mathf.Min(0f, eventText.rectTransform.offsetMin.y - textOffsetTop) / textOffsetBottom, .7f));
				offset = Mathf.MoveTowards(eventText.rectTransform.offsetMin.y, textOffsetTop, eventTextScrollSpeed * Camera.main.pixelHeight * speedMultiplier * Time.deltaTime);
				eventText.rectTransform.offsetMin = new Vector2(eventText.rectTransform.offsetMin.x, offset);
				eventText.rectTransform.offsetMax = new Vector2(eventText.rectTransform.offsetMax.x, offset);

				yield return null;
			}*/
		}

		string AsPossessive(string name)
		{
			return name + (name[name.Length - 1].ToString().ToLower() == "s" ? "'" : "'s");
		}

		string WithIndefiniteArticle(string subject)
		{
			return ("aeiou".IndexOf(subject[0].ToString().ToLower()) > -1 ? "an " : "a ") + subject;
		}
	}
}
