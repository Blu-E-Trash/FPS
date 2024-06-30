using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    //�����ڿ�, Ŭ���� ���� = ��������
    //���� �ߺ� ��ü ���� ����
    public static bool isChangeWeapon = false;

    //���� ��ü ������, ���� ��ü�� ������ ���� ����
    [SerializeField]
    private float changeWeaponDelayTime;
    [SerializeField]
    private float changeWeaponEndDelayTime;

    //���� ������ ���� ����
    [SerializeField]
    private Gun[] guns;
    [SerializeField]
    private CloseWeapon[] hands;
    [SerializeField]
    private CloseWeapon[] axes;
    [SerializeField]
    private CloseWeapon[] pickaxes;

    private Dictionary<string,Gun> gunDictionary = new Dictionary<string,Gun>();
    private Dictionary<string, CloseWeapon> handDictionary = new Dictionary<string,CloseWeapon>();
    private Dictionary<string, CloseWeapon> axeDictionary = new Dictionary<string, CloseWeapon>();
    private Dictionary<string, CloseWeapon> pickaxeDictionary = new Dictionary<string, CloseWeapon>();

    //���� ������ Ÿ��
    [SerializeField]
    private string currentWeaponType;

    //���� ����� ���� ������ �ִϸ��̼�
    public static Transform currentWeapon;
    public static Animator currentWeaponAnim;


    //����
    [SerializeField]
    private Gun_Controller theGunController;
    [SerializeField]
    private Hand_Controller theHandController;
    [SerializeField]
    private AxeController theAxeController;
    [SerializeField]
    private PickAxeController thePickAxeController;

    void Start()
    {
        for (int i = 0; i < guns.Length; i++)
        {
            gunDictionary.Add(guns[i].gunName, guns[i]);
        }
        for(int i = 0;i < hands.Length; i++)
        {
            handDictionary.Add(hands[i].closeWeaponName, hands[i]);
        }
        for (int i = 0; i < axes.Length; i++)
        {
            axeDictionary.Add(axes[i].closeWeaponName, axes[i]);
        }
        for (int i = 0; i < pickaxes.Length; i++)
        {
            pickaxeDictionary.Add(pickaxes[i].closeWeaponName, pickaxes[i]);
        }
    }
    void Update()
    {
        if (!isChangeWeapon)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                StartCoroutine(ChangeWeaponCoroutione("HAND", "�Ǽ�"));
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                StartCoroutine(ChangeWeaponCoroutione("GUN", "SubMachineGun1"));
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                StartCoroutine(ChangeWeaponCoroutione("AXE", "Axe"));
            }
            else if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                StartCoroutine(ChangeWeaponCoroutione("PICKAXE", "PickAxe"));
            }
        }
    }
    public IEnumerator ChangeWeaponCoroutione(string _type,string _name)
    {
        isChangeWeapon = true;
        currentWeaponAnim.SetTrigger("Weapon_out");

        yield return new WaitForSeconds(changeWeaponDelayTime);

        CancelPreWeaponAction();
        WeaponChange(_type,_name);

        yield return new WaitForSeconds(changeWeaponEndDelayTime);

        currentWeaponType = _type;
        isChangeWeapon =false;
    }

    private void CancelPreWeaponAction()
    {
        switch(currentWeaponType) {
            case "GUN":
                theGunController.CancelFineSight();
                theGunController.CancelReload();
                Gun_Controller.isActive = false;
                break;
            case "HAND":
                Hand_Controller.isActive = false;
                break;
            case "AXE":
                AxeController.isActive = false;
                break;
            case "PICKAXE":
                AxeController.isActive = false;
                break;
        }
    }
    private void WeaponChange(string _type, string _name)
    {
        if (_type == "GUN")
        {
            theGunController.GunChange(gunDictionary[_name]);
        }
        else if (_type == "HAND")
        {
            theHandController.CloseWeaponChange(handDictionary[_name]);
        }
        else if (_type == "AXE")
        {
            theAxeController.CloseWeaponChange(axeDictionary[_name]);
        }
        else if (_type == "PICKAXE")
        {
            thePickAxeController.CloseWeaponChange(pickaxeDictionary[_name]);
        }
    }
}
