using System;
using System.Collections.Generic;
using System.Text;

class Const
{
    public const int RECEIVE_BUFFER_SIZE = 256;
    public const int PACKET_HEADER_LENGTH = 4;

    public const int SESSION_SERVER_PORT = 8001;
    public const int BATTLE_SERVER_PORT = 8002;
}

public enum Direction
{
    Up,
    Down,
    Left,
    Right,
}