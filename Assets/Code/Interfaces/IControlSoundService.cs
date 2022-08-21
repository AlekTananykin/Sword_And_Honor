
using UnityEngine;

namespace Assets.Code.Interfaces
{
    public interface IControlSoundService
    {
        void PlaySound(int entity, AudioClip sound, bool isLoop);
    }
}
