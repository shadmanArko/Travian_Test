using UnityEngine;

public class ArmyView : MonoBehaviour, IPoolableView, IArmyView
{
    [SerializeField] private Renderer unitRenderer;
    [SerializeField] private Animator animator;
    
    private MVPContainerComponent mvpContainerComponent;

    public Transform Transform => transform;
    public GameObject GameObject => gameObject;

    private void Awake()
    {
        mvpContainerComponent = gameObject.AddComponent<MVPContainerComponent>();
    }

    public void SetActive(bool active)
    {
        gameObject.SetActive(active);
    }

    public void SetMVPContainer(IMVPContainer container)
    {
        mvpContainerComponent.SetContainer(container);
        
        // Set this view in the presenter
        var presenter = container.GetPresenter<IArmyPresenter>();
        presenter?.SetView(this);
    }

    public void UpdateHealthVisual(float healthPercent)
    {
        var color = Color.Lerp(Color.red, Color.green, healthPercent);
        unitRenderer.material.color = color;
    }

    public void PlayMoveAnimation(bool isMoving)
    {
        animator.SetBool("IsMoving", isMoving);
    }

    public void SetPosition(Vector3 position)
    {
        transform.position = position;
    }

    // Helper method to get model
    public IArmyModel GetModel()
    {
        return mvpContainerComponent.Container.GetModel<IArmyModel>();
    }

    // Pool lifecycle is handled by the factory
}