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

        private SpriteRenderer draggingRenderer;

        private Camera mainCam;

        public PlayerController Player;

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
            draggingRenderer = item.gameObject.GetComponent<SpriteRenderer>();
            Dragging.gameObject.SetActive(true);
        }

        void Update()
        {
            if (DraggingItem)
            {
                if (Input.GetMouseButton(0))
                {
                    FollowMouse();
                }

                if (Input.GetMouseButtonUp(0))
                {
                    DropItem();
                }
            }
        }

        bool CheckIfValidPlacement()
        {
            var wp = mainCam.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(wp, Vector2.zero, 100f, LayerMask.GetMask("Floor"));
            if (hit.transform != null)
            {
                if(Vector3.Distance(Player.transform.position.xy(),new Vector3(hit.point.x,hit.point.y,0))<2f)
                    return true;
            }
            return false;
        }

        void DropItem()
        {
            DraggingItem = false;
            draggingRenderer.color = Color.white;
            var wp = mainCam.ScreenToWorldPoint(Input.mousePosition);
            if (inventory.HoveringOverInventory || !CheckIfValidPlacement())
            {
                Debug.Log("[ItemDragManager]: Failed to place item!");
                if(itemWasIn.PickUp(Dragging))
                    Dragging.gameObject.SetActive(false);
            }
            else
            {
//                Debug.Log($"[ItemDragManager]: Dropped Item at {wp}");
                Dragging.transform.position = wp.xy();
                inventory.UpdateInventoryOrder();
            }
            Dragging = null;
            draggingRenderer = null;
            itemWasIn = null;
        }

        void FollowMouse()
        {
            var wp = mainCam.ScreenToWorldPoint(Input.mousePosition);
            draggingRenderer.color = CheckIfValidPlacement() ? Color.green : Color.red;
            Dragging.transform.position = wp.xy();
        }

    } 
}
