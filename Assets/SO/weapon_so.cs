using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "weapon_so", menuName = "sequestered/weapon_so", order = 1)]
public class weapon_so : ScriptableObject 
{
    [SerializeField] string weapon_name;
    [SerializeField] GameObject bullet_prefab;
    [SerializeField] float fire_rate;

    public float get_fire_rate()
    {
        return fire_rate;
    }

    public string get_weapon_name()
    {
        return weapon_name;
    }

    public void fire(Vector3 position, Quaternion direction, string current_weapon)
    {  
        if (current_weapon == "pistol")
            Instantiate(bullet_prefab, position, direction);
            
        if (current_weapon == "shotgun")
            Instantiate(bullet_prefab, position, direction);  
    }
}
