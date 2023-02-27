using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

class SpawnerAuthoring : MonoBehaviour
{
    public List<GameObject> prefabs;

    public float spawnRate;

    [Header("Random")]
    public bool useSeed = false;
    public ushort seed = 1;

    class SpawnerBaker : Baker<SpawnerAuthoring>
    {
        public override void Bake(SpawnerAuthoring authoring)
        {
            List<Entity> entities = new List<Entity>();   

            foreach(var p in authoring.prefabs)
            {
                entities.Add(GetEntity(p));
            }

            AddComponent(new Spawner
            {
                entityPrefabs = entities.ToNativeArray(Allocator.Temp)[0],
                spawnRate = authoring.spawnRate,
                elapsedTime = authoring.spawnRate,
                random = authoring.useSeed ?
                      new Unity.Mathematics.Random(authoring.seed)
                    : new Unity.Mathematics.Random((ushort)UnityEngine.Random.Range(0, 65536))
            });
        }
    }
}

public struct Spawner : IComponentData
{
    //public NativeArray<Entity> entityPrefabs;
    public Entity entityPrefabs;

    public float spawnRate;

    public float elapsedTime;
    public Unity.Mathematics.Random random;
}