using UnityEngine;


[CreateAssetMenu(menuName = "Item")]
public class Item: ScriptableObject   {
    
    [SerializeField] private string _name;
    public Sprite Icon;
    public string ItemName{
        get{
            return _name;
        }
        set {
            _name = value;
            this.name = value;
        }
    }
    
    public ItemType Type;
    public ItemMaterial Material;
    public float Quality;
    
    
    public Item(ItemType type, ItemMaterial material, float quality, Sprite icon = null){
        Type = type;
        Material = material;
        Quality = quality;
        Icon = icon; 
    }
}
