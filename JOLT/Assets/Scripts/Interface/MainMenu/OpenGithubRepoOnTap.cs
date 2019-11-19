using UnityEngine;
using UnityEngine.UI;

using MyBox;

namespace GameInterface.MainMenu {
    [RequireComponent(typeof(Button))]
    public class OpenGithubRepoOnTap : MonoBehaviour {

        [SerializeField, AutoProperty]
        private Button button;

        private const string GITHUB_REPOSITORY_URL = "https://github.com/TryCatchGame/JOLT";

        private void Awake() {
            if (button == null) {
                button = GetComponent<Button>();
            }

            button.onClick.AddListener(OpenGithubRepository);
        }

        private void OpenGithubRepository() {
            Application.OpenURL(GITHUB_REPOSITORY_URL);
        }
    }
}