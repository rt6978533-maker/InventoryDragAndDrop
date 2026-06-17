using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Inventory
{
    public interface IInjectDragObject { void Inject(RectTransform dragPanel); }

    public class InventoryItemsHandler : MonoBehaviour, IBeginDragHandler, 
        IEndDragHandler, IDragHandler, IInventoryItems, IInjectDragObject
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

            _nameText.text = i.Name;
            _idText.text = i.ID.ToString();
        }
        public void Init(InventoryLogic inventoryLogic, int slotIndex)
        {
            _inventoryLogic = inventoryLogic;
            _slotIndex = slotIndex;
        }
        public void Dispose() => Destroy(gameObject);
        public int GetSlotIndex() => _slotIndex;

        public void OnEndDrag(PointerEventData eventData)
        {
            SetActiveObjectDrag(false);

            if (_inventoryLogic == null) return;

            if (eventData.pointerCurrentRaycast.gameObject.TryGetComponent(out InventoryItemsHandler items))
            {
                _inventoryLogic.Swap(_slotIndex, items.GetSlotIndex());
            }
            else return;
        }

        private RectTransform _dragPanel;

        public void Inject(RectTransform dragPanel) => _dragPanel = dragPanel;
        private void SetActiveObjectDrag(bool value)
        {
            if (_dragPanel == null)
            {
                Debug.LogError("[InventoryItemsHandler][SetActiveObjectDrag] _dragPanel is null");
                return;
            }

            _dragPanel.gameObject.SetActive(value);
        }

        public void OnBeginDrag(PointerEventData eventData) => SetActiveObjectDrag(true);

        public void OnDrag(PointerEventData eventData)
        {
            if (_dragPanel == null) return;

            _dragPanel.position = eventData.position;
        }
    }
}