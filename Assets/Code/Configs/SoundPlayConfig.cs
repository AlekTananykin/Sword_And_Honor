using Assets.Code.Configs;
using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SoundPlayConfig", 
    menuName = "Configs/SoundPlayConfig")]
public sealed class SoundPlayConfig : ScriptableObject
{
    [Serializable]
    private sealed class AudioSequence
    {
        public Track Track;
        public AudioClip Clip;
    }

    [SerializeField]
    private List<AudioSequence> _sequences = new List<AudioSequence>();

    public SoundPlayConfig()
    {
        foreach (var audio in _sequences)
        {
            Clips.Add(audio.Track, audio.Clip);
        }
    }

    public Dictionary<Track, AudioClip> Clips = 
        new Dictionary<Track, AudioClip>();
}
