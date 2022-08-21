using Assets.Code.Components.Unit;
using Assets.Code.Interfaces;
using Leopotam.EcsLite;
using UnityEngine;

public sealed class ControlSoundService: IControlSoundService
{
    public ControlSoundService(IEcsSystems systems)
    {
        EcsWorld world = systems.GetWorld();
        _soundContextPool = world.GetPool<SoundTaskComponent>();
    }

    public void PlaySound(int entity, AudioClip soundClip, bool isLoop)
    {
        if (null == soundClip)
            return;

        if (_soundContextPool.Has(entity))
        {
            ref var soundContext = ref _soundContextPool.Get(entity);
            soundContext.Clip = soundClip;
            soundContext.IsLoop = isLoop;
        }
        else
        {
            ref var soundContext = ref _soundContextPool.Add(entity);
            soundContext.Clip = soundClip;
            soundContext.IsLoop = isLoop;
        }
    }

    private EcsPool<SoundTaskComponent> _soundContextPool = default;
}
