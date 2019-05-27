using PNN.Entities.Events;
using PNN.Entities.Interfaces;
using PNN.Enums;
using System;
using UnityEngine;

namespace PNN.Entities
{
    public class EntityView : MonoBehaviour, IClickable, IOverable
    {
        public event EventHandler<IClickable_OnClickArgs> OnClicked;
        public event EventHandler<IOverable_OnOverArgs> OnOvered;
        public event EventHandler<IOverable_OnOverArgs> OnUnovered;

        private void OnMouseOver()
        {
            if (Find.GameManager.CurrentGamePlayState == GamePlayState.SELECT_ABILITY)
            {
                if (!Input.GetKey(KeyCode.LeftShift))
                {
                    MouseButton click = MouseButton.None;
                    if (Input.GetMouseButtonDown(0))
                        click = MouseButton.LeftClick;
                    else if (Input.GetMouseButtonDown(1))
                        click = MouseButton.RightClick;
                    else if (Input.GetMouseButtonDown(2))
                        click = MouseButton.MiddleClick;

                    if (click != MouseButton.None)
                    {
                        IClickable_OnClickArgs clickArgs = new IClickable_OnClickArgs(click);
                        OnClicked?.Invoke(this, clickArgs);
                        Debug.Log("Clicked (" + click + ") on entity");
                    }
                }
            }

            IOverable_OnOverArgs overArgs = new IOverable_OnOverArgs();
            OnOvered?.Invoke(this, overArgs);
        }

        private void OnMouseExit()
        {
            IOverable_OnOverArgs overArgs = new IOverable_OnOverArgs();
            OnUnovered?.Invoke(this, overArgs);
        }
    }
}
