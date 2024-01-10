using System.Collections.Generic;
using UnityEngine;

namespace Infrastructure.Services.Assets
{
    public class AssetsProvider : IAssetsProvider
    {
        private readonly Dictionary<string, GameObject> cashedResources = new();

        public GameObject LoadResource(string path, bool cashed = true)
        {
            cashedResources.TryGetValue(path, out GameObject prefab);
            if (prefab != null)
            {
                return prefab;
            }
            
            prefab = Resources.Load<GameObject>(path);
            if (cashed)
            {
                cashedResources[path] = prefab;   
            }
            return prefab;
        }
    }
}