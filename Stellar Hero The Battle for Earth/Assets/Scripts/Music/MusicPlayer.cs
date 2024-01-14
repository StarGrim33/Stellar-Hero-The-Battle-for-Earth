using Utils;
using Plugins.Audio.Core;
using UnityEngine;

namespace Music
{
    public class MusicPlayer : MonoBehaviour, IMusicPlayer
    {
        [SerializeField] private SourceAudio _source;

        private void Start()
        {
            _source.Play(Constants.BattleSound);
        }

        public void Pause()
        {
            _source.Mute = true;
        }

        public void UnPause()
        {
            _source.Mute = false;
        }
    }
}