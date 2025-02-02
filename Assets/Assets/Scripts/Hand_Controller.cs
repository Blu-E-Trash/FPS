using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand_Controller : CloseWeaponController
{
    public static bool isActive = false;
    void Update()
    {
        if (isActive)
        {
            TryAttack();
        }
    }
    protected override IEnumerator HitCoroutine()
    {
        while (isSwing)
        {
            if (CheckObjeckt())
            {
                isSwing = false;
                Debug.Log(hitInfo.transform.name);
            }
            yield return null;
        }
    }
    public override void CloseWeaponChange(CloseWeapon _closeWeapon)
    {
        base.CloseWeaponChange(_closeWeapon);
        isActive = true;
    }
}
