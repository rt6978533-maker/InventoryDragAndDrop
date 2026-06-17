using System;
using UnityEngine;

namespace Inventory
{
    [AddComponentMenu("Inventory/InventoryLogic")]
    public class InventoryLogic : MonoBehaviour
    {
        [SerializeField] private InventoryData _inventoryData;
        [SerializeField] private InventoryRender _inventoryRender;

        public void Swap(int slotIndex, int targetSlotIndex)
        {
            InventoryItems i = _inventoryData.Data[slotIndex];

            _inventoryData.Data[slotIndex] = _inventoryData.Data[targetSlotIndex];
            _inventoryData.Data[targetSlotIndex] = i;

            _inventoryRender.UpdateSlot(slotIndex);
            _inventoryRender.UpdateSlot(targetSlotIndex);
        }
    }
}