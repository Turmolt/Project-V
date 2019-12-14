using UnityEngine;


public class NPC: MonoBehaviour   {
    

    //NPC Wants are based on what they DON'T ALREADY HAVE.
    //Randomly determine what Gear each monster already has (always missing at least 1)
    //
    public Sprite Image;
    
    public ItemType? TypeWanted = null;
    public ItemMaterial? MaterialWanted = null;
    public float? MinimumQuality = null;

    private int[] wants = new int[3];
    private Manager _manager;
    
    
    public float ScoreMultiplier;
    
    private void Awake() {
        _manager = FindObjectOfType<Manager>();
        createNeed();
        createNeed();
        createNeed();
        createNeed();
        createNeed();
        createNeed();
        
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
        TypeWanted = (ItemType)Random.Range(0,_manager.NumberOfItemTypes); 
    }
    private void randomMaterial(){
        if(TypeWanted == null){
            MaterialWanted = (ItemMaterial)Random.Range(0,_manager.NumberOfMaterials);
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
                case ItemType.Shield:
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
                case ItemType.Bow:
                     MaterialWanted = (ItemMaterial)Random.Range(3,_manager.NumberOfMaterials-1);
                    break;
                case ItemType.Wand:
                    MaterialWanted = (ItemMaterial)Random.Range(4,_manager.NumberOfMaterials);
                    break;
                case ItemType.Staff:
                    MaterialWanted = (ItemMaterial)Random.Range(4,_manager.NumberOfMaterials);
                    break;
                case ItemType.Robe:
                    int temp = Random.Range(0,4);
                    if (temp>2) temp +=2; 
                    MaterialWanted = (ItemMaterial)(temp);
                    break;
                case ItemType.LeatherArmor:
                    int temp2 = Random.Range(0,4);
                    if (temp2>2) temp2 +=2; 
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

    
}
