// using UnityEngine;
// using UnityEngine.Pool;
// using Zenject;
//
// public abstract class PoolableFactoryBase<TView, TModel, TPresenter> : IPoolableFactory<TView>
//     where TView : Component, IPoolableView
//     where TModel : class, IPoolableModel
//     where TPresenter : class, IPoolablePresenter
// {
//     protected readonly DiContainer container;
//     protected readonly TView prefab;
//     protected ObjectPool<TView> pool;
//
//     protected PoolableFactoryBase(DiContainer container, TView prefab)
//     {
//         this.container = container;
//         this.prefab = prefab;
//         InitializePool();
//     }
//
//     private void InitializePool()
//     {
//         pool = new ObjectPool<TView>(
//             createFunc: CreatePooledObject,
//             actionOnGet: OnGet,
//             actionOnRelease: OnRelease,
//             actionOnDestroy: OnDestroy,
//             collectionCheck: true,
//             defaultCapacity: 10,
//             maxSize: 100
//         );
//     }
//
//     private TView CreatePooledObject()
//     {
//         var viewInstance = GameObject.Instantiate(prefab);
//         
//         // Create Model and Presenter through Zenject
//         var model = container.Resolve<TModel>();
//         var presenter = container.Resolve<TPresenter>();
//         
//         // Create MVP Container
//         var mvpContainer = new MVPContainer(model, presenter);
//         
//         // Inject dependencies into view
//         container.Inject(viewInstance);
//         
//         // Set MVP container in view
//         viewInstance.SetMVPContainer(mvpContainer);
//         
//         return viewInstance;
//     }
//
//     private async void OnGet(TView obj)
//     {
//         await obj.GetComponent<MVPContainerComponent>().Container.InitializeAsync();
//         obj.SetActive(true);
//     }
//
//     private void OnRelease(TView obj)
//     {
//         obj.SetActive(false);
//         obj.GetComponent<MVPContainerComponent>().Container.Reset();
//     }
//
//     private void OnDestroy(TView obj)
//     {
//         obj.GetComponent<MVPContainerComponent>().Container.Dispose();
//         GameObject.Destroy(obj.gameObject);
//     }
//
//     public TView Spawn(Vector3 position, Quaternion rotation, Transform parent = null)
//     {
//         var obj = pool.Get();
//         obj.Transform.position = position;
//         obj.Transform.rotation = rotation;
//         if (parent != null)
//             obj.Transform.SetParent(parent);
//         return obj;
//     }
//
//     public void Despawn(TView obj)
//     {
//         pool.Release(obj);
//     }
// }