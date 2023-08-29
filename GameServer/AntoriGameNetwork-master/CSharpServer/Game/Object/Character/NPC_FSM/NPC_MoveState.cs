using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;

class NPC_MoveState : FSMState<NPC> {
    Random r = new Random();
    public override void Enter(NPC npc) {
        sw.Restart();
        if(npc.dir.Length() > 0f)
        {
            npc.dir *= -1f;
        }
        else
        {
            npc.dir = new Vector2((float)r.NextDouble(), (float)r.NextDouble());
            npc.dir = Vector2.Normalize(npc.dir);
        }
        npc.isMoving = true;
    }
    public override void Update(NPC npc) {
        if (sw.Elapsed.TotalSeconds >= 5) {
            npc.FSM.ChangeState(npc.IdleState);
        }
        //TODO find Enemy
    }
    public override void Exit(NPC npc) {
        npc.isMoving = false;
        if (sw.IsRunning) {
            sw.Stop();
        }
    }
}
