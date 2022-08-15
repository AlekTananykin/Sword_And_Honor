

using Assets.Code.Configs;

namespace Assets.Code.Interfaces
{
    public interface IControlAnimationService
    {
        void StartAnimation(int unitEntity,
            AnimationTrack track, bool isLoop, float speed);
    }
}
