using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

[BurstCompile]
[UpdateBefore(typeof(CollisionSystem))]
public partial class UpdateTranslationSystem : SystemBase
{
    protected override void OnUpdate()
    {
        Entities.WithAll<Translation, LocalToWorld>().ForEach((ref Translation translation, in LocalToWorld localToWorld) =>
        {
            translation.Value = localToWorld.Position;

        }).ScheduleParallel();
    }
}
