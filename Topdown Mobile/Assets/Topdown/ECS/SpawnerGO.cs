using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public class SpawnerGO : IComponentData
{
    public GameObject Prefab;
    public float3 SpawnPosition;
    public float NextSpawnTime;
    public float SpawnRate;
}
