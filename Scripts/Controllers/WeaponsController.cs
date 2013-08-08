using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System.Collections;

public class WeaponsController : MonoBehaviour
{
    protected Weapon[] _weaponry;
    public Dictionary<string, List<Weapon>> WeaponsByName;
    public Transform Target { get; set; }
    public Dictionary<string, List<Weapon>> SelectedWeapons;

    private Rect WeaponWindowPosition;
    //public List<string> SelectedWeapons;

	// Use this for initialization
	void Start ()
	{
		Debug.Log(gameObject);
        WeaponsByName = new Dictionary<string, List<Weapon>>();
        SelectedWeapons = new Dictionary<string, List<Weapon>>();

	    _weaponry = GetComponentsInChildren<Weapon>();
	    foreach (Weapon weapon in _weaponry)
	    {
	        weapon.Controller = this;
	        AddByName(weapon);
	    }
        WeaponWindowPosition = new Rect(Screen.width - 310, Screen.height - 10 - WeaponsByName.Count * 50, 300, WeaponsByName.Count * 50);
	}

    private void AddByName(Weapon weapon)
    {
        if(!WeaponsByName.ContainsKey(weapon.WeaponName))
        {
            WeaponsByName.Add(weapon.WeaponName, new List<Weapon>());
        }
        WeaponsByName[weapon.WeaponName].Add(weapon);
    }
	
	// Update is called once per frame
	void Update ()
    {
	}

    public void AimPoint(Vector3 target)
    {
        foreach (Weapon weapon in _weaponry)
        {
            weapon.AimTarget(target);
        }
    }

    public void LockOn(Transform target)
    {
        Target = target;
    }

    private void OnTargetDestroied(object s, EventArgs e)
    {
        Target = null;
    }

    public void Fire(Transform target, String[] weaponsToFire = null)
    {
        Target = target;
        if(weaponsToFire!=null)
        {
            SetSelectedWeaponTypes(weaponsToFire);
        }
        if(SelectedWeapons!=null)
        {
            foreach (Weapon weapon in _weaponry)
            {
                if (SelectedWeapons.ContainsKey(weapon.WeaponName)) weapon.Fire();
            }
        }
        
    }

    private bool WeaponIsOnTheList(Weapon weapon, string[] weaponsToFire)
    {
        foreach (string s in weaponsToFire)
        {
            if (weapon.WeaponName.ToLower() == s.ToLower()) return true;
        }
        return false;
    }
    
    public void GUI()
    {
        //List<string> weaponTypes = new List<string>();
        WeaponWindowPosition = UnityEngine.GUI.Window(0, WeaponWindowPosition, DrawWeaponSelection, "Weapons");

    }

    protected void DrawWeaponSelection(int windowID)
    {
        int left = Screen.width - 10;
        int top = Screen.height - 10;
        int width = 250;
        int height = 20;

        int padding = 5;
        int index = 1;

        foreach (KeyValuePair<string, List<Weapon>> keyValuePair in WeaponsByName)
        {
            bool selectedState = SelectedWeapons.ContainsKey(keyValuePair.Key);
            //var flag = UnityEngine.GUI.Toggle(new Rect(left - width, top - height * index - padding * index, width, height), selectedState, keyValuePair.Key);
			var flag = UnityEngine.GUI.Toggle(new Rect(padding, (height+padding)*index, width, height), selectedState, keyValuePair.Key);
			if (flag)
            {
                if (!selectedState)
                {
                    SelectedWeapons.Add(keyValuePair.Key, keyValuePair.Value);
                }
            }
            else
            {
                if (selectedState)
                {
                    SelectedWeapons.Remove(keyValuePair.Key);
                }
            }

            index++;
        }
    }

    protected void SetSelectedWeaponTypes(string[] weaponTypes)
    {
        SelectedWeapons.Clear();
        foreach (string weaponType in weaponTypes)
        {
            SelectedWeapons.Add(weaponType, WeaponsByName[weaponType]);
        }
    }

    //protected void SelectWeaponType(string weaponType)
    //{
    //    foreach (string selectedWeapon in SelectedWeapons)
    //    {
    //        if (selectedWeapon == weaponType)
    //            return;
    //    }
    //    SelectedWeapons.Add(weaponType);
    //}

    //protected void DeselectWeaponType(string weaponType)
    //{
    //    for (int i = 0; i < SelectedWeapons.Count; i++)
    //    {
    //        if (SelectedWeapons[i] == weaponType)
    //            SelectedWeapons.RemoveAt(i);
    //    }
    //}

    //protected void TogleWeaponTypeSelection(string weaponType)
    //{
    //    for (int i = 0; i < SelectedWeapons.Count; i++)
    //    {
    //        if (SelectedWeapons[i] == weaponType)
    //        {
    //            SelectedWeapons.RemoveAt(i);
    //            return;
    //        }
    //    }
    //    SelectedWeapons.Add(weaponType);
    //}
}
