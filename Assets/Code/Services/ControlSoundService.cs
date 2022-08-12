using Assets.Code.Components;
using Leopotam.EcsLite;
using System.Collections.Generic;
using UnityEngine;

public sealed class ControlSoundService
{
    public ControlSoundService(IEcsSystems systems)
    {
        EcsWorld world = systems.GetWorld();
        _soundQueuePool = world.GetPool<SoundQueueComponent>();
        _unitSoundPool = world.GetPool<UnitSoundComponent>();
    }

    public void PlaySound(int unitEntity, SoundTrack track, bool isLoop)
    {
        ref SoundQueueComponent soundQueue = ref GetSoundQueue(unitEntity);

        ref var unitSound = ref _unitSoundPool.Get(unitEntity);

        AudioClip clip = GetClip(unitSound.SoundConfig, track);

        if (null == clip)
        {
            Debug.LogWarning($"there is not sound for track {track}");
            return;
        }

        soundQueue.Clips.Enqueue(new SoundTask
        {
            Clip = clip,
            Track = track, 
            IsLoop = isLoop
        });
    }

    AudioClip GetClip(SoundPlayConfig config, SoundTrack track)
    {
        return config.SoundSequences.Find(
              sequence => track == sequence.Track)?.Clip;
    }

    public void StopSound(int unitEntity)
    {
        ref var unitSound = ref _unitSoundPool.Get(unitEntity);
        unitSound.AudioPlayer.Stop();
    }

    private ref SoundQueueComponent GetSoundQueue(int unitEntity)
    {
        if (_soundQueuePool.Has(unitEntity))
            return ref _soundQueuePool.Get(unitEntity);

        ref var soundTask = ref _soundQueuePool.Add(unitEntity);
        soundTask.Clips = new Queue<SoundTask>();

        return ref soundTask;
    }

    private EcsPool<SoundQueueComponent> _soundQueuePool = default;
    private EcsPool<UnitSoundComponent> _unitSoundPool = default;
}
