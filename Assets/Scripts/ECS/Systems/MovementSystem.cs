using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

partial class MovementSystem : SystemBase
{

    protected override void OnUpdate()
    {
        float deltaTime = SystemAPI.Time.DeltaTime;

        Entities
            .ForEach((TransformAspect transform, in Speed speedComponent) =>
            {
                transform.WorldPosition += new float3(speedComponent.direction.x, speedComponent.direction.y, speedComponent.direction.z) * speedComponent.speed * deltaTime;
            }).ScheduleParallel();
       
    }
}