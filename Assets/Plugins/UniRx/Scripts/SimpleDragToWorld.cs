// using UnityEngine;
// using UnityEngine.EventSystems;
// using Zenject;
//
// public class SimpleDragToWorld : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
// {
//     [SerializeField] private BuildingConfigSO buildingConfig;
//     [SerializeField] private Canvas parentCanvas;
//     
//     // Injected dependencies
//     [Inject] private Camera _worldCamera;
//     [Inject] private IBuildingManager _buildingManager;
//     
//     private GameObject _dragInstance;
//     private RectTransform _dragInstanceRect;
//     
//     private void Awake()
//     {
//         if (parentCanvas == null)
//             parentCanvas = GetComponentInParent<Canvas>();
//     }
//     
//     public void OnBeginDrag(PointerEventData eventData)
//     {
//         // Create instance of this UI element
//         _dragInstance = Instantiate(gameObject, parentCanvas.transform);
//         _dragInstanceRect = _dragInstance.GetComponent<RectTransform>();
//         
//         // Remove the drag script from instance to avoid conflicts
//         var dragScript = _dragInstance.GetComponent<SimpleDragToWorld>();
//         if (dragScript != null)
//             Destroy(dragScript);
//         
//         // Make instance semi-transparent
//         var canvasGroup = _dragInstance.GetComponent<CanvasGroup>();
//         if (canvasGroup == null)
//             canvasGroup = _dragInstance.AddComponent<CanvasGroup>();
//         canvasGroup.alpha = 0.7f;
//         canvasGroup.blocksRaycasts = false;
//         
//         // Set initial position
//         _dragInstanceRect.position = Input.mousePosition;
//     }
//     
//     public void OnDrag(PointerEventData eventData)
//     {
//         if (_dragInstance != null)
//         {
//             _dragInstanceRect.position = Input.mousePosition;
//         }
//     }
//     
//     public void OnEndDrag(PointerEventData eventData)
//     {
//         // Clean up drag instance
//         if (_dragInstance != null)
//         {
//             Destroy(_dragInstance);
//             _dragInstance = null;
//         }
//         
//         // If not over UI, create building
//         if (!EventSystem.current.IsPointerOverGameObject())
//         {
//             Vector2 worldPos = _worldCamera.ScreenToWorldPoint(Input.mousePosition);
//             CreateBuilding(worldPos);
//         }
//     }
//     
//     private void CreateBuilding(Vector2 position)
//     {
//         _buildingManager?.PlaceBuilding(buildingConfig, position);
//     }
// }