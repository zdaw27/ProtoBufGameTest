using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using System;
using UnityEngine;

public class GameController : MonoSingleton<GameController>
{
    public static TcpHandler sessionHandler = null;
    public static TcpHandler battleHandler = null;
    private Dictionary<long, ClientObject> clientObjects = null;
    [SerializeField]
    private GameObject npcObjectPrefab;
    [SerializeField]
    private GameObject userObjectPrefab;


    private void StartGame()
    {
        TcpClient tcpClient = new TcpClient("127.0.0.1", Const.SESSION_SERVER_PORT);

        Debug.Log("Session Server Connected!");

       

        sessionHandler = gameObject.AddComponent<TcpHandler>();
        sessionHandler.InitHandler(tcpClient, 0);

        sessionHandler.SendPacket(new Login_REQ_C2S
        {
            PID = DateTime.Now.Ticks.ToString(),
        });

        Debug.Log("Send : [Login_REQ_C2S]");
    }

    
    // Start is called before the first frame update
    void Start()
    {
        
        if (clientObjects == null)
        {
            clientObjects = new Dictionary<long, ClientObject>();
        }
        
        
        ProtocolManager.Instance.Register();
        ProtocolDispatcher.Instance.Register();
        StartGame();
    }

    private void Update()
    {
        if (clientObjects.Count == 0)
            return;

        Vector2 inputDir = Vector2.zero;

        if (Input.GetKey(KeyCode.UpArrow))
        {
            inputDir += Vector2.up;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            inputDir += Vector2.down;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            inputDir += Vector2.right;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            inputDir += Vector2.left;
        }

        inputDir.Normalize();

        if (inputDir.magnitude > 0f)
        {
            battleHandler.SendPacket(new MoveStart_C2B
            {
                Pos_x = inputDir.x,
                Pos_y = inputDir.y

            }) ;

            Debug.Log("Send : [MoveStart_C2B]");
        }
        else
        {
            battleHandler.SendPacket(new MoveEnd_C2B());

            Debug.Log("Send : [MoveEnd_C2B]");
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            battleHandler.SendPacket(new Attack_C2B());
        }
        
    }

    private void OnDestroy()
    {
        sessionHandler.StopProcess();
        battleHandler.StopProcess();
    }

    public void ClientObjectsStartMove(int objectID, int x, int y)
    {

    }

    public void ClientObjectsEndMove(long objectID, int x, int y)
    {
    }

    public void ClientObjectAttack(long objectID)
    {
        if (clientObjects.ContainsKey(objectID))
            clientObjects[objectID].Attack();
        else
            Debug.Log("ObjectID Attack Not Found");
    }

    public void ClientObjectHit(long objectID, int damage)
    {
        if (clientObjects.ContainsKey(objectID))
            clientObjects[objectID].Hit(damage);
        else
            Debug.Log("ObjectID Hit Not Found");
    }

    public void ClientRemoveObject(long objectID)
    {
        GameObject.Destroy(clientObjects[objectID].gameObject);
        if (clientObjects.ContainsKey(objectID))
        {
            clientObjects.Remove(objectID);
        }
    }

    public void ClientUpdateObjectInfo(long objectID, IProtocol protocol)
    {
        ClientObejctIDInfo_B2C info = protocol as ClientObejctIDInfo_B2C;
        SpawnObject(objectID, (ObjectType)info.ObjType);
        clientObjects[objectID].UpdateObjectInfo(info);
    }

    public void ClientUpdatePos(long objectID, Vector2 pos)
    {
        clientObjects[objectID].transform.position = pos;
    }

    private void SpawnObject(long objectID, ObjectType type)
    {
        if (!clientObjects.ContainsKey(objectID))
        {
            if(type == ObjectType.NPCObject)
                clientObjects.Add(objectID, GameObject.Instantiate(npcObjectPrefab).GetComponent<ClientObject>());
            else if(type == ObjectType.PlayerObject)
                clientObjects.Add(objectID, GameObject.Instantiate(userObjectPrefab).GetComponent<ClientObject>());
        }
    }

    private void RemoveObject(long objectID)
    {

    }
}
