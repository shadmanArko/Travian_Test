//using Cysharp.Threading.Tasks;

public interface IPoolablePresenter
{
    //UniTask InitializeAsync();
    void Reset();
    void Dispose();
}