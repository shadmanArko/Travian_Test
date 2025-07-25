// using System.Collections.Generic;
// using UnityEngine;
//
// public class ArmyManager : IArmyManager
// {
//     private readonly ArmyFactory armyFactory;
//     private readonly List<ArmyView> spawnedUnits = new List<ArmyView>();
//
//     public ArmyManager(ArmyFactory armyFactory)
//     {
//         this.armyFactory = armyFactory;
//     }
//
//     public void SpawnArmyUnit(Vector3 position)
//     {
//         var unit = armyFactory.Spawn(position, Quaternion.identity);
//         spawnedUnits.Add(unit);
//         
//         // Get the model to do business logic
//         var model = unit.GetModel();
//         model.MoveTo(position + Vector3.forward * 10);
//     }
//
//     public void DespawnUnit(ArmyView unit)
//     {
//         spawnedUnits.Remove(unit);
//         armyFactory.Despawn(unit);
//     }
// }