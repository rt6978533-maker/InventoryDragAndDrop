using System;
using UnityEngine;

namespace Inventory
{
    [AddComponentMenu("Inventory/InventoryRender")]
    public class InventoryRender : MonoBehaviour
    {
        [SerializeField] private InventoryData _inventoryData;

        [SerializeField] private InventoryFabric _inventoryFabric;

        private IInventoryItems[] _bufferHandler;

        private void Start() => UpdateInventory();

        public void ClearInventory()
        {
            if (_bufferHandler != null && _bufferHandler.Length > 0)
            {
                foreach (var item in _bufferHandler)
                    item.Dispose();
            }
        }

        public void CreateInventory()
        {
            ClearInventory();

            if (_inventoryData == null)
            {
                Debug.LogError("[InventoryRender] _inventoryData is null.");
                return;
            }
            int length = _inventoryData.Data.Count;
            if (length == 0) {
                _bufferHandler = new IInventoryItems[0];
                return;
            }

            _bufferHandler = new IInventoryItems[length];

            for (int index = 0; index < length; index++)
            {
                _bufferHandler[index] = _inventoryFabric.Create(index);
            }
        }

        public void UpdateSlot(int index)
        {
            if (_bufferHandler.Length <= index || _inventoryData.Data.Count <= index)
            {
                throw new ArgumentOutOfRangeException(nameof(index));
            }

            _bufferHandler[index].UpdateUI(_inventoryData.Data[index]);
        }

        public void UpdateInventory()
        {
            if (_inventoryData == null ||
                _bufferHandler == null ||
                _bufferHandler.Length != _inventoryData.Data.Count) CreateInventory();

            for (int index = 0; index < _bufferHandler.Length; index++) UpdateSlot(index);
        }
    }
}