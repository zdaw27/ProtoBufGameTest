﻿using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

class STAT {
    public int HP {
        get;
        set;
    }
    public int ATTACK {
        get;
        set;
    }
    public int DEF {
        get;
        set;
    }
    public int SPEED {
        get;
        set;
    }
}

class Character : GameObject {

    private float attackCool = 1f;

    public Vector2 pos {
        get; private set;
    }
    public Vector2 dir {
        get; set;
    }

    public STAT stat {
        get; set;
    }

    public bool isMoving {
        get; set;
    }

    public CharacterController CharacterController {
        get; set;
    }

    public Character(STAT stat, Vector2 pos) {
        this.stat = stat;
        this.pos = pos;
    }

    public void SetPos(Vector2 dest) {
        pos = dest;
    }

    public void MoveStart(Vector2 dir) {
        this.dir = dir;
    }

    public void AttackTo(Character target) {
        target.ReceiveAttack(this);
        if (attackCool >= 1f)
        {
            attackCool = 0f;
            CharacterController.BroadCast_AttackTo(target);
        }
        
    }

    public void Update()
    {
        if (attackCool < 1f)
        {
            attackCool += Program.deltaTime;
        }
    }

    public void ReceiveAttack(Character attacker) {
        ReceiveDamage(attacker.stat.ATTACK - this.stat.DEF);
        CharacterController.BroadCast_ReceiveAttack(attacker, this);
    }

    void ReceiveDamage(int damage) {
        ChangeHP(damage);
    }

    void ChangeHP(int amount_change) {
        stat.HP += amount_change;

        if (stat.HP <= 0)
            OnDead();
    }

    void OnDead() {
        CharacterController.HandleDeadEvent(this);
    }
}

