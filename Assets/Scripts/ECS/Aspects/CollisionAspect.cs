
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEditor.PackageManager;
using UnityEngine;

readonly partial struct CollisionAspect : IAspect
{
    private readonly RefRW<TimeToLive> m_TimeToLive;
    public void ControlTime(float deltaTime, EntityCommandBuffer ecb)
    {
        m_TimeToLive.ValueRW.Value -= deltaTime;
        if (m_TimeToLive.ValueRO.Value <= 0)
        {
            DestroyEntity(m_TimeToLive.ValueRO.entity, ecb);
        }
    }

    private void DestroyEntity(Entity entity, EntityCommandBuffer ecb)
    {
        ecb.DestroyEntity(entity);
        Debug.Log("Deleted");
    }

}
