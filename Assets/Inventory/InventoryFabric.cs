using UnityEngine;

namespace Inventory
{
    public abstract class InventoryFabric : MonoBehaviour
    {
        public abstract IInventoryItems Create(int slotIndex);
    }
}