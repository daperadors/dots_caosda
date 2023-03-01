using Unity.Burst;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;

[BurstCompile]
[UpdateAfter(typeof(MovementSystem))]
[UpdateBefore(typeof(SpawnerSystem))]

public partial struct TriggersCheckerSystem : ISystem
{
    [BurstCompile]
    public void OnCreate(ref SystemState state)
    {
        
    }

    [BurstCompile]
    public void OnDestroy(ref SystemState state)
    {
        
    }

    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        PlayerBehaviour player = PlayerBehaviour.Instance;

        float3 playerPosition = player.Position;
        float playerRadiusSQ = player.RangeRadiusSQ;

    }
}