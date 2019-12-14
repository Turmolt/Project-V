using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace BackwardsCap
{
    [RequireComponent(typeof(Inventory))]
    public class ItemDragManager : MonoBehaviour
    {
        [HideInInspector] public bool DraggingItem = false;

        [HideInInspector] public Item Dragging;

        private Camera mainCam;

        public Tilemap FloorTilemap;

        private Inventory inventory;

        private InventorySlot itemWasIn;



        void Start()
        {
            inventory = GetComponent<Inventory>();
            mainCam = Camera.main;
        }

        public void StartDragging(Item item, InventorySlot from)
        {
            if (Dragging != null)
            {
                Debug.LogError("[ItemDragManager]: Trying to drag 2 items?");
            }
            DraggingItem = true;
            itemWasIn = from;
            Dragging = item;
            Dragging.gameObject.SetActive(true);
        }

        void Update()
        {
            if (DraggingItem && Input.GetMouseButton(0))
            {
                FollowMouse();
            }

            if (Input.GetMouseButtonUp(0))
            {
                if (DraggingItem)
                {
                    DropItem();
                }
            }
        }

        void DropItem()
        {
            DraggingItem = false;
            var wp = mainCam.ScreenToWorldPoint(Input.mousePosition);
            var inventoryRect = inventory.GetComponent<RectTransform>();
            if (inventory.HoveringOverInventory)
            {
                Debug.Log("[ItemDragManager]: Failed to place item!");
                if(itemWasIn.PickUp(Dragging))
                    Dragging.gameObject.SetActive(false);
                Dragging = null;
                itemWasIn = null;
            }
            else
            {
                Debug.Log($"[ItemDragManager]: Dropped Item at {wp}");
                Dragging.transform.position = wp.xy();
                inventory.UpdateInventoryOrder();
                Dragging = null;
                itemWasIn = null;
            }

        }

        void FollowMouse()
        {
            var wp = mainCam.ScreenToWorldPoint(Input.mousePosition);
            Dragging.transform.position = wp.xy();
        }

    } 
}
