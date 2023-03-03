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
            Unity.Rendering.URPMaterialPropertyBaseColor colorComponent = default(Unity.Rendering.URPMaterialPropertyBaseColor);

            if (authoring.red)
            {
                colorComponent.Value = new float4(165, 42, 42, 1);
                AddComponent(new RedTag());
            }
            if (authoring.orange)
            {
                colorComponent.Value = new float4(210, 105, 30, 1);
                AddComponent(new OrangeTag());
            }
            if (authoring.purple)
            {
                colorComponent.Value = new float4(153, 50, 204, 1);
                AddComponent(new PurpleTag());
            }


            AddComponent(new EnemyTag());
            AddComponent(colorComponent);
            AddComponent(new Translation { Value = 0});
            //AddComponent(new TimeToLive{ Value = 5, entity =  this.GetEntity()});
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
public struct Translation : IComponentData
{
    public float3 Value;
}
struct TimeToLive : IComponentData
{
    public float Value;
    public Entity entity;
}
