
namespace Assets.Code.Interfaces
{
    public interface IControlSoundService
    {
        void PlaySound(int unitEntity, SoundTrack track, bool isLoop);
    }
}
