using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Manager: MonoBehaviour
{
    public  GameObject[] ItemList;
    private int totalItemWeight;
    public  int NumberOfMaterials = 8;
    public  int NumberOfItemTypes = 14;
    public  float minQuality = 1;
    public  float maxQaulity = 80;
    public  float chanceToSpawn = 0.2f;
    //public  float populationWeight = 0.1f;


    private void Awake(){
        foreach (GameObject g in ItemList){
            totalItemWeight +=g.GetComponent<Item>().Rarity;
        }
    }


    public  GameObject[] PopulateItems(GameObject parent, Tilemap floorMap){
        foreach(Vector3Int pos in floorMap.cellBounds.allPositionsWithin){
           if(floorMap.GetTile(pos)){

                if(Random.Range(0.0f,1.0f)<chanceToSpawn){
                    Instantiate(randomItem(),pos + new Vector3(0.5f,0.5f),Quaternion.identity,parent.transform);
                }
           }
        }
        return new GameObject[0];
    }

    private  GameObject randomItem(){

        int randomWeight = Random.Range(0, totalItemWeight);
        int currentWeight = 0;

        foreach (GameObject g in ItemList)
        {
            Item i = g.GetComponent<Item>();
            
            currentWeight += i.Rarity;
            if (randomWeight <= currentWeight) {
                print(g.name);
                return g; // selected one
            }
        }
        return new GameObject();
    }


}
    // Start is called before the first frame update