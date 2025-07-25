using UnityEngine;

public interface IPoolableView
{
    Transform Transform { get; }
    GameObject GameObject { get; }
    void SetActive(bool active);
    void SetMVPContainer(IMVPContainer container);
}