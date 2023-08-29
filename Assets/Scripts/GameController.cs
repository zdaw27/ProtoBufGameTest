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
        if (battleHandler == null)
            return;

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            battleHandler.SendPacket(new MoveStart_C2B
            {
                Direction = (int)Direction.Up,
            });

            Debug.Log("Send : [MoveStart_C2B]");
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            battleHandler.SendPacket(new MoveStart_C2B
            {
                Direction = (int)Direction.Down,
            });

            Debug.Log("Send : [MoveStart_C2B]");
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            battleHandler.SendPacket(new MoveStart_C2B
            {
                Direction = (int)Direction.Right,
            });

            Debug.Log("Send : [MoveStart_C2B]");
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            battleHandler.SendPacket(new MoveStart_C2B
            {
                Direction = (int)Direction.Left,
            });

            Debug.Log("Send : [MoveStart_C2B]");
        }

        if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            battleHandler.SendPacket(new MoveEnd_C2B());

            Debug.Log("Send : [MoveEnd_C2B]");
        }
        else if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            battleHandler.SendPacket(new MoveEnd_C2B());

            Debug.Log("Send : [MoveEnd_C2B]");
        }
        else if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            battleHandler.SendPacket(new MoveEnd_C2B());

            Debug.Log("Send : [MoveEnd_C2B]");
        }
        else if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            battleHandler.SendPacket(new MoveEnd_C2B());

            Debug.Log("Send : [MoveEnd_C2B]");
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

    public void ClientObjectRemove(long objectID)
    {
        clientObjects.Remove(objectID);
    }

    public void ClientObjectAttack(long objectID)
    {
        clientObjects[objectID].Attack();
    }

    public void ClientObjectHit(long objectID, int damage)
    {
        clientObjects[objectID].Hit(damage);
    }

    public void ClientUserObjectSpawn(long objectID, Vector2 pos)
    {
        if (!clientObjects.ContainsKey(objectID))
        {
            clientObjects.Add(objectID, GameObject.Instantiate(userObjectPrefab).GetComponent<ClientObject>());

        }
        clientObjects[objectID].transform.position = pos;
    }

    public void ClientObjectSpawn(long objectID, Vector2 pos)
    {
        if (!clientObjects.ContainsKey(objectID))
        {
            clientObjects.Add(objectID, GameObject.Instantiate(npcObjectPrefab).GetComponent<ClientObject>());
        }
        clientObjects[objectID].transform.position = pos;
    }
}
