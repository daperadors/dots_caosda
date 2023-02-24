using Unity.Burst;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

// �s el sistema de paral�lelitzaci� de Unity, cal indicar
// que volem que l'utilitzi i cal posar-ho a tota funci�
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

        

        //Aquest sistema far� canvis estructurals (afegir, treure o crear components).
        //�s per aix� que necessitem una refer�ncia al commandBuffer.
        //En concret just despr�s d'instanciar, hi ha diversos segons la prefer�ncia.
        var ecbSingleton = SystemAPI.GetSingleton<EndInitializationEntityCommandBufferSystem.Singleton>();
        EntityCommandBuffer ecb = ecbSingleton.CreateCommandBuffer(state.WorldUnmanaged);

        //Creem finalment el job en q�esti�
        SpawnerJob spawnerJob = new SpawnerJob
        {
            ECB = ecb,
            deltaTime = deltaTime
        };

        //I el llancem
        spawnerJob.Schedule();

        // Realment a l'exemple est� poc optimitzat, aix� hauria de
        // ser un simple sistema probablement i el moviment s� que
        // seria un Job ScheduleParallel amb el seu c�lcul de moviment.
    }
}

//Aquest �s el joc que executar� les tasques del nostre sistema
[BurstCompile]
partial struct SpawnerJob : IJobEntity
{
    public EntityCommandBuffer ECB;
    public float deltaTime;

    //Com a par�metres de l'execute vindriem a posar les condicions de cerca
    //del nostre sistema. En el nostre cas agafem l'aspecte
    //in -> read-only
    //ref -> read-write
    void Execute(ref SpawnerAspect spawner)
    {
        spawner.ElapseTime(deltaTime, ECB);
    }
}