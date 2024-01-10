using UnityEngine;

namespace Infrastructure.Services.Assets
{
    public interface IAssetsProvider
    {
        GameObject LoadResource(string path, bool cashed = true);
    }
}