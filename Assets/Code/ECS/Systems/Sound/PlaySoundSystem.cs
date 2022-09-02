using Assets.Code.Components;
using Assets.Code.Components.Unit;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

public sealed class PlaySoundSystem : IEcsRunSystem
{
    private EcsFilterInject<Inc<SoundTaskComponent, UnitSoundComponent>> 
        _soundTaskFilter = default;


    public void Run(IEcsSystems systems)
    {
        foreach (var unitEntity in _soundTaskFilter.Value)
        {
            ref var soundTask = ref _soundTaskFilter.Pools.Inc1.Get(unitEntity);
            if (null == soundTask.Clip)
            {
                _soundTaskFilter.Pools.Inc1.Del(unitEntity);
                continue;
            }

            ref var unitSound = ref _soundTaskFilter.Pools.Inc2.Get(unitEntity);
            var player = unitSound.AudioPlayer;

            

            if (player.isPlaying)
                continue;

            player.loop = soundTask.IsLoop;
            player.clip = soundTask.Clip;
            player.Play();

            soundTask.Clip = null;
            _soundTaskFilter.Pools.Inc1.Del(unitEntity);
        }
    }
}
