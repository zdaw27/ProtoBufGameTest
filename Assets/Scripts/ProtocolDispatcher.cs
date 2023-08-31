using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using System.Threading;
using UnityEngine;

class ProtocolDispatcher : MonoSingleton<ProtocolDispatcher>{
    Dictionary<int, Action<IProtocol, TcpHandler>> HandlePool = new Dictionary<int, Action<IProtocol, TcpHandler>>();
    Queue<ProtocolAction> actionQueue = new Queue<ProtocolAction>();

    struct ProtocolAction
    {
        private Action<IProtocol, TcpHandler> action;
        IProtocol protocol;
        TcpHandler tcpHandler;

        public ProtocolAction(Action<IProtocol, TcpHandler> action, IProtocol protocol, TcpHandler tcpHandler)
        {
            this.action = action;
            this.protocol = protocol;
            this.tcpHandler = tcpHandler;
        }

        public void DoAction()
        {
            action.Invoke(protocol, tcpHandler);
        }
    }
    
    public void Register() {
        var baseType = typeof(IProtocol);
        var a = Assembly.GetAssembly(baseType).GetTypes().Where(t => baseType != t && baseType.IsAssignableFrom(t));
        foreach (var b in a) {
            IProtocol instance = (IProtocol)Activator.CreateInstance(b);
            HandlePool.Add(instance.GetProtocol_ID(), createAction(instance));
        }
    }

    public void Dispatch(IProtocol protocol, TcpHandler handler) {
        if (HandlePool.TryGetValue(protocol.GetProtocol_ID(), out var action)) {
            actionQueue.Enqueue(new ProtocolAction(action, protocol, handler));
        } else {
            Debug.Log("No Protocol ID");
        }
    }

    public void Update()
    {
        while(actionQueue.Count != 0)
        {
            if(actionQueue.TryDequeue(out var protocalAction))
            {
                protocalAction.DoAction();
            }
        }
    }

    Action<IProtocol, TcpHandler> createAction(IProtocol dummyProtocol) {
        Action<IProtocol, TcpHandler> action = null;

        if(dummyProtocol is Login_RES_S2C) {
            action = (IProtocol protocol, TcpHandler handler) => {
                var cast = protocol as Login_RES_S2C;
                Debug.Log("Receive : [Login_RES_S2C]");

                handler.SendPacket(new Login_FIN_C2S());

                TcpClient tcpClient = new TcpClient("127.0.0.1", Const.BATTLE_SERVER_PORT);

                GameController.battleHandler = gameObject.AddComponent<TcpHandler>();
                GameController.battleHandler.InitHandler(tcpClient, cast.ZONE_ID);

                GameController.battleHandler.SendPacket(new NewBattleUser_REQ_C2B {
                    USER_ID = 1,
                });

                Debug.Log("Send : [NewBattleUser_REQ_C2B]");

                Debug.Log("Send : [Login_FIN_C2S]");
            };
        }
        if (dummyProtocol is NewBattleUser_RES_C2B) {
            action = (IProtocol protocol, TcpHandler handler) => {
                var cast = protocol as NewBattleUser_RES_C2B;
                Debug.Log("Receive : [NewBattleUser_RES_C2B]");

                GameController.battleHandler.SendPacket(new NewBattleUser_FIN_C2B {
                    ZONE_ID = GameController.battleHandler.zone_id,
                });

                

                Debug.Log("Battle Server Connected!");
            };

        }
        else if (dummyProtocol is ClientObejctIDInfo)
        {
            action = (IProtocol protocol, TcpHandler handler) => {
                var cast = protocol as ClientObejctIDInfo;
                GameController.Instance.ClientUserObjectSpawn(cast.OBJECT_ID, Vector2.zero);
                Debug.Log("Receive : [ClientObejctIDInfo]");
            };
        }
        else if (dummyProtocol is MoveStart_B2C) {
            action = (IProtocol protocol, TcpHandler handler) => {
                var cast = protocol as MoveStart_B2C;
                Debug.Log("Receive : [MoveStart_B2C]");
            };
        } else if (dummyProtocol is MoveEnd_B2C) {
            action = (IProtocol protocol, TcpHandler handler) => {
                var cast = protocol as MoveEnd_B2C;
                
                Debug.Log("Receive : [MoveEnd_B2C]");
            };
        } else if (dummyProtocol is ChangePos_B2C) {
            action = (IProtocol protocol, TcpHandler handler) => {
                var cast = protocol as ChangePos_B2C;
                Debug.Log($"Receive : [ChangePos_B2C], OBJECT_ID : {cast.OBJECT_ID}");
                GameController.Instance.ClientObjectSpawn(cast.OBJECT_ID, new Vector2(cast.Pos_x, cast.Pos_y));
            };
        } else if (dummyProtocol is RestAPI_RES_S2C) {
            action = (IProtocol protocol, TcpHandler handler) => {
                var cast = protocol as RestAPI_RES_S2C;
                Debug.Log("Receive : [RestAPI_RES_S2C]");

                Debug.Log(cast.Info);
            };
        }
        else if (dummyProtocol is Attack_B2C)
        {
            action = (IProtocol protocol, TcpHandler handler) => {
                var cast = protocol as Attack_B2C;
                Debug.Log("Receive : [Attack_B2C]");

                Debug.Log($"Attacker : { cast.OBJECT_ID}");
            };
        }
        else if (dummyProtocol is Hit_B2C)
        {
            action = (IProtocol protocol, TcpHandler handler) => {
                var cast = protocol as Hit_B2C;
                Debug.Log("Receive : [Hit_B2C]");

                Debug.Log($"Hitter : { cast.OBJECT_ID} Damage : {cast.Damage}");
            };
        }

        return action;
    }
}

