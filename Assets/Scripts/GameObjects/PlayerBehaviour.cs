using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Entities;
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


    private static PlayerBehaviour m_Instance;
    public static PlayerBehaviour Instance => m_Instance;

    private float m_RadiusSQ;
    public float RangeRadiusSQ => m_RadiusSQ;

    private Unity.Mathematics.float3 m_Position;
    public Unity.Mathematics.float3 Position => m_Position;

    private void Awake()
    {
        if (m_Instance == null)
            m_Instance = this;
        else
            Destroy(gameObject);

        m_Position = transform.position;

    }

    private void Update()
    {
        Vector3 movement = Vector3.zero;
        if (Input.GetKey(KeyCode.W))
            movement += Vector3.forward;
        if (Input.GetKey(KeyCode.S))
            movement -= Vector3.forward;
        if (Input.GetKey(KeyCode.A))
            movement -= Vector3.right;
        if (Input.GetKey(KeyCode.D))
            movement += Vector3.right;
        if(Input.GetKey(KeyCode.Q))
            movement += Vector3.up;
        if (Input.GetKey(KeyCode.E))
            movement -= Vector3.up;

        transform.position += movement.normalized * m_MovementSpeed * Time.deltaTime;
        m_Position = transform.position;

    }

    private void LateUpdate()
    {
        /*EntityQuery inRangeEntitiesQuery = World.DefaultGameObjectInjectionWorld.EntityManager.CreateEntityQuery(typeof(IsInRangeTag));
        Unity.Collections.NativeArray<Entity> inRangeEntities = inRangeEntitiesQuery.ToEntityArray(Unity.Collections.Allocator.Temp);

        m_Text.text = inRangeEntities.Length + "";
        foreach(Entity entity in inRangeEntities)
        {
            Vector3 entityPosition= World.DefaultGameObjectInjectionWorld.EntityManager.GetComponentData<WorldTransform>(entity).Position;
            Debug.DrawLine(transform.position, entityPosition, Color.green);
        }*/
    }
}
