using System;

namespace Inventory
{
    public interface IInventoryItems : IInventoryItemsInject, IInventoryItemsUpdate, IDisposable { }
    public interface IInventoryItemsUpdate
    {
        void UpdateUI(InventoryItems i);
    }
    public interface IInventoryItemsInject {
        void Init(InventoryLogic inventoryLogic, int slotIndex);
    }
}