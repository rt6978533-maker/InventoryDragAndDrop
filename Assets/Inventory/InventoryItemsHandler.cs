using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Inventory
{
    public class InventoryItemsHandler : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
    {
        [SerializeField] private Text _nameText, _idText;

        private InventoryLogic _inventoryLogic;

        private int _slotIndex;

        public void UpdateUI(InventoryItems i)
        {
            if (i == null)
            {
                _nameText.text = "";
                _idText.text = "";
                return;
            }

            _nameText.text = i.name;
            _idText.text = i.ID.ToString();
        }
        public void Init(InventoryLogic inventoryLogic,  int slotIndex)
        {
            _inventoryLogic = inventoryLogic;
            _slotIndex = slotIndex;
        }
        public int GetSlotIndex() => _slotIndex;

        public void OnBeginDrag(PointerEventData eventData)
        {
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (_inventoryLogic == null) return;

            if (eventData.pointerCurrentRaycast.gameObject.TryGetComponent(out InventoryItemsHandler items))
            {
                _inventoryLogic.Swap(_slotIndex, items.GetSlotIndex());
            }
            else return;
        }

        public void OnDrag(PointerEventData eventData)
        {
            
        }
    }
}