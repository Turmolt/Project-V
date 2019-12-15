using UnityEngine;
using System.Collections;
using UnityEngine.UI;



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
    public float MinimumTier;

    public float Patience = 0.0f;
    public float MaxPatience = 180.0f;
    public Image PatienceImage;
    private int[] wants = new int[3];
    private Manager _manager;

    public bool Satisfied = false;
    
    public float ScoreMultiplier;

    private Transform CheckMark;
    
    private void Awake() {
        _manager = FindObjectOfType<Manager>();
        Patience = Random.Range(60,MaxPatience);
        PatienceImage.fillAmount = Patience/MaxPatience;
        StartCoroutine(ReducePatience());
        createNeed();
    }

    private IEnumerator ReducePatience()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            Patience-=1;
//            print(Patience);
//            print(Patience/MaxPatience);
            PatienceImage.fillAmount = Patience/MaxPatience;
            if(Patience<=0){
                NPCLeave();
                break;
            }
        }
    }

    private void NPCLeave(){
        //TODO: Make NPC Leave if Patience runs out OR if item is fulfilled
    }

    private void Start() {
        RequestCanvas = Instantiate(RequestCanvas, this.transform);
        CheckMark = RequestCanvas.transform.Find("Check Mark");
        Debug.Log(CheckMark.name);
        //RequestCanvas = GetComponentInChildren<Canvas>();
        RequestCanvas.enabled = false;
        requestObject = displayRequest();
        requestObject.tag = "Seeking";
        requestObject.layer = LayerMask.NameToLayer("Seeking");
        var item = requestObject.GetComponentInChildren<Item>();
        if (item.Quality == 0)
        {
            print("NoQual");
        }
        CheckMark.SetAsLastSibling();
        //displayRequest();
    }

    public float ScoreItem(Item receiving)
    {
        Debug.Log($"Receiving: {receiving.ItemState}, Type: {receiving.Type}, T: {receiving.Tier}, Q: {receiving.Quality}");
        Debug.Log($"Seeking: Type: {TypeWanted}, T: {MinimumTier}, Q: {MinimumTier}");
        var tierBasePoints = 5f;
        var baseQualityBonus = 5f;
        var baseTypePoints = 5f;

        if (receiving.ItemState != Item.State.Finished)
        {
            Debug.Log("Finish it first!");
            tierBasePoints *= .25f;
            baseQualityBonus *= .25f;
        }
        
        var score = 0f;
        
        if (TypeWanted != null&& TypeWanted != ItemType.nil && receiving.Type != TypeWanted)
        {
            Debug.Log("Type mismatch");
            return 0;
        }
        
        score += baseTypePoints;

        if (MaterialWanted != null)
        {
            if (receiving.Tier == MinimumTier)
            {
                Debug.Log("Tier =");
                score += tierBasePoints;
            }
            else if (receiving.Tier > MinimumTier)
            {
                Debug.Log("Tier >");
                score += 1.5f * tierBasePoints;
            }
            else
            {
                Debug.Log("Tier <");
                score += .5f * tierBasePoints;
            }
        }

        if(MinimumQuality != null)
            if (receiving.Quality == MinimumQuality)
            {
                Debug.Log("Meets quality");
                score += baseQualityBonus;
            }else if (receiving.Quality > MinimumQuality)
            {
                Debug.Log("Exceeds quality!");
                score += ((receiving.Quality - (float)MinimumQuality) * .25f) + 1f * baseQualityBonus;
            }
            else
            {
                Debug.Log("Doesnt meet quality");
                score += baseQualityBonus * (receiving.Quality/(float)MinimumQuality) * .5f;
            }

        Satisfied = true;

        return score;
    }

    private void createNeed()
    {
        Satisfied = false;
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
        MinimumQuality = (float)Random.Range(3,6);
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

    public GameObject displayRequest(){
        RequestCanvas.enabled = true;
        GameObject wantObject = new GameObject();

        Transform bubble = RequestCanvas.transform.Find("SpeechBubble");
            
        if(wants[0] == 1 && wants[1] == 1){
            //Mat and Type
            foreach(GameObject item in _manager.ItemList){
                Item temp = item.GetComponent<Item>();
                if(temp.Material == MaterialWanted && temp.Type == TypeWanted)
                {
                    MinimumTier = temp.Tier;
                    wantObject = Instantiate(item, bubble.position, Quaternion.identity, bubble.transform);
                }
            }
        } else if(wants[0] == 1){
            //Type Only
            foreach (GameObject item in _manager.TypeList){
                Item temp = item.GetComponent<Item>();
                if(temp.Type == TypeWanted){
                    MinimumTier = temp.Tier;
                    wantObject = Instantiate(item, bubble.position, Quaternion.identity, bubble.transform);
                }

            }

        } else if(wants[1] == 1){
            //Mats Only
            foreach (GameObject item in _manager.MaterialList){
                Item temp = item.GetComponent<Item>();
                if(temp.Material == MaterialWanted){
                    MinimumTier = temp.Tier;
                    wantObject = Instantiate(item, bubble.position, Quaternion.identity, bubble.transform);
                }

            }
        } else { 
                wantObject = Instantiate(_manager.AnyItem, bubble.position, Quaternion.identity, bubble.transform);
        }
        if(wants[2] == 1){
            
            Item temp = wantObject.GetComponent<Item>();
            MinimumTier = temp.Tier;
            print("Quality: " + MinimumQuality);
            temp.SetQuality((int)MinimumQuality);
                //temp.Quality = (float)MinimumQuality;
                //print("Quality: " + temp.GetQuality());
            
        } 
        // } else{
        //     if(wantObject){
                
        //         QualityBar qualityBar = wantObject.GetComponentInChildren<QualityBar>();
        //         qualityBar.gameObject.SetActive(false);
        //     }
        // }

        if(wantObject){
            wantObject.transform.localScale = new Vector3(0.5f,0.5f,0.5f);
        }        
        
        return wantObject;
    }
    
}
