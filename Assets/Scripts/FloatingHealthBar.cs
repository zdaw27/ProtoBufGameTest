using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatingHealthBar : MonoBehaviour
{
    [SerializeField]
    private Image hpBar;
    
    public Transform target { get; set; }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            FloatingHealthBarManager.Instance.RemoveHealthBar(this);
            return;
        }

        transform.position = target.position;
        transform.rotation = Camera.main.transform.rotation;
    }

    public void UpdateHpBar(float percent)
    {
        if (target == null)
        {
            FloatingHealthBarManager.Instance.RemoveHealthBar(this);
            return;
        }

        hpBar.fillAmount = percent;
    }
}
