using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Tilemaps;

public class Manager: MonoBehaviour
{
    public  GameObject[] ItemList;
    public GameObject[] MaterialList;
    public GameObject[] TypeList;
    private int[] ItemRarity = new int[0];
    private int totalItemWeight;
    public  int NumberOfMaterials = 8;
    public  int NumberOfItemTypes = 11;
    public  float minQuality = 1;
    public  float maxQaulity = 80;
    public  float chanceToSpawn = 0.2f;
    //public  float populationWeight = 0.1f;


    private void Awake(){
        //foreach (GameObject g in ItemList){
        //    totalItemWeight +=g.GetComponent<Item>().Rarity;
        //}
        ItemRarity = new int[ItemList.Length];
        for (int i = 0; i < ItemList.Length; i++)
        {
            ItemRarity[i] = ItemList[i].GetComponent<Item>().Rarity;
            totalItemWeight += ItemRarity[i];
        }
        /*
        //NOT SURE IF SUPPOSED TO SORT HIGH>LOW or LOW>HIGH
        print("Before");
        foreach (GameObject g in ItemList){
            print(g.name);
        }
        Array.Sort(ItemRarity,ItemList);
        print ("After");
        foreach (GameObject g in ItemList){
            print(g.name);
        }
        */
        
    }


    public  GameObject[] PopulateItems(GameObject parent, Tilemap floorMap){
        foreach(Vector3Int pos in floorMap.cellBounds.allPositionsWithin){
           if(floorMap.GetTile(pos)){

                if(UnityEngine.Random.Range(0.0f,1.0f)<chanceToSpawn){
                    Instantiate(randomItem(),pos + new Vector3(0.5f,0.5f),Quaternion.identity,parent.transform);
                }
           }
        }
        return new GameObject[0];
    }

    private  GameObject randomItem(){

        int randomWeight = UnityEngine.Random.Range(0, totalItemWeight);
        int currentWeight = 0;

        foreach (GameObject g in ItemList)
        {
            Item i = g.GetComponent<Item>();
            
            currentWeight += i.Rarity;
            if (randomWeight <= currentWeight) {
                i.SetQuality(UnityEngine.Random.Range(minQuality,maxQaulity));
                //print(g.name);
                return g; // selected one
            }
        }
        return new GameObject();
    }


}
    // Start is called before the first frame update