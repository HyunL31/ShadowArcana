using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class ResourceManager : MonoBehaviour
{
    public static ResourceManager Instance;

    private Dictionary<string, AsyncOperationHandle> _handles = new Dictionary<string, AsyncOperationHandle>();

    private void Awake()
    {
        Instance = this;
    }

    public void LoadAsset<T>(string path, Action<T> callback) where T : UnityEngine.Object
    {
        if (_handles.TryGetValue(path, out AsyncOperationHandle handle))
        {
            callback?.Invoke(handle.Result as T);
            return;
        }

        AsyncOperationHandle<T> loadHandle = Addressables.LoadAssetAsync<T>(path);

        loadHandle.Completed += (op) =>
        {
            if (loadHandle.Status == AsyncOperationStatus.Succeeded)
            {
                _handles[path] = op;
                callback?.Invoke(op.Result);
            }
        };
    }

    public void InstantiatePrefab(string path, Transform parent, Action<GameObject> callback)
    {
        Addressables.InstantiateAsync(path, parent).Completed += (op) =>
        {
            callback?.Invoke(op.Result);
        };
    }
}
