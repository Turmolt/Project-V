using UnityEngine;


public class NPC: MonoBehaviour   {
    

    //NPC Wants are based on what they DON'T ALREADY HAVE.
    //Randomly determine what Gear each monster already has (always missing at least 1)
    //
    public Sprite Image;

    public Canvas RequestCanvas;

    private GameObject requestObject;
    
    public ItemType? TypeWanted = null;
    public ItemMaterial? MaterialWanted = null;
    public float? MinimumQuality = null;

    private int[] wants = new int[3];
    private Manager _manager;
    
    
    public float ScoreMultiplier;
    
    private void Awake() {
        _manager = FindObjectOfType<Manager>();
        createNeed();
    }
    private void Start() {
        RequestCanvas = Instantiate(RequestCanvas, this.transform);
        //RequestCanvas = GetComponentInChildren<Canvas>();
        RequestCanvas.enabled = false;
        
        
        displayRequest();
    }
    private void createNeed(){
        TypeWanted = null;
        MaterialWanted = null;
        MinimumQuality = null;
        wants = new int[]{0,0,0};
        float firstNeed = Mathf.RoundToInt(Random.Range(0,3));
        switch (firstNeed){
            case 0:
            wants[0] = 1;
            break;
            case 1:
            wants[1] = 1;
            break;
            case 2:
            wants[2] = 1;
            break;
            default: break;
        }
        float secondNeed = Mathf.RoundToInt(Random.Range(0,6));
        switch (secondNeed){
            case 0:
            if(firstNeed == 0 || firstNeed == 1) wants[2] = 1;
                else wants[0] = 1;
            break;

            case 1:
            if(firstNeed == 0 || firstNeed == 2)wants[1] = 1;
            else wants[0] = 1;
            break;

            default: break;
        }
        if(secondNeed<=1) {
            float thirdNeed = Mathf.RoundToInt(Random.Range(0,9));
            switch(thirdNeed){
                case 0:
                if(wants[0] == 0) wants[0] = 1;
                else if(wants[1] == 0)wants[1] = 1;
                else wants[2] = 1;
                break;

                default: break;
            }

        }

        if(wants[0] == 1){
            randomType();
        }
        if(wants[1] == 1){
            randomMaterial();
        } 
        if(wants[2] == 1){
            randomQuality();
        }

        print(TypeWanted + ", " + MaterialWanted +", " + MinimumQuality);
    }

    private void randomQuality(){
        MinimumQuality = Random.Range(0.5f,1.0f);
    }
    private void randomType(){
        TypeWanted = (ItemType)Random.Range(2,_manager.NumberOfItemTypes); 
    }
    private void randomMaterial(){
        if(TypeWanted == null){
            MaterialWanted = (ItemMaterial)Random.Range(2,_manager.NumberOfMaterials);
        } else{
            switch (TypeWanted){
                case ItemType.Axe:
                    MaterialWanted = (ItemMaterial)Random.Range(2,_manager.NumberOfMaterials-2);
                    break;
                case ItemType.Dagger:
                    MaterialWanted = (ItemMaterial)Random.Range(2,_manager.NumberOfMaterials-2);
                    break;
                case ItemType.Mace:
                    MaterialWanted = (ItemMaterial)Random.Range(2,_manager.NumberOfMaterials-2);
                    break;
                case ItemType.Spear:
                    MaterialWanted = (ItemMaterial)Random.Range(2,_manager.NumberOfMaterials-2);
                    break;
                case ItemType.Sword:
                    MaterialWanted = (ItemMaterial)Random.Range(2,_manager.NumberOfMaterials-2);
                    break;
                case ItemType.HeavyArmor:
                    MaterialWanted = (ItemMaterial)Random.Range(2,_manager.NumberOfMaterials-2);
                    break;
                case ItemType.Shield:
                    MaterialWanted = (ItemMaterial)Random.Range(3,_manager.NumberOfMaterials-1);
                    break;
                case ItemType.Bow:
                     MaterialWanted = (ItemMaterial)Random.Range(3,_manager.NumberOfMaterials-1);
                    break;
                case ItemType.Wand:
                    MaterialWanted = (ItemMaterial)Random.Range(4,_manager.NumberOfMaterials);
                    break;
                case ItemType.Staff:
                    MaterialWanted = (ItemMaterial)Random.Range(4,_manager.NumberOfMaterials);
                    break;
                //I've adjusted the Robe and Leather Armor to ONLY be requested at Tiers 3 and 4 due to
                //Difficulty finding "Material" solo sprite for Light and Tough
                case ItemType.Robe:
                    int temp = Random.Range(4,6);
                    //if (temp>2) temp +=2; 
                    MaterialWanted = (ItemMaterial)(temp);
                    break;
                case ItemType.LeatherArmor:
                    int temp2 = Random.Range(4,6);
                    //if (temp2>2) temp2 +=2; 
                    MaterialWanted = (ItemMaterial)(temp2);
                    break;

                default: break;
            }
        }
        
    }
      
    public NPC(ItemType type, ItemMaterial material, float scoreMultiplier, Sprite image = null){
        TypeWanted = type;
        MaterialWanted = material;
        ScoreMultiplier = scoreMultiplier;
        Image = image; 
    }

    public void displayRequest(){
        RequestCanvas.enabled = true;
        Transform bubble = RequestCanvas.transform.Find("SpeechBubble");
        if (requestObject == null){
            if(wants[0] == 1 && wants[1] == 1){
                //Mat and Type
                foreach(GameObject item in _manager.ItemList){
                    Item temp = item.GetComponent<Item>();
                    if(temp.Material == MaterialWanted && temp.Type == TypeWanted){
                        requestObject = Instantiate(item, bubble.position, Quaternion.identity, bubble.transform);
                        
                        //Item temp2 = obj.GetComponent<Item>();
                        // if(MinimumQuality != null){
                        //     temp2.Quality = (float)MinimumQuality;
                        // } else{
                        //     Canvas qualityCan = obj.GetComponentInChildren<Canvas>();
                        //     qualityCan.enabled = false;
                        // }
                        
                        
                        //break;
                    }
                }
            } else if(wants[0] == 1){
                //Type Only
                foreach (GameObject item in _manager.TypeList){
                    Item temp = item.GetComponent<Item>();
                    if(temp.Type == TypeWanted){
                        requestObject = Instantiate(item, bubble.position, Quaternion.identity, bubble.transform);
                    }

                }

            } else if(wants[1] == 1){
                //Mats Only
                foreach (GameObject item in _manager.MaterialList){
                    Item temp = item.GetComponent<Item>();
                    if(temp.Material == MaterialWanted){
                        requestObject = Instantiate(item, bubble.position, Quaternion.identity, bubble.transform);
                    }

                }

            }
            if(wants[2] == 1){
                if(requestObject){
                    Item temp = requestObject.GetComponent<Item>();
                    temp.SetQuality((float)MinimumQuality);
                    //temp.Quality = (float)MinimumQuality;
                } else{

                }
                
            } else{
                if(requestObject){
                    //QualityBar qualityBar = requestObject.GetComponentInChildren<QualityBar>();
                    //qualityBar.gameObject.SetActive(false);
                }
            }
            if(requestObject){
                requestObject.transform.localScale = new Vector3(0.5f,0.5f,0.5f);
            }
            
        }

        //TODO: Material only items are spawning with incorrect Quality being shown on bar. Need to fix
        
        
        
    }
    
}
