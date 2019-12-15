using UnityEngine;


public class Item: MonoBehaviour   {
    
    private Sprite _image;

    public Canvas QualityBarPrefab;

    private QualityBar qualityBar;
    
    public Sprite Image{
        set{
            _image = value;
            SpriteRenderer rend = GetComponent<SpriteRenderer>();
            if(rend.sprite == null) rend.sprite = value;
        }
        get{
            return _image;
        }
    }
    
    public ItemType Type;
    public ItemMaterial Material;

    public int Rarity;

    [Range(0,100)][HideInInspector] public float Quality;

    public void SetQuality(float quality)
    {
        Quality = quality;
        if (qualityBar != null) qualityBar.Quality = Quality;
    }

    public float GetQuality()
    {
        Debug.Log($"{Type.ToString()}  {Material.ToString()}");
        return Quality;
    }
    
    private void Start()
    {
        Canvas canvas;
        if(qualityBar == null){
            canvas = Instantiate(QualityBarPrefab,this.transform);
            qualityBar = canvas.GetComponent<QualityBar>();
        } else{
            canvas = qualityBar.GetComponent<Canvas>();
        }
        Quality = qualityBar.Quality;
        canvas.overrideSorting = true;
        canvas.sortingOrder = 100;
        SpriteRenderer rend = GetComponent<SpriteRenderer>();
        if(rend.sprite == null) rend.sprite = Image;
        else Image = rend.sprite;
    }

    public Item(ItemType type, ItemMaterial material, float quality, Sprite image = null){
        Type = type;
        Material = material;
        Quality = quality;
        Image = image; 
    }
}
