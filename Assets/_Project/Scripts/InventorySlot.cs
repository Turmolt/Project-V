using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace BackwardsCap
{
    public class InventorySlot : MonoBehaviour , IPointerDownHandler
    {
        public Image Display;

        public Item Holding;

        private Inventory inventory;

        private int inventoryId;

        private Sprite blankSlotSprite;

        public QualityBar qualityBar;

        public void Start()
        {
            blankSlotSprite = Display.sprite;
            qualityBar.gameObject.SetActive(false);
        }

        public void Initialize(int id, Inventory parent)
        {
            inventoryId = id;
            inventory = parent;
        }

        public bool PickUp(Item item)
        {
            if (Holding != null) return false;
            
            Display.sprite = item.Image;
            Holding = item;
            qualityBar.barImage.fillAmount = item.Quality/100f;
            qualityBar.gameObject.SetActive(true);
            return true;
        }

        public Item PopItem()
        {
            var item = Holding;
            Holding = null;
            qualityBar.gameObject.SetActive(false);
            Display.sprite = blankSlotSprite;
            return item;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (Holding != null)
            {
                Debug.Log($"[InventorySlot {inventoryId}]: Item Drag Start");
                inventory.ItemRemoved(PopItem(), this);
            }
        }
    } 
}