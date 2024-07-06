using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionController : MonoBehaviour
{
    [SerializeField]
    private float range;

    private bool pickupActivated = false;

    private RaycastHit hitInfo;
    [SerializeField]
    private LayerMask layerMask;

    //ÇÊÄÄ
    [SerializeField]
    private Text acntionText;
    [SerializeField]
    private Inventory theInventory;

    void Update()
    {
        TryAction();
        CheckItem();
    }
    private void TryAction()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            CheckItem();
            CanPickUp();
        }

    }
    private void CanPickUp()
    {
        if (pickupActivated)
        {
            if(hitInfo.transform != null)
            {
                Debug.Log(hitInfo.transform.GetComponent<ItemPickUp>().item.itemName + "È¹µæÇÔ");
                theInventory.AcquireItem(hitInfo.transform.GetComponent<ItemPickUp>().item);
                Destroy(hitInfo.transform.gameObject);
                InfoDisappear();
            }
        }
    }
    private void CheckItem()
    {
        if (Physics.Raycast(transform.position, transform.forward, out hitInfo, range, layerMask))
        {
            if (hitInfo.transform.tag == "Item")
            {
                ItemInfoAppear();
            }
        }
        else
            InfoDisappear();
    }
    private void ItemInfoAppear()
    {
        pickupActivated = true;
        acntionText.gameObject.SetActive(true);
        acntionText.text = hitInfo.transform.GetComponent<ItemPickUp>().item.itemName+"È¹µæ"+"<color=yellow>"+"(E)"+"</color>";
    }
    private void InfoDisappear()
    {
        pickupActivated = false;
        acntionText.gameObject.SetActive(false);
        
    }
}
