/* A basic template for a manager script to set up and manage
 * a battle. It is entirely possible to split this script
 * up into multiple ones, seperating battle and actor creation, UI, etc.
 */

using UnityEngine;
using UnityEngine.UI;
using Ares.ActorComponents;
using Ares.UI;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace Ares.Examples {
	public class BattleManager2D : MonoBehaviour {
		[SerializeField, Header("Battle")] BattleRules rules; // The rules and settings that the battle will adhere to.
		[SerializeField] Actor hero;
		[SerializeField] Actor slime;
		[SerializeField] Button[] buttons;


		[SerializeField, Header("UI References")] Text[] eventTexts;
		[SerializeField, Header("UI Controls")] float eventTextScrollSpeed;
		[SerializeField] float eventTextHoldTime;

		float textOffsetBottom;
		float textOffsetDisplay;
		float textOffsetTop;
		int currentEventText;
		int currentEventTextMargin;
		int currentEventTextsActive;

		Battle battle; // A reference to the actual Battle object

		void Awake(){
			// Spawn all dynamic actors and set up their Actor* components here.
		}

		void Start(){
			// Set up battle
			battle = new Battle(rules);

			// Set up the required battle delegates and events
			battle.OnActorNeedsActionInput.AddListener(ShowActionInput);
			battle.OnActorNeedsSingleTargetInput.AddListener(ShowTargetInput);
			battle.OnActorNeedsActorsTargetInput.AddListener(ShowTargetInput);
			battle.OnActorNeedsGroupTargetInput.AddListener(ShowTargetInput);
			battle.OnActorHasGivenAllNeededInput.AddListener(HideInput);
			battle.OnBattleEnd.AddListener(OnBattleEnd);

			// Set up groups and win conditions
			BattleGroup group1 = battle.AddGroup("Heroes");
			BattleGroup group2 = battle.AddGroup("Enemies");

			group1.OnDefeat.AddListener(() => EndBattle(false));
			group2.OnDefeat.AddListener(() => EndBattle(true));

			// Add all actors to their respective groups
			group1.AddActor(hero, true);
			group2.AddActor(slime, true);

			// Start the battle and get it initialized
			battle.Start(true);

			// If we'd started the battle with `progressAutomatically = false`, we could wait a while here to open menus etc.
			// before manually progressing to the first round by calling `battle.ProgressBattle()`.
		}
			
		void ShowActionInput(Actor actor, ActionInput actionInput){
			// Set up and show the UI for selecting an actor's item or ability.

			// `actionInput.ValidAbilities` and `.ValidItems` are filtered lists of all abilities and items that can be used
			// given the current state of the battle.

			// `actor.Abilities` can be used to access all abilities.

			// The actor's full inventory can be accessed from either `actor.inventory`, `actor.Group.Inventory`
			// or both, depending on how your game works and which items you wish to show when.
			// These inventories can be filtered based on the `rules.ItemComsumptionMoment`.
			// Typically `OnRoundStart` and `OnTurn` moments would use the `Inventory.Filter.All` filter,
			// and `OnTurnButMarkPendingOnSelect` would use `Inventory.Filter.ExcludePending`.

			// To select an item or ability, call the respective callback method inside `actionInput`.
			// These callbacks will return a `success` bool.

			for(int i = 0; i < actionInput.ValidAbilities.Length; i++){
				Ability ability = actionInput.ValidAbilities[i];

				buttons[i].GetComponent<Animator>().SetTrigger("open");
				buttons[i].GetComponentInChildren<Text>().text = ability.Data.DisplayName;

				buttons[i].onClick.AddListener(() => {
					if(actionInput.AbilitySelectCallback(ability)){
						for(int j = 0; j < actionInput.ValidAbilities.Length; j++){
							buttons[j].GetComponent<Animator>().SetTrigger("close");
							buttons[j].onClick.RemoveAllListeners();
						}
					}
				});
			}
		}

		void ShowItemInput(Actor actor, StackedItem[] items, ActionInput actionInput){
			// Set up and show the UI for selecting an item for the current actor to use.
			// When a target is selected, call `actionInput.ItemSelectCallback(chosenItem)`
		}

		void ShowTargetInput(Actor actor, TargetInputSingleActor targetInput){
			// Set up and show the UI for selecting the chosen action's target actor.
			// When a target is selected, call `actionInput.TargetSelectCallback(chosenActor)`.
			// This callback will return a `success` bool.

			for(int i = 0; i < targetInput.ValidTargets.Length; i++){
				Actor target = targetInput.ValidTargets[i];

				buttons[i].GetComponent<Animator>().SetTrigger("open");
				buttons[i].GetComponentInChildren<Text>().text = target.DisplayName;

				buttons[i].onClick.AddListener(() => {
					if(targetInput.TargetSelectCallback(target)){
						for(int j = 0; j < targetInput.ValidTargets.Length; j++){
							buttons[j].GetComponent<Animator>().SetTrigger("close");
							buttons[j].onClick.RemoveAllListeners();
						}
					}
				});
			}
		}

		void ShowTargetInput(Actor actor, TargetInputNumActors targetInput){
			// Set up and show the UI for selecting the chosen action's target actors.
			// When a target is selected, call `actionInput.TargetSelectCallback(chosenActors)`.
			// This callback will return a `success` bool.
		}

		void ShowTargetInput(Actor actor, TargetInputGroup targetInput){
			// Set up and show the UI for selecting the chosen action's target group.
			// When a target is selected, call `actionInput.TargetSelectCallback(chosenBattleGroup)`.
			// This callback will return a `success` bool.
		}

		void HideInput(Actor actor){
			// Hide the UI now that the actor has received all needed input.
		}
			
		void EndBattle(bool playerWon){
			// A win condition has been met; end the battle.
			battle.EndBattle(Battle.EndReason.WinLoseConditionMet);

			// Show victory/ defeat animations and UI
		}

		void OnBattleEnd(Battle.EndReason endReason){
			if(endReason == Battle.EndReason.OutOfTurns){
				// Show tie screen or determine winner
			}
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

			actor.OnHPChange.AddListener((newHP) => ShowEventText(
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

			battle.OnEnvironmentVariableSet.AddListener((envVar) => {
				switch (envVar.Data.name.ToLower())
				{
					case "darkness": ShowEventText("Darkness has befallen the land!"); return;
					case "neutral zone": ShowEventText("A neutral zone has been established!"); return;
					default: ShowEventText(string.Format("{0} has begun!", envVar.Data.DisplayName)); return;
				}
			});

			battle.OnEnvironmentVariableUnset.AddListener((envVar) => {
				switch (envVar.Data.name.ToLower())
				{
					case "darkness": ShowEventText("Light has returned!"); return;
					case "neutral zone": ShowEventText("The neutral zone has been lifted!"); return;
					default: ShowEventText(string.Format("{0} has ended!", envVar.Data.DisplayName)); return;
				}
			});
		}

		void ShowEventText(string text)
		{
			StartCoroutine(CRShowEventText(text));
		}

		IEnumerator CRShowEventText(string text)
		{
			Text eventText = eventTexts[currentEventText];
			float goalOffset = textOffsetDisplay - eventText.GetPixelAdjustedRect().height * 1.2f * currentEventTextMargin;
			float offset;
			float speedMultiplier;

			eventText.text = text;
			eventText.rectTransform.offsetMin = new Vector2(eventText.rectTransform.offsetMin.x, textOffsetBottom);
			eventText.rectTransform.offsetMax = new Vector2(eventText.rectTransform.offsetMax.x, textOffsetBottom);

			currentEventText = (currentEventText + 1) % eventTexts.Length;
			currentEventTextMargin++;
			currentEventTextsActive++;

			while (goalOffset - eventText.rectTransform.offsetMin.y > .02f)
			{
				speedMultiplier = Mathf.Lerp(.03f, 1f, Mathf.Pow(Mathf.Min(0f, eventText.rectTransform.offsetMin.y - goalOffset) / textOffsetBottom, .7f));
				offset = Mathf.MoveTowards(eventText.rectTransform.offsetMin.y, goalOffset, eventTextScrollSpeed * Camera.main.pixelHeight * speedMultiplier * Time.deltaTime);

				eventText.rectTransform.offsetMin = new Vector2(eventText.rectTransform.offsetMin.x, offset);
				eventText.rectTransform.offsetMax = new Vector2(eventText.rectTransform.offsetMax.x, offset);

				yield return null;
			}

			yield return new WaitForSeconds(eventTextHoldTime);

			currentEventTextsActive--;

			if (currentEventTextsActive == 0)
			{
				currentEventTextMargin = 0;
			}

			while (textOffsetTop - eventText.rectTransform.offsetMin.y > .02f)
			{
				speedMultiplier = Mathf.Lerp(.03f, 1f, Mathf.Pow(Mathf.Min(0f, eventText.rectTransform.offsetMin.y - textOffsetTop) / textOffsetBottom, .7f));
				offset = Mathf.MoveTowards(eventText.rectTransform.offsetMin.y, textOffsetTop, eventTextScrollSpeed * Camera.main.pixelHeight * speedMultiplier * Time.deltaTime);
				eventText.rectTransform.offsetMin = new Vector2(eventText.rectTransform.offsetMin.x, offset);
				eventText.rectTransform.offsetMax = new Vector2(eventText.rectTransform.offsetMax.x, offset);

				yield return null;
			}
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