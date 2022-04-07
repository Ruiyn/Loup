using System.Collections;
using System.Collections.Generic;
using MoreMountains.Tools;
using UnityEngine;

namespace MoreMountains.InventoryEngine
{
    [CreateAssetMenu(fileName = "HeldItem", menuName = "MoreMountains/InventoryEngine/HeldItem", order = 0)]
    public class HeldItem : InventoryItem
    {
        public int Strength;

        public override bool Equip(string playerID)
        {
            base.Equip(playerID);



            if (TargetInventory(playerID).Owner == null)
            {
                return false;
            }

            Debug.Log("Fuck");
            CharacterStats.updateStats(ref CharacterStats.Str, Strength);

            return true;
        }
    }
}
