using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static bool canPlayerMove = true;

    public static bool isOpenInventory = false;
    public static bool isOpenCraftManual = false;

    public static bool isNight = false;
    public static bool isWater = false;

    private WeaponManager theweaponManager;
    private bool flag = false;
    void Update()
    {
        if (isOpenInventory || isOpenCraftManual)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            canPlayerMove = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            canPlayerMove = true;
        }
        if (isWater)
        {
            if (!flag)
            {
                StopAllCoroutines();
                StartCoroutine(theweaponManager.WeaponInCoroutine());
                flag = true;
            }
        }
        else
        {
            if (flag)
            {   
                flag = false;
                theweaponManager.WeaponOut();
            }
        }
    }
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        theweaponManager = FindObjectOfType<WeaponManager>();
    }
}
