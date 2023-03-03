using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Physics;
using Unity.Rendering;
using Unity.Transforms;
using UnityEngine;
using UnityEngine.Rendering;

[BurstCompile]
[UpdateAfter(typeof(MovementSystem))]
[UpdateBefore(typeof(SpawnerSystem))]

public partial class CollisionSystem : SystemBase
{

    EntityManager entityManager;
    [BurstCompile]
    protected override void OnCreate()
    {
        entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;

    }

    [BurstCompile]
    protected override void OnUpdate()
    {
        PlayerBehaviour player = PlayerBehaviour.Instance;

        float3 playerPosition = player.Position;
        float playerRadiusSQ = player.RangeRadiusSQ;
        URPMaterialPropertyBaseColor baseColor = player.colorComponent;

        float timeToLive = 5.0f;
        float deltaTime = SystemAPI.Time.DeltaTime;


        EntityCommandBuffer ecbSingleton = SystemAPI.GetSingleton<EndInitializationEntityCommandBufferSystem.Singleton>().CreateCommandBuffer(World.Unmanaged);
        bool playerCollided = false;
        Entities.WithAll<LocalToWorld>().WithoutBurst().ForEach((Entity entity, TransformAspect transform) =>
        {
            playerPosition = player.Position;
            float distance = CalculateDistance(transform.LocalPosition, playerPosition);
            
            if (distance <= playerRadiusSQ)
            {
                if(!EntityManager.HasComponent<TimeToLive>(entity))
                {
                    ecbSingleton.AddComponent(entity, new TimeToLive { Value = 5, entity = entity });
                    ecbSingleton.RemoveComponent<URPMaterialPropertyBaseColor>(entity);
                    ecbSingleton.AddComponent<URPMaterialPropertyBaseColor>(entity, player.colorComponent);
                    Debug.Log("Collision");
                }
            }

        }).Run();

        foreach (CollisionAspect collision in SystemAPI.Query<CollisionAspect>())
        {
            collision.ControlTime(deltaTime, ecbSingleton);
        }
    }
    private float CalculateDistance(float3 pointA, float3 pointB)
    {
        return math.distancesq(pointA, pointB);
    }
}
