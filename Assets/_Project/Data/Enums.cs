using UnityEngine;
using System.Collections;


public enum ItemType{
	/*
	//If we use item subtypes
	Weapon,
	Armor,
	Accessory
	*/

	//Weapons
	Sword = 0,
	Dagger = 1,
	Axe = 2,
	Mace = 5,
	Spear = 6,
	Bow = 3,
	Staff = 4,
	Wand = 14,
	//Armor
	HeavyArmor = 7,
	LightArmor = 8,
	Robe = 9,
	Shield = 10,
	//Accessories
	Ring = 11,
	Necklace = 12,
	Boots = 13

}
//If we use item subtypes:
/*
public enum WeaponType{
	Sword,
	Bow,
	Staff,
	Mace,
	TwoHandedSword,

}

public enum ArmorType{
	Heavy,
	Light,
	Robe,
	Shield
}

public enum AccessoryType{
	Ring,
	Necklace,
	Boots
}
*/

public enum ItemMaterial{
	
	Cloth = 0,
	Leather = 1,
	Iron = 2,
	Bronze = 3,
	DarkSteel = 4,
	Gold = 5,
	Wood = 6,
	Bone = 7,
	
	
	
	
}