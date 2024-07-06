using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{
    [SerializeField]
    private int hp;              

    [SerializeField]
    private float destroyTime;     
    [SerializeField]
    private SphereCollider col;     

    [SerializeField]
    private GameObject go_Rock;     
    [SerializeField]
    private GameObject go_Debris;  

    [SerializeField]
    private GameObject go_EffectPrefab;
    [SerializeField]
    private GameObject go_rock_item_prefab;

    [SerializeField]
    private int count;

    [SerializeField]
    private string strikeSound;
    [SerializeField]
    private string destroySound;
    

    public void Mining()
    {
        SoundManager.instance.PlaySE(strikeSound);
        var clone = Instantiate(go_EffectPrefab, col.bounds.center, Quaternion.identity);
        Destroy(clone, destroyTime);

        hp--;
        if (hp <= 0)
        {
            Destruction();
        }
    }

    private void Destruction()
    {
        SoundManager.instance.PlaySE(destroySound);
        col.enabled = false;
        for (int i = 0; i <= count; i++)
        {
            Instantiate(go_rock_item_prefab, go_Rock.transform.position, Quaternion.identity);
        }
        Destroy(go_Rock);

        go_Debris.SetActive(true);
        Destroy(go_Debris, destroyTime);
    }
}