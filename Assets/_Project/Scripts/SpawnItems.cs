using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System;

public class SpawnItems : MonoBehaviour
{
    public Tilemap floormap;

    private Manager manager;
    // Start is called before the first frame update
    void Start()
    {
        manager = FindObjectOfType<Manager>();
        manager.PopulateItems(this.gameObject,floormap);
        //Array.Sort(manager.ItemList);
    }
}
