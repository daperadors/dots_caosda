using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

// Forma bàsica de crear sistemes senzills.
partial class MovementSystem : SystemBase
{
    protected override void OnUpdate()
    {
        //No estem a MonoBehaviour, per tal d'accedir a les eines
        //que coneixeu del sistema, hem d'anar al SystemAPI.
        //A més dins la Query sou a Burst i no podeu accedir a la
        //SystemAPI.
        float deltaTime = SystemAPI.Time.DeltaTime;

        // Farem una cerca d'entitats amb la condició que busquem el TransformAspect i el nostre component de Speed.
        // Els Aspects són un nou sistema d'Entities per tal d'agrupar diferents components i comportaments
        // i així poder separar i organitzar el codi, de moment no ho fem servir pel nostre compte.

        //Codi antic de DOTs, encara suportat
        Entities
            //.WithAll<MovementTag>() //Aquí podriem posar condicions, sense agafar el seu valor. Útil per a etiquetes
            .ForEach((TransformAspect transform, in Speed speedComponent) =>
            {
                //Iterem per cada entitat que tingui Speed i accedim al seu TransformAspect
                transform.WorldPosition += new float3(speedComponent.direction.x, speedComponent.direction.y, speedComponent.direction.z) * speedComponent.speed * deltaTime;

                // Cal que indiquem com serà executat aquest job que genera el Entity Query
                // -> Run (main thread)
                // -> Schedule (single thread, async)
                // -> ScheduleParallel (multiple threads, async)
            }).ScheduleParallel();
        

        //Nova sintaxi de DOTs només per a executar al MainThread
        /*
        // Utilitzem la coneguda sintaxi del foreach per a iterar per a totes les Entitats.
        // Al agafar components individuals (que no Aspects) cal indicar si fem Read Only o Read Write
        // com podeu veure al Speed
        foreach (var (transform, speed) in SystemAPI.Query<TransformAspect, RefRO<Speed>>())
        {
            //Com és un RefRO primer hem d'accedir al seu ValueRO
            float3 direction = speed.ValueRO.direction;
            transform.WorldPosition += new float3(direction.x, direction.y, direction.z) * speed.ValueRO.speed * deltaTime;
        }
        */
    }
}