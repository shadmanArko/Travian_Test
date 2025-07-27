using UnityEngine;

namespace _Scripts.ObstacleSystem.View
{
    public class ObstacleView : MonoBehaviour
    {
        public void SetPosition(Vector3 position)
        {
            transform.position = position;
        }
    
        public void SetActive(bool active)
        {
            gameObject.SetActive(active);
        }
    
        public Vector3 GetPosition()
        {
            return transform.position;
        }
    }
}