using UnityEngine;

using MyBox;
namespace GameManager.Sound {
    [RequireComponent(typeof(AudioSource))]
    public class SoundManager : Singleton<SoundManager> {
        #region AudioFile_STRUCT
        [System.Serializable]
        private struct AudioFile {
            [SerializeField, Tooltip("The respective audio clip for the audio type"), MustBeAssigned]
            private AudioClip audioClip;

            [SerializeField, SearchableEnum]
            private SoundType soundType;

            public AudioClip AudioClip { get => audioClip; }
            public SoundType SoundType { get => soundType; }
        }
        #endregion

        [AutoProperty, SerializeField]
        private AudioSource soundAudioSource;

        [Separator()]
        [SerializeField, MustBeAssigned]
        private AudioFile[] audioFiles;

        private void Awake() {
            AdjustSoundAudioSourceBySoundStatus();
            DontDestroyOnLoad(this);
        }

        internal void ToggleSoundStatus() {
            GlobalProperties.SoundOn_Local = !GlobalProperties.SoundOn_Local;
            AdjustSoundAudioSourceBySoundStatus();
        }

        internal void PlaySoundBySoundType(SoundType soundType) {
            if (TryGetAudioClipBySoundType(soundType, out AudioClip audioClipToPlay)) {
                soundAudioSource.PlayOneShot(audioClipToPlay);
            } else {
                // DEBUG!!!
                Debug.LogWarning("Audio Clip for sound type of " + soundType.ToString() + " is not found!");
            }
        }

        #region Utils
        private void AdjustSoundAudioSourceBySoundStatus() {
            if (GlobalProperties.SoundOn_Local) {
                soundAudioSource.volume = 1f;
            } else {
                soundAudioSource.volume = 0f;
            }
        }

        private bool TryGetAudioClipBySoundType(SoundType soundType, out AudioClip result) {
            foreach (var audioFile in audioFiles) {
                if (audioFile.SoundType == soundType) {
                    result = audioFile.AudioClip;
                    return true;
                }
            }
            result = null;
            return false;
        }
        #endregion
    }
}