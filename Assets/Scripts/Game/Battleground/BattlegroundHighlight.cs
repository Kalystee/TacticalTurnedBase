using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace PNN.Battlegrounds
{
    [Serializable]
    public class BattlegroundHighlight
    {
        public event EventHandler<Battleground_OnHighlightChangedArgs> OnColorChanged;

        private static Color noColor = new Color32(0, 0, 0, 0);

        List<HighlightElement> highlights;

        public BattlegroundHighlight()
        {
            this.highlights = new List<HighlightElement>();
        }
            
        public void Reset()
        {
            this.highlights = new List<HighlightElement>();

            Battleground_OnHighlightChangedArgs args = new Battleground_OnHighlightChangedArgs(GetColor());
            OnColorChanged?.Invoke(this, args);
        }

        private bool IsAlreadyHighlighted(string name)
        {
            if(this.highlights.Count < 1)
            {
                return false;
            }
            else
            {
                return this.highlights.Any(hle => hle.name == name);
            }
        }

        public void SetHighlight(string name, Color color, int priority)
        {
            if (!IsAlreadyHighlighted(name))
            {
                HighlightElement oldTop = GetTopPriorityHighlight();

                this.highlights.Add(new HighlightElement(name, color, priority));
                if (oldTop == null || oldTop.priority < priority)
                {
                    Battleground_OnHighlightChangedArgs args = new Battleground_OnHighlightChangedArgs(GetColor());
                    OnColorChanged?.Invoke(this, args);
                }
            }
        }

        public void SetOtherwiseRemove(string name, Color color, int priority)
        {
            if (!IsAlreadyHighlighted(name))
            {
                HighlightElement oldTop = GetTopPriorityHighlight();

                this.highlights.Add(new HighlightElement(name, color, priority));
                if (oldTop == null || oldTop.priority < priority)
                {
                    Battleground_OnHighlightChangedArgs args = new Battleground_OnHighlightChangedArgs(GetColor());
                    OnColorChanged?.Invoke(this, args);
                }
            }
            else
            {
                HighlightElement oldTop = GetTopPriorityHighlight();
                HighlightElement toDeleteHighlight = this.highlights.Find(h => h.name == name);
                
                if (oldTop == toDeleteHighlight && this.highlights.Remove(toDeleteHighlight))
                {
                    Battleground_OnHighlightChangedArgs args = new Battleground_OnHighlightChangedArgs(GetColor());
                    OnColorChanged?.Invoke(this, args);
                }
            }
        }

        public bool TryRemoveHighlight(string name)
        {
            if (!IsAlreadyHighlighted(name))
            {
                return false;
            }

            HighlightElement oldTop = GetTopPriorityHighlight();
            HighlightElement toDeleteHighlight = this.highlights.Find(h => h.name == name);
            
            bool dirty = this.highlights.Remove(toDeleteHighlight);

            if (oldTop == toDeleteHighlight && dirty)
            {
                Battleground_OnHighlightChangedArgs args = new Battleground_OnHighlightChangedArgs(GetColor());
                OnColorChanged?.Invoke(this, args);
            }

            return dirty;
        }

        private HighlightElement GetTopPriorityHighlight()
        {
            if (this.highlights.Count == 0)
                return null;

            return this.highlights.OrderByDescending(hl => hl.priority).First();
        }

        public Color GetColor()
        {
            HighlightElement top = GetTopPriorityHighlight();
            if (top == null)
                return noColor;

            return top.color;
        }
    }

    public class HighlightElement
    {
        public readonly string name;
        public readonly Color color;
        public readonly int priority;

        public HighlightElement(string name, Color color, int priority)
        {
            this.name = name;
            this.color = color;
            this.priority = priority;
        }
    }

}