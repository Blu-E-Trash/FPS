using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseWeapon : MonoBehaviour
{
    public string closeWeaponName;     //맨손이나 무기 구분

    public bool isHand;
    public bool isAxe;
    public bool isPickaxe;

    public float range;         //범위
    public int damage;          //딜
    public float workSpeed;     //작업 속도
    public float attackDelay;   //공격 딜레이
    public float attackDelayA;  //공격 활성화 시점
    public float attackDelayB;  //공격 비활성화 시점

    public Animator anim;//애니매이션
    
}
