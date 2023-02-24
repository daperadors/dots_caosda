using Unity.Burst;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

// És el sistema de paral·lelització de Unity, cal indicar
// que volem que l'utilitzi i cal posar-ho a tota funció
[BurstCompile]
partial struct SpawnerSystem : ISystem
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
        //No estem a MonoBehaviour, per tal d'accedir a les eines
        //que coneixeu del sistema, hem d'anar al SystemAPI
        float deltaTime = SystemAPI.Time.DeltaTime;

        

        //Aquest sistema farà canvis estructurals (afegir, treure o crear components).
        //És per això que necessitem una referència al commandBuffer.
        //En concret just després d'instanciar, hi ha diversos segons la preferència.
        var ecbSingleton = SystemAPI.GetSingleton<EndInitializationEntityCommandBufferSystem.Singleton>();
        EntityCommandBuffer ecb = ecbSingleton.CreateCommandBuffer(state.WorldUnmanaged);

        //Creem finalment el job en qüestió
        SpawnerJob spawnerJob = new SpawnerJob
        {
            ECB = ecb,
            deltaTime = deltaTime
        };

        //I el llancem
        spawnerJob.Schedule();

        // Realment a l'exemple està poc optimitzat, això hauria de
        // ser un simple sistema probablement i el moviment sí que
        // seria un Job ScheduleParallel amb el seu càlcul de moviment.
    }
}

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