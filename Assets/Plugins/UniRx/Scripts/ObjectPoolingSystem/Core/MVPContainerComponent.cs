using UnityEngine;

public class MVPContainerComponent : MonoBehaviour
{
    public IMVPContainer Container { get; private set; }

    public void SetContainer(IMVPContainer container)
    {
        Container = container;
    }
}