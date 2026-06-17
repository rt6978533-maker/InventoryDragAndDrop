using UnityEngine;

namespace Inventory
{
    [CreateAssetMenu(fileName = "InventoryItem", menuName = "Inventory/Items", order = 0)]
    public class InventoryItems : ScriptableObject
    {
        public int ID;
        public string Name;
    }
}