using Assets.Code.ECS.Components;
using Leopotam.EcsLite;

namespace Assets.Code.Services
{
    public class AddCharacterService
    {
        public AddCharacterService(IEcsSystems systems)
        {
            EcsWorld world = systems.GetWorld();
            _characterData = world.GetPool<CharacterData>();
        }

        public int Add()
        {
            return 0;
        }

        public void Remove()
        {
            
        }


        private EcsPool<CharacterData> _characterData = default;
    }
}
