using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEditor.PackageManager;
using static UnityEngine.GraphicsBuffer;

// L'aspecte ens servir� com a interf�cie per al nostre
// component Spawner. Aix� podem tenir-hi depend�ncies,
// incloure tot all� que ens interessa i fer funcions
// per a accedir al codi.
readonly partial struct SpawnerAspect : IAspect
{
    //Aix� �s opcional, �s una refer�ncia a nosaltres
    //si el poseu, autom�ticament queda referenciat.
    //private readonly Entity entity;

    //refer�ncia que farem al nostre component spawner
    private readonly RefRW<Spawner> m_Spawner;
    //refer�ncia que farem al nostre transformAspect
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

        //Per defecte les entitats no s�n a prop del jugador
       // ecb.SetComponentEnabled<IsInRangeTag>(entity, false);
    }
}
