using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Sirenix.OdinInspector;

namespace PNN.UI.Abilities
{
    public class AbilityBarView : MonoBehaviour
    {
        private bool isVisible = false;

        [BoxGroup("Settings")] public RectTransform parent;
        [BoxGroup("Settings")] public Transform transformAbilityPanel;

        [BoxGroup("Debug Informations"), ReadOnly] public AbilityBase SelectedAbility;

        private const string abilityPrefabImageName = "Ability:Image";

        private void Awake()
        {
            parent.position.Set(parent.position.x, -parent.sizeDelta.y, parent.position.z);

            Find.GameManager.OnGamePlayStateChanged += HandleGamePlayStateChanged;
        }

        private void HandleGamePlayStateChanged(object sender, GameManager_OnGamePlayStateChangedArgs args)
        {
            if(args.gamePlayState == GamePlayState.SELECT_ABILITY)
            {
                SetVisibleState(false);
            }
            else if(args.gamePlayState == GamePlayState.BEGIN_TURN)
            {
                if (Find.Battleground.PlayingEntity.Team == Find.GameManager.playerTeam)
                {
                    SetVisibleState(true);
                    DisplayAbilities(Find.Battleground.PlayingEntity.Character.abilityKnowledge.LearntAbilities);
                }
            }
            else if(args.gamePlayState ==  GamePlayState.END_TURN)
            {
                SetVisibleState(false);
                DitchAllAbilityButtons();
            }
            else if(args.gamePlayState == GamePlayState.IDLE)
            {
                if (Find.Battleground.PlayingEntity.Team == Find.PlayerTeam)
                    SetVisibleState(true);
            }
        }

        public void SwitchVisibleState()
        {
            isVisible = !isVisible;
            if (isVisible)
            {
                StartCoroutine(MoveBarTo(0));
            }
            else
            {
                StartCoroutine(MoveBarTo(-parent.sizeDelta.y));
            }
        }

        public void SetVisibleState(bool state)
        {
            if (isVisible != state)
            {
                isVisible = state;
                if (isVisible)
                {
                    StartCoroutine(MoveBarTo(0));
                }
                else
                {
                    StartCoroutine(MoveBarTo(-parent.sizeDelta.y));
                }
            }
        }

        IEnumerator MoveBarTo(float endY, float speed = 7.5f)
        {
            float percent = 0f;
            Vector2 position = parent.position;
            float startY = parent.position.y;

            while (percent < 1f)
            {
                position = new Vector2(position.x, Mathf.Lerp(startY, endY, percent));
                parent.position = position;
                percent += Time.deltaTime * (Mathf.Sin(Mathf.PI * 0.9f * percent + 0.15f)) * speed;
                yield return new WaitForEndOfFrame();
            }
            parent.position = new Vector2(position.x, endY);
        }

        #region Abilities Button
        List<AbilityButton> abilityButtons = new List<AbilityButton>();

        public AbilityButton GetAbilityButton(AbilityBase ability)
        {
            return abilityButtons.Find(ab => ab.ability == ability);
        }

        private void DitchAllAbilityButtons()
        {
            foreach (AbilityButton ab in abilityButtons.ToArray())
            {
                Destroy(ab.parent);
                abilityButtons.Remove(ab);
            }
        }

        private void DisplayAbilities(AbilityBase[] abilities)
        {
            if (abilities != null)
            {
                foreach (AbilityBase ability in abilities)
                {
                    GameObject prefabAbilityObject = Instantiate(Find.Prefabs.abilityPrefabButton);
                    prefabAbilityObject.GetComponent<RectTransform>().SetParent(transformAbilityPanel);
                    LayoutRebuilder.ForceRebuildLayoutImmediate(transformAbilityPanel.GetComponent<RectTransform>());

                    Button prefabAbilityButton = prefabAbilityObject.GetComponent<Button>();
                    prefabAbilityButton.onClick.AddListener(
                    delegate ()
                        {
                        if (ability.cost <= Find.Battleground.PlayingEntity.Character.PA.CurrentValue)
                            {
                                SelectedAbility = ability;
                                Find.GameManager.CurrentGamePlayState = GamePlayState.SELECT_ABILITY;
                            }
                        });

                    Image prefabAbilityImage = prefabAbilityObject.transform.Find(abilityPrefabImageName).GetComponent<Image>();
                    prefabAbilityImage.sprite = ability.sprite;

                    abilityButtons.Add(new AbilityButton(ability, prefabAbilityObject, prefabAbilityImage, prefabAbilityButton));
                }
            }
        }
        #endregion
    }
}
