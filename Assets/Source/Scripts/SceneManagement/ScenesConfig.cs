using Source.Scripts.Data;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Source.Scripts.SceneManagement
{
    [CreateAssetMenu(fileName = "ScenesConfig", menuName = "ScenesConfig", order = 0)]
    public class ScenesConfig : KeyValueStorage<SceneType, AssetReference>
    {
    }
}