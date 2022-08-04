using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Assets.Code.Systems
{
    class GroundSystem : IEcsRunSystem
    {
        EcsPoolInject<Unit> _units = default;

        public void Run(IEcsSystems systems)
        {

        }
    }
}
