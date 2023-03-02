using Unity.Burst;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

partial class SpawnerSystem : SystemBase
{
    protected override void OnUpdate()
    {
        float deltaTime = SystemAPI.Time.DeltaTime;

        EntityCommandBuffer ecbSingleton = SystemAPI.GetSingleton<EndInitializationEntityCommandBufferSystem.Singleton>().CreateCommandBuffer(World.Unmanaged);

        foreach (SpawnerAspect spawner in SystemAPI.Query<SpawnerAspect>())
        {
            spawner.ElapseTime(deltaTime, ecbSingleton);
        }
        //ecbSingleton.Playback()
    }
}
