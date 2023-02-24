using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

class MovementTagAuthoring : MonoBehaviour
{
    class MovementTagAuthoringBaker : Baker<MovementTagAuthoring>
    {
        public override void Bake(MovementTagAuthoring authoring)
        {
            AddComponent(new MovementTag());
        }
    }
}

public struct MovementTag : IComponentData
{
}