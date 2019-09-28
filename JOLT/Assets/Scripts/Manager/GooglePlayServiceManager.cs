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
            Social.Active.ShowAchievementsUI();
        }

        internal void ShowLeaderboardInterface() {
            Social.Active.ShowLeaderboardUI();
        }

        internal void UpdateLeaderboardWithScore(int score) {
            Social.ReportScore(score, GPGSIds.leaderboard_high_score, (bool success) => { });
        }

        internal void UpdateScoreAchievements(int scoreAmount) {
            foreach (var achievementIDs in GPGSIds.score_achievement_IDs) {
                playGamesPlatform.IncrementAchievement(achievementIDs, scoreAmount, null);
            }
        }

        internal void IncrementCoreAchievements() {
            playGamesPlatform.ReportProgress(GPGSIds.achievement_new_core, 100f, null);
            playGamesPlatform.IncrementAchievement(GPGSIds.achievement_variety_is_key, 1, null);
            playGamesPlatform.IncrementAchievement(GPGSIds.achievement_the_collector, 1, null);
        }

        internal void UpdateDiamondAchievements(int diamondAmount) {
            foreach (var achievementIDs in GPGSIds.diamond_achievement_IDs) {
                playGamesPlatform.IncrementAchievement(achievementIDs, diamondAmount, null);
            }
        }
        #endregion
    }
}