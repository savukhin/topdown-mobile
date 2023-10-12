using Unity.Entities;
using Unity.Transforms;
using Unity.Burst;
using Unity.Mathematics;
// using UnityEngine;
using System.Diagnostics;
// using UnityEngine;
// using System;

// [BurstCompile]
public partial struct SpawnerSystem : ISystem
{
    private Random random;

    public void OnCreate(ref SystemState state) { 
        random = new Random(56);
    }

    public void OnDestroy(ref SystemState state) { }

    // [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        // Queries for all Spawner components. Uses RefRW because this system wants
        // to read from and write to the component. If the system only needed read-only
        // access, it would use RefRO instead.
        foreach (RefRW<Spawner> spawner in SystemAPI.Query<RefRW<Spawner>>())
        {
            ProcessSpawner(ref state, spawner);
        }
    }

    private void ProcessSpawner(ref SystemState state, RefRW<Spawner> spawner)
    {
        var ecb = SystemAPI.GetSingleton<BeginFixedStepSimulationEntityCommandBufferSystem.Singleton>().CreateCommandBuffer(state.WorldUnmanaged);
        // If the next spawn time has passed.
        if (spawner.ValueRO.NextSpawnTime < SystemAPI.Time.ElapsedTime)
        {
            UnityEngine.Debug.Log("Process, instantiate");
            // Spawns a new entity and positions it at the spawner.
            // Entity newEntity = ecb.Instantiate(spawner.ValueRO.Prefab);
            Entity newEntity = state.EntityManager.Instantiate(spawner.ValueRO.Prefab);
            // GameObject.Instantiate(spawner.ValueRO.Prefab);
            // LocalPosition.FromPosition returns a Transform initialized with the given position.
            // state.EntityManager.SetComponentData(newEntity, 
            //     new Translation { Value = new float3(Random.N)}
            // );
            // state.EntityManager.SetComponentData(newEntity, LocalTransform.FromPosition(spawner.ValueRO.SpawnPosition));
            state.EntityManager.SetComponentData(newEntity, LocalTransform.FromPosition(random.NextFloat3(
                
            )));

            // Resets the next spawn time.
            spawner.ValueRW.NextSpawnTime = (float)SystemAPI.Time.ElapsedTime + spawner.ValueRO.SpawnRate;
        }
    }
}
