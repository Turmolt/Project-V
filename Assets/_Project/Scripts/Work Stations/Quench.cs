using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BackwardsCap
{
    public class Quench : WorkStation
    {
        public Inventory Inventory;
        public ParticleSystem ParticleSystem;

        public AudioClip QuenchSound;
        public SoundManager SoundManager;

        public override void LoadItem(Item item)
        {
            if (item.ItemState == Item.State.Heated || item.ItemState == Item.State.Hammered)
            {
                SoundManager.ClipArrayVariation(QuenchSound);
                ParticleSystem.Play();
                item.SetFinished();
            }
            item.GetComponent<SpriteRenderer>().color = Color.white;
            Inventory.UpdateInventoryOrder();
            Inventory.PickupItem(item);
        }
    } 
}
