using UnityEngine;

namespace Inventory
{
    public class InventoryFabricItems : InventoryFabric
    {
        [SerializeField] private InventoryItemsHandler _prefab;
        [SerializeField] private Transform _contentParent;
        [SerializeField] private InventoryLogic _inventoryLogic;
        [SerializeField] private RectTransform _dragPanel;

        public override IInventoryItems Create(int slotIndex)
        {
            InventoryItemsHandler value = Instantiate(_prefab, _contentParent);
            value.Init(_inventoryLogic, slotIndex);
            value.Inject(_dragPanel);

            return value;
        }
    }
}