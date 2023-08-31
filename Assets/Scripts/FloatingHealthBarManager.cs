using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingHealthBarManager : MonoSingleton<FloatingHealthBarManager>
{
    private Transform canvas;

    // Start is called before the first frame update
   
    public FloatingHealthBar CreateHealthBar(Transform target)
    {
        if(canvas == null)
        {
            canvas = GameObject.Find("FloatingHealthBarCanvas").transform;
        }
        var healthBar = GameObject.Instantiate(Resources.Load<GameObject>("Prefabs/FloatingHealthBar"), canvas).GetComponent<FloatingHealthBar>();

        healthBar.target = target;
        healthBar.UpdateHpBar(1f);
        return healthBar;
    }
}
