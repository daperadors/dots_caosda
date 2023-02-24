using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEditor.PackageManager;
using static UnityEngine.GraphicsBuffer;

// L'aspecte ens servirà com a interfície per al nostre
// component Spawner. Així podem tenir-hi dependències,
// incloure tot allò que ens interessa i fer funcions
// per a accedir al codi.
readonly partial struct SpawnerAspect : IAspect
{
    //Això és opcional, és una referència a nosaltres
    //si el poseu, automàticament queda referenciat.
    //private readonly Entity entity;

    //referència que farem al nostre component spawner
    private readonly RefRW<Spawner> m_Spawner;
    //referència que farem al nostre transformAspect
    private readonly TransformAspect m_TransformAspect;


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
        //new Random((ushort)UnityEngine.Random.Range(0, m_Spawner.ValueRO.entityPrefabs.Length)).NextInt()
        Entity entity = ecb.Instantiate(m_Spawner.ValueRO.entityPrefabs[0]);

        //inicialitzem els components de la entitat spawnejada
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
