using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Google.Protobuf;
using System.IO;
using Google.Protobuf.Examples.AddressBook;

public class PacketHandler : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Character newChar = new Character();
        newChar.X = 2;
        newChar.Y = 3;
        Debug.Log(SerializeOrderInfo(newChar));

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private static byte[] SerializeOrderInfo(Character orderInfo)
    {
        byte[] protoBufMessageBytes;

        using (var stream = new MemoryStream())
        {
            // Save the Order Info to a stream
            orderInfo.WriteTo(stream);
            protoBufMessageBytes = stream.ToArray();
        }

        return protoBufMessageBytes;
    }
}
