//using Cysharp.Threading.Tasks;

public interface IMVPContainer
{
    //UniTask InitializeAsync();
    void Reset();
    void Dispose();
    T GetModel<T>() where T : class, IPoolableModel;
    T GetPresenter<T>() where T : class, IPoolablePresenter;
}