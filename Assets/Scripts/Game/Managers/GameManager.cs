using PNN.Battlegrounds;
using PNN.UI.Abilities;
using Sirenix.OdinInspector;
using System;
using UnityEngine;

namespace PNN
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager instance { get; private set; }

        private void Awake()
        {
            if (instance != null)
            {
                Debug.LogError("Duplicates of GameManager, destroying the oldest", instance);
                Destroy(this.gameObject);
            }
            instance = this;

            DontDestroyOnLoad(gameObject);
        }

        public EventHandler<GameManager_OnGameStateChangedArgs> OnGameStateChanged;
        public EventHandler<GameManager_OnGamePlayStateChangedArgs> OnGamePlayStateChanged;

        public BattlegroundController battlegroundController;

        [BoxGroup("Managers")] public CameraManager cameraManager;
        [BoxGroup("Managers")] public InputManager inputManager;
        [BoxGroup("Managers")] public LevelManager levelManager;
        [BoxGroup("Managers")] public PrefabsList prefabsList;

        [BoxGroup("Main Elements")] public Camera mainCamera;

        [BoxGroup("UI Elements")] public AbilityBarView abilityBarView;

        [BoxGroup("Game Settings")] public Team playerTeam;

        private void OnApplicationQuit()
        {
            instance = null;
        }

        [SerializeField, BoxGroup("Current States")] private GameState _currentGameState;
        public GameState CurrentGameState
        {
            get
            {
                return this._currentGameState;
            }
            set
            {
                if (this._currentGameState != value)
                {
                    this._currentGameState = value;
                    if(this._currentGameState == GameState.PLAYING)
                    {
                        this._currentGamePlayState = GamePlayState.IDLE;
                    }
                    else
                    {
                        this._currentGamePlayState = GamePlayState.NONE;
                    }
                    GameManager_OnGameStateChangedArgs args = new GameManager_OnGameStateChangedArgs(value);
                    OnGameStateChanged?.Invoke(this, args);
                }
            }
        }

        [SerializeField, BoxGroup("Current States")] private GamePlayState _currentGamePlayState;
        public GamePlayState CurrentGamePlayState
        {
            get
            {
                if (_currentGameState != GameState.PLAYING)
                    return GamePlayState.NONE;

                return _currentGamePlayState;
            }
            set
            {
                if (_currentGameState == GameState.PLAYING)
                {
                    if (this._currentGamePlayState != value)
                    {
                        this._currentGamePlayState = value;
                        GameManager_OnGamePlayStateChangedArgs args = new GameManager_OnGamePlayStateChangedArgs(value);
                        OnGamePlayStateChanged?.Invoke(this, args);
                    }
                }
            }
        }
    }
}