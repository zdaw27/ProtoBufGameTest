using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientObject : MonoBehaviour
{
    private const float characterMoveSpeed = 1f;

    [SerializeField]
    private Animator animator;
    [SerializeField]
    private Transform transform;
    

    private FloatingHealthBar healthBar;

   

    public void Move(float x, float y)
    {
        animator.CrossFadeInFixedTime("move", 0.1f);
    }

    public void StartMove(float x, float y)
    {
        animator.CrossFadeInFixedTime("move", 0.1f);
        StartCoroutine(MoveMent(new Vector2(x, y)));
    }

    IEnumerator MoveMent(Vector2 dest)
    {
        while(Vector2.Distance(transform.position, dest) > 0f)
        {
            Vector2.MoveTowards(transform.position, dest, Time.deltaTime);
            yield return null;
        }
    }

    public void UpdateObjectInfo(IProtocol protocol)
    {
        ClientObejctIDInfo_B2C info = protocol as ClientObejctIDInfo_B2C;
        if(healthBar == null)
        {
            healthBar = FloatingHealthBarManager.Instance.CreateHealthBar(transform);
        }
        healthBar.UpdateHpBar((float)info.HP * 0.01f);
    }

    public void EndMove(float x, float y)
    {
        animator.CrossFadeInFixedTime("move", 0.1f);
    }

    public void Hit(int damage)
    {
        animator.CrossFadeInFixedTime("hit", 0.1f);
    }

    public void Attack()
    {
        animator.CrossFadeInFixedTime("attack", 0.1f);
    }

    public void Dead()
    {
        animator.CrossFadeInFixedTime("dead", 0.1f);
    }
}

