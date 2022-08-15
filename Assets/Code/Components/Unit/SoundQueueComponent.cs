using System.Collections.Generic;
using UnityEngine;

namespace Assets.Code.Components
{
    struct SoundTask
    {
        public AudioClip Clip;
        public SoundTrack Track;
        public bool IsLoop;
    }

    struct SoundQueueComponent
    {
        public Queue<SoundTask> Clips;
    }
}
