using Assets.Code.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

public sealed class PlaySoundSystem : IEcsRunSystem
{
    private EcsFilterInject<Inc<SoundQueueComponent, UnitSoundComponent>> 
        _soundTaskFilter = default;

    private EcsPoolInject<SoundQueueComponent> _soundQueuePool = default;
    private EcsPoolInject<UnitSoundComponent> _unitSoundPool = default;

    public void Run(IEcsSystems systems)
    {
        foreach (var unitEntity in _soundTaskFilter.Value)
        {
            ref var unitSound = ref _unitSoundPool.Value.Get(unitEntity);
            var player = unitSound.AudioPlayer;

            if (player.isPlaying)
                continue;

            ref var soundQueue = ref _soundQueuePool.Value.Get(unitEntity);

            if (0 == soundQueue.Clips.Count)
            {
                _soundQueuePool.Value.Del(unitEntity);
                continue;
            }

            var soundTask = soundQueue.Clips.Dequeue();

            player.loop = soundTask.IsLoop;
            player.clip = soundTask.Clip;
            player.Play();
        }
    }
}
