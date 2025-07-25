using UnityEngine;
using Zenject;

public class GameController : MonoBehaviour
{
    [Inject] private IArmyManager armyManager;

    private void Start()
    {
        // Spawn some army units
        armyManager.SpawnArmyUnit(Vector3.zero);
        armyManager.SpawnArmyUnit(Vector3.right * 5);
    }
}