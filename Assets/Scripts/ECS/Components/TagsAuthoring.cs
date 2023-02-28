using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

class TagsAuthoring : MonoBehaviour
{
    public bool red;
    public bool orange;
    public bool purple;
    class TagsAuthoringBaker : Baker<TagsAuthoring>
    {

        public override void Bake(TagsAuthoring authoring)
        {
            if (authoring.red) AddComponent(new RedTag());
            if (authoring.orange) AddComponent(new PurpleTag());
            if (authoring.purple) AddComponent(new OrangeTag());

            AddComponent(new EnemyTag());
        }
    }
}

public struct RedTag : IComponentData
{
}
public struct PurpleTag : IComponentData
{
}
public struct OrangeTag : IComponentData
{
}
public struct EnemyTag : IComponentData
{
}