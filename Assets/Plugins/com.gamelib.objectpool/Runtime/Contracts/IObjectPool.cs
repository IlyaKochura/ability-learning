using ObjectPool.Runtime.Contracts;
using UnityEngine;

namespace ObjectPool.Contracts
{
    public interface IObjectPool
    {
        public T Spawn<T>(GameObject prefab, Vector3 position, Quaternion quaternion, Transform parent = null)
            where T : Component, IRecycle;
        
        public T Spawn<T>(GameObject prefab, Transform parent = null)
            where T : Component, IRecycle;
    }
}