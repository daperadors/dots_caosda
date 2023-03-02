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
                colorComponent.Value = new float4(1, 0, 0, 1);
                AddComponent(new RedTag());
            }
            if (authoring.orange)
            {
                colorComponent.Value = new float4(0, 35, 100, 0);
                AddComponent(new OrangeTag());
            }

            if (authoring.purple)
            {
                colorComponent.Value = new float4(0, 1, 0, 0.5f);
                AddComponent(new PurpleTag());
            }


            AddComponent(new EnemyTag());
            AddComponent(colorComponent);

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