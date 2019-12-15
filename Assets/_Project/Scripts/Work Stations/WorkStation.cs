using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkStation : MonoBehaviour
{
    public Item InMachine;
    public Transform ItemSpace;
    private BoxCollider2D loadedBoxCollider2D;
    protected SpriteRenderer loadedSpriteRenderer;

    public virtual void LoadItem(Item item)
    {
        if (InMachine != null)
        {
            Debug.Log("[WorkStation]: Loading too many items");
            return;
        }
        Debug.Log($"[WorkStation]: Loading {item.name}");
        item.transform.position = ItemSpace.position;
        InMachine = item;
        loadedBoxCollider2D = item.gameObject.GetComponent<BoxCollider2D>();
        loadedBoxCollider2D.enabled = false;
        loadedSpriteRenderer = item.gameObject.GetComponent<SpriteRenderer>();
    }

    public virtual Item PopItem()
    {
        if (InMachine != null)
        {
            loadedBoxCollider2D.enabled = true;
            loadedBoxCollider2D = null;
            var item = InMachine;
            InMachine = null;
            Debug.Log($"[WorkStation]: Popping item {item.name}");
            return item;
        }
        return null;
    }
}
