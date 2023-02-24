using System.Collections;
using System.Collections.Generic;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;


//Component (GameObject) per afegir a una entitat i que
//a la seva vegada, li crearà un Component (Entitat)
class SpeedAuthoring : MonoBehaviour
{
    //Paràmetres que tocarem des del GameObject per a veure'ls
    //al Inspector i futurament li passarem a l'Entitat
    public float speed;
    public Vector3 direction;

    //Classe interior per tal que el sistema intern de Unity
    //sigui capaç de convertir del MonoBehaviour a Entitat
    //i li posi els Components i valors corresponents.
    class SpeedBaker : Baker<SpeedAuthoring>
    {
        public override void Bake(SpeedAuthoring authoring)
        {
            //S'afegeix l'estructura i se li inicialitzen els valors
            AddComponent(new Speed
            {
                speed = authoring.speed,
                direction = authoring.direction.normalized
            });
        }
    }
}

//El component que tindrà l'Entitat.
//Només són dades
public struct Speed : IComponentData
{
    public float speed;
    public float3 direction;
}