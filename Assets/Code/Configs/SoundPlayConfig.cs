using System;
using System.Collections.Generic;
using UnityEngine;

public enum SoundTrack
{
    idle,
    walk,
    run,
    jump,
    fall,
    attack1,
    attack2,
    attack3,
    block,
    hurt,
    death
}

[CreateAssetMenu(fileName = "SoundPlayConfig", 
    menuName = "Configs/SoundPlayConfig")]
public sealed class SoundPlayConfig : ScriptableObject
{
    [Serializable]
    public sealed class AudioSequence
    {
        public SoundTrack Track;
        public AudioClip Clip;
    }

    [SerializeField]
    public List<AudioSequence> SoundSequences = new List<AudioSequence>();
}
