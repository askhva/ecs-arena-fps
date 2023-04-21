﻿using _project.ecs_learning.Scripts.ModuleEnemies.Configs;
using _project.ecs_learning.Scripts.ModuleEntityControl.Components;
using _project.ecs_learning.Scripts.ModuleGameState.Components;
using _project.ecs_learning.Scripts.ModuleMaps.Components;
using Scellecs.Morpeh;
using UnityEngine;
using Object = UnityEngine.Object;

namespace _project.ecs_learning.Scripts.ModuleEnemies.SubModuleEnemiesLoad.Systems
{
    public class EnemiesLoadSystem : ISystem
    {
        private Filter _startMarkerFilter;
        private Filter _stageFilter;
        private EnemiesCollection _enemiesCollection;

        public World World { get; set; }

        public EnemiesLoadSystem(EnemiesCollection enemiesCollection)
        {
            _enemiesCollection = enemiesCollection;
        }

        public void OnAwake()
        {
            _startMarkerFilter = World.Filter
                .With<PlayStartMarker>()
                .Without<BlockMarker>();
            
            _stageFilter = World.Filter
                .With<MapComponent>();
        }
        
        public void OnUpdate(float deltaTime)
        {
            foreach (var startMarkerEntity in _startMarkerFilter)
            {
                foreach (var stageEntity in _stageFilter)
                {
                    ref var stageComponent = ref stageEntity.GetComponent<MapComponent>();

                    for (int i = 0; i < stageComponent.enemySpawnPointsTransforms.Length; i++)
                    {
                        var enemyPrefab = 
                            _enemiesCollection.Prefabs[Random.Range(0, _enemiesCollection.Prefabs.Length)];
                        
                        Object.Instantiate(enemyPrefab, 
                            stageComponent.enemySpawnPointsTransforms[i].position, Quaternion.identity);
                    }
                }
            }
        }
        
        public void Dispose()
        {
            _startMarkerFilter = null;
            _stageFilter = null;
        }
    }
}