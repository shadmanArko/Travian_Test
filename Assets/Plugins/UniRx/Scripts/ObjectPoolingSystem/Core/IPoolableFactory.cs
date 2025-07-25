using UnityEngine;

public interface IPoolableFactory<T> where T : Component, IPoolableView
{
    T Spawn(Vector3 position, Quaternion rotation, Transform parent = null);
    void Despawn(T obj);
}