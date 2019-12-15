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

        private Color priorColor;

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
            priorColor = draggingRenderer.color;
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

        (bool,WorkStation) CheckIfOverValidMachine()
        {
            var wp = mainCam.ScreenToWorldPoint(Input.mousePosition);

            RaycastHit2D hit = Physics2D.Raycast(wp, Vector2.zero, 100f, LayerMask.GetMask("WorkStations"));
            if (hit.transform != null)
            {
                var workStation = hit.transform.GetComponent<WorkStation>();
                if (workStation.CompareTag("Forge")&& Dragging.ItemState != Item.State.Normal) return (true, null);
                
                if (workStation.InMachine != null) return (true,null);
                if (Vector3.Distance(Player.transform.position.xy(), new Vector3(hit.point.x, hit.point.y, 0)) < 2f)
                    return (true,workStation);
            }
            return (false,null);
        }

        NPC CheckIfOverNpc()
        {
            var wp = mainCam.ScreenToWorldPoint(Input.mousePosition);

            RaycastHit2D hit = Physics2D.Raycast(wp, Vector2.zero, 100f, LayerMask.GetMask("Seeking"));
            if (hit.transform != null)
            {
                if (Vector3.Distance(Player.transform.position.xy(), new Vector3(hit.point.x, hit.point.y, 0)) < 2f)
                {
                    if (hit.transform.CompareTag("Seeking"))
                    {
                        var npc = hit.transform.GetComponentInParent<NPC>();
                        return npc.Satisfied ? null : npc;
                    }
                    else
                    {
                        var npc = hit.transform.GetComponent<NPC>();
                        return npc.Satisfied?null:npc;
                    }
                }
            }
            return null;
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
            draggingRenderer.color = priorColor;
            var wp = mainCam.ScreenToWorldPoint(Input.mousePosition);
            var workStation = CheckIfOverValidMachine();
            var npc = CheckIfOverNpc();
            if (npc != null)
            {
                var score = npc.ScoreItem(Dragging);
                ScoreManager.instance.AddPoints(score);
                Destroy(Dragging.gameObject);
                npc.GenerateNewRequest();
                inventory.UpdateInventoryOrder();
            }
            else if (workStation.Item1)
            {
                if (workStation.Item2 != null)
                {
                    workStation.Item2.LoadItem(Dragging);
                    inventory.UpdateInventoryOrder();
                }
                else
                {
                    if (itemWasIn.PickUp(Dragging))
                        Dragging.gameObject.SetActive(false);
                }
            }
            else if (inventory.HoveringOverInventory || !CheckIfValidPlacement())
            {
                Debug.Log("[ItemDragManager]: Failed to place item!");
                if(itemWasIn.PickUp(Dragging))
                    Dragging.gameObject.SetActive(false);
            }
            else
            {

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
            var t = CheckIfOverValidMachine();
            draggingRenderer.color = (!t.Item1&&CheckIfValidPlacement()) || (t.Item1 && t.Item2!=null)? Color.green : new Color(0.5f,0.5f,0.5f,0.5f);
            Dragging.transform.position = wp.xy();
        }

    } 
}
