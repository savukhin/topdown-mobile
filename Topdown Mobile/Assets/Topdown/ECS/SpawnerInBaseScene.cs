using Unity.Entities;
using UnityEngine;
using Unity.Transforms;
using Unity.Rendering;
using Unity.Mathematics;

public class SpawnerInBaseScene : MonoBehaviour
{
    public Entity Prefab;
    public GameObject Prefab2;
    public float SpawnRate;

    private void Start() {
        MakeEntity();
    }

    void MakeEntity() {
        EntityManager entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;

        SpawnerGO spawner = new SpawnerGO
        {
            Prefab = Prefab2,
            SpawnRate = SpawnRate
        };

        EntityArchetype archetype = entityManager.CreateArchetype(
            // typeof(Transform),
            // typeof(RenderMesh),
            // typeof(RenderBounds),
            // typeof(LocalToWorld),
            typeof(SpawnerGO)
        );

        Entity spawnerEntity = entityManager.CreateEntity(archetype);

        // spawnerEntity.

        // entityManager.AddComponentData(spawnerEntity, new Spawner{
        //     Prefab = Prefab,
        //     SpawnRate = SpawnRate,
        // });

        Debug.Log("Spawner" + spawner);
        entityManager.AddComponentData(spawnerEntity, spawner);

    }

}
