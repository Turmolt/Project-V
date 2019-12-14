using UnityEngine;


public class Item: MonoBehaviour   {
    
    private Sprite _image;

    public Canvas QualityBar;
    
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
    [Range(0,100)]
    private float _quality;
    public float Quality{
        get{
            return _quality;
        }
        set{
            _quality = value;
            QualityBar.GetComponent<QualityBar>().Quality = _quality;
        }

    }

    private void Awake() {
        Instantiate(QualityBar,this.transform);
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
