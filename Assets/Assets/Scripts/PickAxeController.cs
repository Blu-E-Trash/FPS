using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickAxeController : CloseWeaponController
{
    public static bool isActive = false;

    private void Start()
    {
        WeaponManager.currentWeapon = currentCloseWeapon.GetComponent<Transform>();
        WeaponManager.currentWeaponAnim = currentCloseWeapon.anim;
    }
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
                if(hitInfo.transform.tag == "Rock") {
                    hitInfo.transform.GetComponent<Rock>().Mining();        
                }
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
