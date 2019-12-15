using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BackwardsCap
{
    public class SeekingQueueSlot : MonoBehaviour
    {
        public Image Display;
        public Item Seeking;

        public QualityBar qualityBar;

        private Sprite blankSlotSprite;

        public void Start()
        {
            blankSlotSprite = Display.sprite;
            qualityBar.gameObject.SetActive(false);
        }

        public void LoadItem(Item item)
        {
            Seeking = item;
            Display.sprite = item.GetComponent<SpriteRenderer>().sprite;
            qualityBar.Quality = item.Quality;
            qualityBar.FinishedMark.enabled = true;
            qualityBar.gameObject.SetActive(true);

        }

    } 
}
