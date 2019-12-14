using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace BackwardsCap
{
    public class Inventory : MonoBehaviour
    {
        public InventorySlot[] Slots;

        public ItemDragManager DragManager;

        [HideInInspector] public bool HoveringOverInventory = false;

        public void Start()
        {
            for (int i = 0; i < Slots.Length; i++)
            {
                Slots[i].Initialize(this);
            }
        }

        public bool PickupItem(Item item)
        {
            if (HoveringOverInventory) return false;
            for (int i = 0; i < Slots.Length; i++)
            {
                if (Slots[i].PickUp(item))
                {
                    item.gameObject.SetActive(false);
                    Debug.Log($"[Inventory]: Picking up {item.name} in slot {i}");
                    UpdateInventoryOrder();
                    return true;
                }
            }
            Debug.Log("[Inventory]: Inventory Full!");
            return false;
        }

        public void UpdateInventoryOrder()
        {
            var children = transform.GetComponentsInChildren<InventorySlot>();
            for (int i = 0; i < children.Length; i++)
            {
                if(children[i].Holding==null)children[i].transform.SetAsLastSibling();
            }
        }

        public void SetHoveringOverInventory(bool isHovering)
        {
            HoveringOverInventory = isHovering;
        }

        public void ItemRemoved(Item item, InventorySlot slot)
        {
            DragManager.StartDragging(item, slot);
        }

    } 
}