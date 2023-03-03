using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using TMPro;
using Unity.Entities;
using Unity.Rendering;
using Unity.Transforms;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBehaviour : MonoBehaviour
{
    [SerializeField]
    private float m_MovementSpeed = 5f;

    [SerializeField]
    private float m_RadiusIncreaseSpeed = 1f;

    [SerializeField]
    private TextMeshProUGUI m_Text;

    [SerializeField]
    private Material m_Material;

    private static PlayerBehaviour m_Instance;
    public static PlayerBehaviour Instance => m_Instance;

    [SerializeField] private float m_RadiusSQ;
    public float RangeRadiusSQ => m_RadiusSQ;

    EntityManager entityManager;

    private Unity.Mathematics.float3 m_Position;
    public Unity.Mathematics.float3 Position => m_Position;
    public Unity.Rendering.URPMaterialPropertyBaseColor colorComponent;
    public float speed = 5f;
    float mouseX;
    float mouseY;

    private void Awake()
    {
        if (m_Instance == null)
            m_Instance = this;
        else
            Destroy(gameObject);
        entityManager= World.DefaultGameObjectInjectionWorld.EntityManager;
        colorComponent = default(Unity.Rendering.URPMaterialPropertyBaseColor);
        colorComponent.Value = new Unity.Mathematics.float4(0, 1, 0, 1);
        UpdateRadius();
    }

    private void UpdateRadius()
    {
        m_RadiusSQ = transform.localScale.x / 2;
        m_RadiusSQ *= m_RadiusSQ;
    }

    private void Update()
    {
        m_Position = transform.position;
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(horizontal, 0f, vertical) * speed * Time.deltaTime;

        mouseX += Input.GetAxis("Mouse X");
        mouseY -= Input.GetAxis("Mouse Y");

        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) movement += Vector3.up * speed * Time.deltaTime;
        if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl)) movement += Vector3.down * speed * Time.deltaTime;


        transform.Translate(movement, Space.Self);
        transform.rotation = Quaternion.Euler(mouseY, mouseX, 0f);


    }

    private void LateUpdate()
    {
        EntityQuery enemyQueryCollision = entityManager.CreateEntityQuery(typeof(TimeToLive));
        Unity.Collections.NativeArray<Entity> enemiesEntities = enemyQueryCollision.ToEntityArray(Unity.Collections.Allocator.Temp);
        if(enemiesEntities.Length>0)
            Grow(enemiesEntities.Length);
        /*
       foreach(Entity entity in enemiesEntities)
        {
            if (World.DefaultGameObjectInjectionWorld.EntityManager.Exists(entity))
            {
                Vector3 entityPosition = World.DefaultGameObjectInjectionWorld.EntityManager.GetComponentData<WorldTransform>(entity).Position;

                if (Vector2.Distance(entityPosition, transform.position) < 2f)
                {

                    entityManager.RemoveComponent<URPMaterialPropertyBaseColor>(entity);
                    entityManager.AddComponentData<URPMaterialPropertyBaseColor>(entity, colorComponent);
                     transform.localScale += (Vector3.one / 5);
                }

            }
            
        }
        */
    }

    internal void Grow(int amount)
    {
        transform.localScale = transform.localScale + Vector3.one * amount * Time.deltaTime;
        UpdateRadius();
    }
}
