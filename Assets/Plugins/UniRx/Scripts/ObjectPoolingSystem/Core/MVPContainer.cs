//using Cysharp.Threading.Tasks;

public class MVPContainer : IMVPContainer
{
    private IPoolableModel model;
    private IPoolablePresenter presenter;

    public MVPContainer(IPoolableModel model, IPoolablePresenter presenter)
    {
        this.model = model;
        this.presenter = presenter;
    }

    // public async UniTask InitializeAsync()
    // {
    //     if (model != null) await model.InitializeAsync();
    //     if (presenter != null) await presenter.InitializeAsync();
    // }

    public void Reset()
    {
        model?.Reset();
        presenter?.Reset();
    }

    public void Dispose()
    {
        model?.Dispose();
        presenter?.Dispose();
    }

    public T GetModel<T>() where T : class, IPoolableModel => model as T;
    public T GetPresenter<T>() where T : class, IPoolablePresenter => presenter as T;
}