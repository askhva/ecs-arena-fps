﻿using _project.ecs_learning.Scripts.ModulePlayer.SubModulePlayerInput.Components;
using _project.ecs_learning.Scripts.ModulePlayer.SubModulePlayerLoad.Components;
using Scellecs.Morpeh;

namespace _project.ecs_learning.Scripts.ModulePlayer.SubModulePlayerLoad.Systems
{
    public class PlayerLoadShootingDataSystem : ISystem
    {
        private Filter _filter;

        public World World { get; set; }
        
        public void OnAwake()
        {
            _filter = World.Filter
                .With<PlayerComponent>()
                .With<PlayerLoadMarker>();
        }
        
        public void OnUpdate(float deltaTime)
        {
            foreach (var entity in _filter)
            {
                ref var characterComponent = ref entity.GetComponent<PlayerComponent>();
                entity.SetComponent(new PlayerShootingInputData {weaponEntity = characterComponent.weapon.Entity});
            }
        }
        
        public void Dispose()
        {
            _filter = null;
        }
    }
}