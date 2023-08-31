using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

abstract class CharacterController : TickBase {
    protected Vector2 startPoint;
    protected ZoneController zoneController;
    public List<Character> characterList = new List<Character>();

    public CharacterController(ZoneController cc, Vector2 startPoint) {
        this.zoneController = cc;
        this.startPoint = startPoint;
    }

    protected long elapsedTime = 0;
    protected float deltaTime = 0;

    public override void Update() {
        if (sw.IsRunning) {
            if (sw.ElapsedMilliseconds <= 0) {
                return;
            }

            elapsedTime = sw.ElapsedMilliseconds;
            sw.Stop();
            
        }

        sw.Start();

        #region move
        Parallel.ForEach(characterList, (pc) => {

            pc.Update();

            if (pc.isMoving) {
                //if (pc.dir == Direction.Right) {
                //    pc.SetPos(new Vector2(pc.pos.X + (elapsedTime / 1000) * pc.stat.SPEED, pc.pos.Y));
                //} else if (pc.dir == Direction.Left) {
                //    pc.SetPos(new Vector2(pc.pos.X - (elapsedTime / 1000) * pc.stat.SPEED, pc.pos.Y));
                //} else if (pc.dir == Direction.Up) {
                //    pc.SetPos(new Vector2(pc.pos.X, pc.pos.Y + (elapsedTime / 1000) * pc.stat.SPEED));
                //} else if (pc.dir == Direction.Down) {
                //    pc.SetPos(new Vector2(pc.pos.X, pc.pos.Y - (elapsedTime / 1000) * pc.stat.SPEED));
                //}

                pc.SetPos(pc.pos + pc.stat.SPEED * pc.dir * Program.deltaTime);

                BroadCast_RefreshPos(pc);
            }
            
        });

        //Barrier Lock

        #endregion

        #region attack


        #endregion





        #region attack

        #endregion

        //Barrier Lock






        #region status

        #endregion

        //Barrier Lock
    }

    public abstract Character CreateCharacter(Vector2 startPoint);
    public abstract void HandleDeadEvent(Character character);

    public void BroadCast_MoveStart(Character caster) {
        var protocol = new MoveStart_B2C {
            OBJECT_ID = caster.OBJECT_ID,
            Dir_x = caster.dir.X,
            Dir_y = caster.dir.Y,
    };

        caster.isMoving = true;

        zoneController.SendPacketToZone(protocol);
    }

    public void BroadCast_MoveEnd(Character caster) {
        var protocol = new MoveEnd_B2C {
            OBJECT_ID = caster.OBJECT_ID,
        };

        caster.isMoving = false;

        zoneController.SendPacketToZone(protocol);
    }

    public void BroadCast_RefreshPos(Character caster) {
        var protocol = new ChangePos_B2C {
            OBJECT_ID = caster.OBJECT_ID,
            Pos_x = caster.pos.X,
            Pos_y = caster.pos.Y,
        };

        zoneController.SendPacketToZone(protocol);
    }

    public void BroadCast_AttackTo(Character caster)
    {
        var protocol = new Attack_B2C
        {
            OBJECT_ID = caster.OBJECT_ID,
        };

        zoneController.SendPacketToZone(protocol);
    }

    public void TryAttack(Character attacker)
    {
        Character target = zoneController.npcController.characterList.Where(x => Vector2.Distance(x.pos, attacker.pos) <= 1.5f && !attacker.Equals(x)).OrderBy(x => Vector2.Distance(x.pos, attacker.pos)).FirstOrDefault();

        BroadCast_AttackTo(attacker);
        if (target != null)
        {
            BroadCast_ReceiveAttack(attacker, target);
        }
    }

    public void BroadCast_ReceiveAttack(Character attacker, Character hitter)
    {
        var protocol = new Hit_B2C
        {
            OBJECT_ID = hitter.OBJECT_ID,
            Damage = attacker.stat.ATTACK
        };

        zoneController.SendPacketToZone(protocol);
    }

    //public void BroadCast_AttackTo(Character caster, Character target) {
    //    target.ReceiveAttack(this);
    //}

    //public void BroadCast_ReceiveAttack(Character attacker) {
    //    ReceiveDamage(attacker.stat.ATTACK - this.stat.DEF);
    //}

    //void BroadCast_ReceiveDamage(int damage) {
    //    ChangeHP(damage);
    //}

    //void BroadCast_ChangeHP(int amount_change) {
    //    stat.HP += amount_change;

    //    if (stat.HP <= 0)
    //        OnDead();
    //}
}
