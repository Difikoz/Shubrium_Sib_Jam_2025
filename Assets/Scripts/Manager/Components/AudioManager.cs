using FMOD.Studio;
using FMODUnity;
using UnityEngine;

namespace WinterUniverse
{
    public class AudioManager : Singleton<AudioManager>
    {
        [SerializeField] private EventReference _musicRef;
        [SerializeField] private EventReference _ambientRef;

        private EventInstance _musicEvent;
        private EventInstance _ambientEvent;

        protected override void OnAwake()
        {
            _musicEvent = RuntimeManager.CreateInstance(_musicRef);
            _ambientEvent = RuntimeManager.CreateInstance(_ambientRef);
            _musicEvent.start();
            _ambientEvent.start();
            ChangeBackgroundMusic(0);
        }

        public void ChangeBackgroundMusic(int value)
        {
            _musicEvent.setParameterByName("gameState", value);
            _ambientEvent.setParameterByName("gameState", value);
        }

        public void PlaySound(string eventPath)
        {   
            RuntimeManager.PlayOneShot(eventPath);
        }

        public void PlaySoundAtPosition(string eventPath, Vector3 position)
        {
            RuntimeManager.PlayOneShot(eventPath, position);
        }

        public void PlaySoundAttached(string eventPath, GameObject go)
        {
            RuntimeManager.PlayOneShotAttached(eventPath, go);
        }

        // 0 - main menu
        // 1 - combat
        // 2 - elevator
        // 3 - boss
        // 4 - death
        // 5 - win
    }
}