using System.Diagnostics;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEditor.PackageManager;

readonly partial struct SpawnerAspect : IAspect
{
    private readonly RefRW<Spawner> m_Spawner;
    
    public void ElapseTime(float deltaTime, EntityCommandBuffer ecb)
    {
        m_Spawner.ValueRW.elapsedTime -= deltaTime;
        if(m_Spawner.ValueRO.elapsedTime <= 0)
        {
            m_Spawner.ValueRW.elapsedTime += m_Spawner.ValueRO.spawnRate;
            Spawn(ecb);
        }
    }

    private void Spawn(EntityCommandBuffer ecb)
    {
        Entity entity;
        if (m_Spawner.ValueRW.random.NextInt(0, 3) == 0) entity = ecb.Instantiate(m_Spawner.ValueRO.entityPrefabRed);
        else if (m_Spawner.ValueRW.random.NextInt(0, 3) == 1) entity = ecb.Instantiate(m_Spawner.ValueRO.entityPrefabPurple);
        else entity = ecb.Instantiate(m_Spawner.ValueRO.entityPrefabOrange);
        float3 direction = math.normalize(m_Spawner.ValueRW.random.NextFloat3(-1, 1));
        float3 position = m_Spawner.ValueRW.random.NextFloat3(-10, 10);
        float speed = m_Spawner.ValueRW.random.NextFloat(.5f, 5f);
        
        ecb.SetComponent(entity, new Speed
        {
            speed = speed,
            direction = direction
        });

        ecb.SetComponent(entity, new WorldTransform
        {
            Position = position
        });

        //Per defecte les entitats no són a prop del jugador
       // ecb.SetComponentEnabled<IsInRangeTag>(entity, false);
    }
}
