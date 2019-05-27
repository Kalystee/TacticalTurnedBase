using PNN.Battlegrounds;
using PNN.UI.Abilities;
using UnityEngine;

namespace PNN
{
    public static class Find
    {
        public static GameManager GameManager
        {
            get
            {
                return GameManager.instance;
            }
        }

        public static Camera Camera
        {
            get
            {
                return GameManager?.mainCamera;
            }
        }

        public static InputManager InputManager
        {
            get
            {
                return GameManager?.inputManager;
            }
        }

        public static Team PlayerTeam
        {
            get
            {
                return GameManager?.playerTeam;
            }
        }

        public static AbilityBarView AbilityBarView
        {
            get
            {
                return GameManager?.abilityBarView;
            }
        }

        public static BattlegroundController BattlegroundController
        {
            get
            {
                return GameManager.battlegroundController;
            }
        }

        public static Battleground Battleground
        {
            get
            {
                return BattlegroundController?.battleground;
            }
        }

        public static PrefabsList Prefabs
        {
            get
            {
                return GameManager.prefabsList;
            }
        }
    }
}
