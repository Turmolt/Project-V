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
	Bow = 3,
	Staff = 4,
	Mace = 5,
	Spear = 6,
	Wand = 14,
	//Armor
	Heavy = 7,
	Light = 8,
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
	Wood = 0,
	Cloth = 1,
	Iron = 2,
	Leather = 3,
	Bone = 4,
	Bronze = 5,
	DarkSteel = 6,
	Gold = 7
	
	
	
}