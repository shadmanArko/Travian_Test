using UnityEngine;

namespace _Scripts.Configs
{
    [CreateAssetMenu(fileName = "GameConfig", menuName = "Configs/GameConfig", order = 0)]
    public class GameConfig : ScriptableObject
    {
        [Header("Player Settings")]
        public float jumpForce = 10f;
        public float gravity = -20f;
        
        [Header("Obstacle Settings")]
        public float obstacleSpeed = 5f;
        public float spawnInterval = 1f;
        public float minHeignt = -2f;
        public float maxHeignt = 2f;
        public float despawnDistance = -10f;

        [Header("GameSettings")] public float scoreMultiplier = 10f;
    }
}