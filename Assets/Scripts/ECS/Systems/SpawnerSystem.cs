using Unity.Burst;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

// És el sistema de paral·lelització de Unity, cal indicar
// que volem que l'utilitzi i cal posar-ho a tota funció
partial class SpawnerSystem : SystemBase
{
    protected override void OnUpdate()
    {
        //No estem a MonoBehaviour, per tal d'accedir a les eines
        //que coneixeu del sistema, hem d'anar al SystemAPI
        float deltaTime = SystemAPI.Time.DeltaTime;

        EntityCommandBuffer ecbSingleton = SystemAPI.GetSingleton<EndInitializationEntityCommandBufferSystem.Singleton>().CreateCommandBuffer(World.Unmanaged);

        /*
        //Creem finalment el job en qüestió
        SpawnerJob spawnerJob = new SpawnerJob
        {
            ECB = ecb,
            deltaTime = deltaTime
        };

        spawnerJob.Schedule();
        */

        foreach (SpawnerAspect spawner in SystemAPI.Query<SpawnerAspect>())
        {
            spawner.ElapseTime(deltaTime, ecbSingleton);
        }
        //ecbSingleton.Playback()
    }
}

/*
//Aquest és el joc que executarà les tasques del nostre sistema
[BurstCompile]
partial struct SpawnerJob : IJobEntity
{
    public EntityCommandBuffer ECB;
    public float deltaTime;

    //Com a paràmetres de l'execute vindriem a posar les condicions de cerca
    //del nostre sistema. En el nostre cas agafem l'aspecte
    //in -> read-only
    //ref -> read-write
    void Execute(ref SpawnerAspect spawner)
    {
        spawner.ElapseTime(deltaTime, ECB);
    }
}
*/