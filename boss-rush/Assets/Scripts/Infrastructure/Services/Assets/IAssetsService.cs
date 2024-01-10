using UnityEngine;

namespace Infrastructure.Services.Assets
{
    public interface IAssetsService
    {
        GameObject Instantiate(string path);
        GameObject Instantiate(string path, Vector3 at);
        GameObject Instantiate(string path, Vector3 at, Transform parent);
        GameObject Instantiate(string path, Transform parent);
    }
}