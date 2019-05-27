using PNN;
using PNN.Entities;
using PNN.Entities.Events;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;

public class StatView : MonoBehaviour
{
    [BoxGroup("GameObjects")] public GameObject statWindow;

    [BoxGroup("Texts")] public TMP_Text nameText;
    [BoxGroup("Texts")] public TMP_Text pvText;
    [BoxGroup("Texts")] public TMP_Text paText;
    [BoxGroup("Texts")] public TMP_Text strText;
    [BoxGroup("Texts")] public TMP_Text intText;
    [BoxGroup("Texts")] public TMP_Text cstText;
    [BoxGroup("Texts")] public TMP_Text dextText;

    private bool isOvering = false;

    private void Awake()
    {
        Find.InputManager.OnMouseMoved += OnEntityOver;
        statWindow.SetActive(true);
    }

    public void OnEntityOver(object sender, MouseMoveArgs args)
    {
        Entity overedEntity = Find.BattlegroundController.GetEntityAt(args.worldIsometricPosition.AsVector2Int());
        if(overedEntity != null)
        {
            if (!isOvering)
            {
                nameText.text = new StringBuilder("Name : ").Append(overedEntity.Character.name).ToString();
                pvText.text = new StringBuilder("PV : ").Append(overedEntity.Character.PV.CurrentValue).Append(" / ").Append(overedEntity.Character.PV.Value).Append(" (+").Append(overedEntity.Character.PV.Value - overedEntity.Character.PV.BaseValue).Append(")").ToString();
                paText.text = new StringBuilder("PA : ").Append(overedEntity.Character.PA.CurrentValue).Append(" / ").Append(overedEntity.Character.PA.Value).Append(" (+").Append(overedEntity.Character.PA.Value - overedEntity.Character.PA.BaseValue).Append(")").ToString();
                strText.text = new StringBuilder("Strength : ").Append(overedEntity.Character.Strength.Value).ToString();
                intText.text = new StringBuilder("Intelligence : ").Append(overedEntity.Character.Intelligence.Value).ToString();
                cstText.text = new StringBuilder("Constitution : ").Append(overedEntity.Character.Consitution.Value).ToString();
                dextText.text = new StringBuilder("Dexterity : ").Append(overedEntity.Character.Dexterity.Value).ToString();
                statWindow.SetActive(true);
                isOvering = true;
            }
            statWindow.transform.position = Input.mousePosition;
        }
        else
        {
            isOvering = false;
            statWindow.SetActive(false);
        }
    }
}
