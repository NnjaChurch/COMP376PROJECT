using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnWeaponFound(string weaponName)
    {
        // When a weapon is found, it will no longer spawn as loot and its Loot Table drop chance is replaced by materials
    }

    public void OnArmourFound(string armourName)
    {
        // When armour is found, it will no longer spawn as loot and its Loot Table drop chance is replaced by materials
    }


    public void SaveInventory()
    {

    }
    public void LoadInventory()
    {

    }


}
