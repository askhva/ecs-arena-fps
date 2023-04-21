﻿using _project.ecs_learning.Scripts.ModuleEntityControl.Components;
using _project.ecs_learning.Scripts.ModuleGameState.Components;
using _project.ecs_learning.Scripts.ModuleStages.Components;
using _project.ecs_learning.Scripts.ModuleStages.Configs;
using _project.ecs_learning.Scripts.ModuleUi.SubModulePlayerIndicators.Components;
using Scellecs.Morpeh;

namespace _project.ecs_learning.Scripts.ModuleStages.Systems
{
    public class StageLoadSystem : ISystem
    {
        private Filter _startMarkerFilter;
        private StageInfosCollection _stageInfosCollection;

        public World World { get; set; }

        public StageLoadSystem(StageInfosCollection stageInfosCollection)
        {
            _stageInfosCollection = stageInfosCollection;
        }

        public void OnAwake()
        {
            _startMarkerFilter = World.Filter
                .With<PlayStartMarker>()
                .Without<BlockMarker>();
        }

        public void OnUpdate(float deltaTime)
        {
            foreach (var startMarkerEntity in _startMarkerFilter)
            {
                World.CreateEntity().SetComponent(new CurrentStageData
                {
                    stageId = 0,
                    defeatEnemiesToWin = _stageInfosCollection.StageInfos[0].EnemiesCountToWin,
                    enemiesDefeated = 0
                });
                
                Entity entity = World.CreateEntity();
                entity.SetComponent(new EnemiesCountSetMarker());
                entity.SetComponent(new BlockMarker());
            }
        }

        public void Dispose()
        {
            _startMarkerFilter = null;
        }
    }
}