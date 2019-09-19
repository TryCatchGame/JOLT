using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine;

using MyBox;

namespace GameManager {
    public class GooglePlayServiceManager : Singleton<GooglePlayServiceManager> {

        [SerializeField, Tooltip("The main menu scene to load"), MustBeAssigned]
        private SceneReference mainMenuScene;

        private PlayGamesPlatform playGamesPlatform;

        private void Awake() {
            DontDestroyOnLoad(this);
        }

        private void Start() {
            if (playGamesPlatform == null) {
                InitalizePlayGamesPlatform();
            }

            Social.Active.localUser.Authenticate(LoadMainMenu);

            #region Local_Function
            void InitalizePlayGamesPlatform() {
                PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().Build();
                PlayGamesPlatform.InitializeInstance(config);
                PlayGamesPlatform.DebugLogEnabled = Application.isEditor;

                playGamesPlatform = PlayGamesPlatform.Activate();
            }

            void LoadMainMenu(bool authenticationSuccess) {
                SceneTransitionManager.Instance.TransitionToScene(mainMenuScene);
            }

            #endregion
        }
    }
}