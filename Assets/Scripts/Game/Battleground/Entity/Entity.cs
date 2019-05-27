using System;
using PNN.Battlegrounds;
using PNN.Entities.Events;
using PNN.Entities.Interfaces;
using UnityEngine;
using PNN.Characters;

namespace PNN.Entities
{
    public class Entity : IMoveable, IDamageable
    {
        public readonly Character Character;
        public readonly Battleground battleground;

        public Team Team;

        public event EventHandler<IDamageable_DamageTakenArgs> OnDamageTaken;
        public event EventHandler<IMoveable_PositionChangedArgs> OnPositionChanged;

        public Entity(Battleground battleground, Character character, Vector2Int spawnPosition, Team team)
        {
            this.battleground = battleground;
            this.Character = character;
            this._position = spawnPosition;
            this.Team = team;
        }

        [SerializeField] private Vector2Int _position;
        
        public Vector2Int Position
        {
            get
            {
                return _position;
            }
            set
            {
                if(_position != value)
                {
                    this._position = value;
                    IMoveable_PositionChangedArgs args = new IMoveable_PositionChangedArgs(this.Position, value);
                    OnPositionChanged?.Invoke(this, args);
                }
            }
        }

        public void TakeDamage(int damage)
        {
            IDamageable_DamageTakenArgs args = new IDamageable_DamageTakenArgs(this.Character.PV, damage);
            OnDamageTaken?.Invoke(this, args);

            Character.PV.CurrentValue -= damage;
        }
    }
}