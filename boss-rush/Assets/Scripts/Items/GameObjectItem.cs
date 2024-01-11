using UnityEngine;

namespace Items
{
    public abstract class GameObjectItem : Item
    {
        [SerializeField] protected GameObject prefab;
        [SerializeField] protected Vector3 spawnPoint;

        public GameObject Prefab => prefab;
        public Vector3 SpawnPoint => spawnPoint;
    }
}