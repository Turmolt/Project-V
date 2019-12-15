using UnityEngine;
using UnityEngine.UI;


public class Item: MonoBehaviour   {
    
    private Sprite _image;

    public Canvas QualityBarPrefab;

    private QualityBar qualityBar;

    public enum State { Normal, Heated, Hammered, Finished}

    public int Tier;

    public State ItemState= State.Normal;

    public int SwingsLeft = 3;

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

    [HideInInspector] public float Quality;

    private Image finishedCheck;

    public void SetFinished()
    {
        ItemState = State.Finished;
        finishedCheck.enabled = true;
    }

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

        qualityBar.Quality = Quality;
        finishedCheck = qualityBar.FinishedMark;
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
