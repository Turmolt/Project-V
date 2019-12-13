using UnityEngine;


[CreateAssetMenu(menuName = "NPC")]
public class NPC: MonoBehaviour   {
    
    [SerializeField] private string _name;
    public Sprite Image;
    public string NPCName{
        get{
            return _name;
        }
        set {
            _name = value;
            this.name = value;
        }
    }
    
    public ItemType TypeWanted;
    public ItemMaterial MaterialWanted;
    public float ScoreMultiplier;
    
    
    
    public NPC(ItemType type, ItemMaterial material, float scoreMultiplier, Sprite image = null){
        TypeWanted = type;
        MaterialWanted = material;
        ScoreMultiplier = scoreMultiplier;
        Image = image; 
    }
}
