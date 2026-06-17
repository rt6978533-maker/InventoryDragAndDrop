using System;
using UnityEngine;
using UnityEngine.UIElements;

namespace Inventory
{
    [AddComponentMenu("Inventory/InventoryRender")]
    public class InventoryRender : MonoBehaviour
    {
        [SerializeField] private InventoryData _inventoryData;
        [SerializeField] private InventoryLogic _inventoryLogic;

        [SerializeField] private InventoryItemsHandler _prefabCreate;
        [SerializeField] private Transform _parentContext;

        private InventoryItemsHandler[] _bufferHandler;

        private void Start() => UpdateInventory();

        public void ClearInventory()
        {
            if (_bufferHandler != null && _bufferHandler.Length > 0)
            {
                foreach (var item in _bufferHandler)
                {
                    Destroy(item.gameObject);
                }
            }
        }

        public void CreateInventory()
        {
            int length = _inventoryData.Data.Count;
            if (_inventoryData == null || length == 0) {
                _bufferHandler = new InventoryItemsHandler[0];
                return;
            }
            ClearInventory();

            _bufferHandler = new InventoryItemsHandler[length];

            for (int index = 0; index < length; index++)
            {
                _bufferHandler[index] = Instantiate(_prefabCreate, _parentContext.transform);
                _bufferHandler[index].Init(_inventoryLogic, index);
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