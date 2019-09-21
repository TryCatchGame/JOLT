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

        #region Utils
        internal void ShowAchievementInterface() {
            Social.ShowAchievementsUI();
        }

        internal void ShowLeaderboardInterface() {
            Social.ShowLeaderboardUI();
        }

        internal void UpdateScoreAchievements(int scoreAmount) {
            foreach (var achievementIDs in GPGSIds.score_achievement_IDs) {
                playGamesPlatform.IncrementAchievement(achievementIDs, scoreAmount, null);
            }
        }

        internal void IncrementCoreAchievements() {
            foreach (var achievementIDs in GPGSIds.core_purchase_achievement_IDs) {
                playGamesPlatform.IncrementAchievement(achievementIDs, 1, null);
            }
        }

        internal void UpdateDiamondAchievements(int diamondAmount) {
            foreach (var achievementIDs in GPGSIds.diamond_achievement_IDs) {
                playGamesPlatform.IncrementAchievement(achievementIDs, diamondAmount, null);
            }
        }
        #endregion
    }
}