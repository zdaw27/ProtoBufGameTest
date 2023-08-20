﻿using System;
using System.Collections.Generic;
using System.Numerics;
using System.Threading.Tasks;
using System.Text;
using System.Threading;

class ZoneController : TickBase {
    public int ZONE_ID = 0;
    int OBJECT_ID = 0;

    List<TcpHandler> clientList = new List<TcpHandler>();
    List<TickBase> controllerList = new List<TickBase>();
    public NPCController npcController {
        get;
        private set;
    }
    public PlayerCharacterController playerCharacterController {
        get;
        private set;
    }

    //TODO : Load For Map Data
    Vector2 startPoint = new Vector2 {
        X = 0,
        Y = 0
    };

    #region Controller Initialize
    public ZoneController() {
        npcController = new NPCController(this, startPoint);
        controllerList.Add(npcController);

        playerCharacterController = new PlayerCharacterController(this, startPoint);
        controllerList.Add(playerCharacterController);
    }

    #endregion

    #region Zone Logic
    public override void Update() {
        foreach(var con in controllerList){
            con.Update();
        }
    }

    #endregion

    #region Session
    public void AddClient(TcpHandler_Battle client) {
        clientList.Add(client);

        Character character = playerCharacterController.CreateCharacter(startPoint);

        if (character is PlayerCharacter) {
            client.PlayerCharacter = character as PlayerCharacter;
        } else {
            Console.WriteLine("Player Character Create Error!");
        }
    }

    public void RemoveClient(TcpHandler_Battle client) {
        clientList.Remove(client);
        client.PlayerCharacter = null;
    }

    public void SendPacketToZone(IProtocol protocol) {
        foreach (var client in clientList) {
            client.SendPacket(protocol);
        }
    }

    public int GetObjectID() {
        return Interlocked.Increment(ref OBJECT_ID);
    }

    #endregion
}
