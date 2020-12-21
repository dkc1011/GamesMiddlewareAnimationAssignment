using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelect : MonoBehaviour
{
    static bool knightSelect;
    static bool paladinSelect;


    void Start()
    {
        
    }
    
    public bool getKnightSelect()
    {
        return knightSelect;
    }

    public bool getPaladinSelect()
    {
        return paladinSelect;
    }

    public void ToggleKnight()
    {
        knightSelect = true;
        paladinSelect = false;
    }

    public void TogglePaladin()
    {
        knightSelect = false;
        paladinSelect = true;
    }
}
