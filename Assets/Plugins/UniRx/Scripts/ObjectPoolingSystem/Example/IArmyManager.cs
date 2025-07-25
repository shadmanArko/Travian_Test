using UnityEngine;

public interface IArmyManager
{
    void SpawnArmyUnit(Vector3 position);
    void DespawnUnit(ArmyView unit);
}