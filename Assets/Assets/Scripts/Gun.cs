using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public string gunName;          //총 이름
    public float range;             //사거리
    public float accuracy;          //정확도
    public float fireRate;          //연사속도
    public float reloadTime;        //재장전 속도
    public int damage;              //딜
    public int reloadBulletCount;   //총알 재장전 개수
    public int currentBulletCount;  //지금 남은 총알
    public int maxBulletCount;      //최대 소유 가능 총알
    public int carryBulletCount;    //현재 소유한 총알
    public float retroActionForce;  //반동 세기
    public float retroActionSineSightForce;//정조준시의 반동 세기

    public Vector3 fineSightOriginPos;
    public Animator anim;
    public ParticleSystem muzzleFlash;
    public AudioClip fire_Sound;
}
