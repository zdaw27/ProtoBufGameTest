using System.IO;
using System.Text;

class NewBattleUser_REQ_C2B : IProtocol {
	//COMMON
	public int PACKET_LENGTH = 0;
	public int PROTOCOL_ID = 1;
	//MEMBER
	public long USER_ID;
	public int Level;
	public int HP;
	public int TODOUserInfo;
	public void SetPacketLength() {
		PACKET_LENGTH = sizeof(int) + sizeof(int) + sizeof(long) + sizeof(int) + sizeof(int) + sizeof(int);
	}
	public int GetPacketLength() {
		return PACKET_LENGTH;
	}
	public int GetProtocol_ID() {
		return PROTOCOL_ID;
	}
	public void Read(BinaryReader br) {
		USER_ID = br.ReadInt64();
		Level = br.ReadInt32();
		HP = br.ReadInt32();
		TODOUserInfo = br.ReadInt32();
	}
	public void Write(BinaryWriter bw) {
		SetPacketLength();
		bw.Write(PACKET_LENGTH);
		bw.Write(PROTOCOL_ID);
		bw.Write(USER_ID);
		bw.Write(Level);
		bw.Write(HP);
		bw.Write(TODOUserInfo);
	}
}
class NewBattleUser_RES_C2B : IProtocol {
	//COMMON
	public int PACKET_LENGTH = 0;
	public int PROTOCOL_ID = 2;
	//MEMBER
	public long ObjectIDList;
	public int TODOStatusList;
	public void SetPacketLength() {
		PACKET_LENGTH = sizeof(int) + sizeof(int) + sizeof(long) + sizeof(int);
	}
	public int GetPacketLength() {
		return PACKET_LENGTH;
	}
	public int GetProtocol_ID() {
		return PROTOCOL_ID;
	}
	public void Read(BinaryReader br) {
		ObjectIDList = br.ReadInt64();
		TODOStatusList = br.ReadInt32();
	}
	public void Write(BinaryWriter bw) {
		SetPacketLength();
		bw.Write(PACKET_LENGTH);
		bw.Write(PROTOCOL_ID);
		bw.Write(ObjectIDList);
		bw.Write(TODOStatusList);
	}
}
class NewBattleUser_FIN_C2B : IProtocol {
	//COMMON
	public int PACKET_LENGTH = 0;
	public int PROTOCOL_ID = 3;
	//MEMBER
	public int ZONE_ID;
	public void SetPacketLength() {
		PACKET_LENGTH = sizeof(int) + sizeof(int) + sizeof(int);
	}
	public int GetPacketLength() {
		return PACKET_LENGTH;
	}
	public int GetProtocol_ID() {
		return PROTOCOL_ID;
	}
	public void Read(BinaryReader br) {
		ZONE_ID = br.ReadInt32();
	}
	public void Write(BinaryWriter bw) {
		SetPacketLength();
		bw.Write(PACKET_LENGTH);
		bw.Write(PROTOCOL_ID);
		bw.Write(ZONE_ID);
	}
}
class MoveStart_C2B : IProtocol {
	//COMMON
	public int PACKET_LENGTH = 0;
	public int PROTOCOL_ID = 4;
	//MEMBER
	public float Pos_x;
	public float Pos_y;
	public void SetPacketLength() {
		PACKET_LENGTH = sizeof(int) + sizeof(int) + sizeof(float) + sizeof(float);
	}
	public int GetPacketLength() {
		return PACKET_LENGTH;
	}
	public int GetProtocol_ID() {
		return PROTOCOL_ID;
	}
	public void Read(BinaryReader br) {
		Pos_x = br.ReadSingle();
		Pos_y = br.ReadSingle();
	}
	public void Write(BinaryWriter bw) {
		SetPacketLength();
		bw.Write(PACKET_LENGTH);
		bw.Write(PROTOCOL_ID);
		bw.Write(Pos_x);
		bw.Write(Pos_y);
	}
}
class MoveEnd_C2B : IProtocol {
	//COMMON
	public int PACKET_LENGTH = 0;
	public int PROTOCOL_ID = 5;
	//MEMBER
	public void SetPacketLength() {
		PACKET_LENGTH = sizeof(int) + sizeof(int);
	}
	public int GetPacketLength() {
		return PACKET_LENGTH;
	}
	public int GetProtocol_ID() {
		return PROTOCOL_ID;
	}
	public void Read(BinaryReader br) {
	}
	public void Write(BinaryWriter bw) {
		SetPacketLength();
		bw.Write(PACKET_LENGTH);
		bw.Write(PROTOCOL_ID);
	}
}
class MoveStart_B2C : IProtocol {
	//COMMON
	public int PACKET_LENGTH = 0;
	public int PROTOCOL_ID = 6;
	//MEMBER
	public long OBJECT_ID;
	public float Dir_x;
	public float Dir_y;
	public void SetPacketLength() {
		PACKET_LENGTH = sizeof(int) + sizeof(int) + sizeof(long) + sizeof(float) + sizeof(float);
	}
	public int GetPacketLength() {
		return PACKET_LENGTH;
	}
	public int GetProtocol_ID() {
		return PROTOCOL_ID;
	}
	public void Read(BinaryReader br) {
		OBJECT_ID = br.ReadInt64();
		Dir_x = br.ReadSingle();
		Dir_y = br.ReadSingle();
	}
	public void Write(BinaryWriter bw) {
		SetPacketLength();
		bw.Write(PACKET_LENGTH);
		bw.Write(PROTOCOL_ID);
		bw.Write(OBJECT_ID);
		bw.Write(Dir_x);
		bw.Write(Dir_y);
	}
}
class MoveEnd_B2C : IProtocol {
	//COMMON
	public int PACKET_LENGTH = 0;
	public int PROTOCOL_ID = 7;
	//MEMBER
	public long OBJECT_ID;
	public float Pos_x;
	public float Pos_y;
	public void SetPacketLength() {
		PACKET_LENGTH = sizeof(int) + sizeof(int) + sizeof(long) + sizeof(float) + sizeof(float);
	}
	public int GetPacketLength() {
		return PACKET_LENGTH;
	}
	public int GetProtocol_ID() {
		return PROTOCOL_ID;
	}
	public void Read(BinaryReader br) {
		OBJECT_ID = br.ReadInt64();
		Pos_x = br.ReadSingle();
		Pos_y = br.ReadSingle();
	}
	public void Write(BinaryWriter bw) {
		SetPacketLength();
		bw.Write(PACKET_LENGTH);
		bw.Write(PROTOCOL_ID);
		bw.Write(OBJECT_ID);
		bw.Write(Pos_x);
		bw.Write(Pos_y);
	}
}
class ChangePos_B2C : IProtocol {
	//COMMON
	public int PACKET_LENGTH = 0;
	public int PROTOCOL_ID = 8;
	//MEMBER
	public long OBJECT_ID;
	public float Pos_x;
	public float Pos_y;
	public void SetPacketLength() {
		PACKET_LENGTH = sizeof(int) + sizeof(int) + sizeof(long) + sizeof(float) + sizeof(float);
	}
	public int GetPacketLength() {
		return PACKET_LENGTH;
	}
	public int GetProtocol_ID() {
		return PROTOCOL_ID;
	}
	public void Read(BinaryReader br) {
		OBJECT_ID = br.ReadInt64();
		Pos_x = br.ReadSingle();
		Pos_y = br.ReadSingle();
	}
	public void Write(BinaryWriter bw) {
		SetPacketLength();
		bw.Write(PACKET_LENGTH);
		bw.Write(PROTOCOL_ID);
		bw.Write(OBJECT_ID);
		bw.Write(Pos_x);
		bw.Write(Pos_y);
	}
}
class ClientObejctIDInfo : IProtocol {
	//COMMON
	public int PACKET_LENGTH = 0;
	public int PROTOCOL_ID = 9;
	//MEMBER
	public long OBJECT_ID;
	public int HP;
	public void SetPacketLength() {
		PACKET_LENGTH = sizeof(int) + sizeof(int) + sizeof(long) + sizeof(int);
	}
	public int GetPacketLength() {
		return PACKET_LENGTH;
	}
	public int GetProtocol_ID() {
		return PROTOCOL_ID;
	}
	public void Read(BinaryReader br) {
		OBJECT_ID = br.ReadInt64();
		HP = br.ReadInt32();
	}
	public void Write(BinaryWriter bw) {
		SetPacketLength();
		bw.Write(PACKET_LENGTH);
		bw.Write(PROTOCOL_ID);
		bw.Write(OBJECT_ID);
		bw.Write(HP);
	}
}
class Hit_B2C : IProtocol {
	//COMMON
	public int PACKET_LENGTH = 0;
	public int PROTOCOL_ID = 10;
	//MEMBER
	public long OBJECT_ID;
	public int Damage;
	public void SetPacketLength() {
		PACKET_LENGTH = sizeof(int) + sizeof(int) + sizeof(long) + sizeof(int);
	}
	public int GetPacketLength() {
		return PACKET_LENGTH;
	}
	public int GetProtocol_ID() {
		return PROTOCOL_ID;
	}
	public void Read(BinaryReader br) {
		OBJECT_ID = br.ReadInt64();
		Damage = br.ReadInt32();
	}
	public void Write(BinaryWriter bw) {
		SetPacketLength();
		bw.Write(PACKET_LENGTH);
		bw.Write(PROTOCOL_ID);
		bw.Write(OBJECT_ID);
		bw.Write(Damage);
	}
}
class Attack_B2C : IProtocol {
	//COMMON
	public int PACKET_LENGTH = 0;
	public int PROTOCOL_ID = 11;
	//MEMBER
	public long OBJECT_ID;
	public void SetPacketLength() {
		PACKET_LENGTH = sizeof(int) + sizeof(int) + sizeof(long);
	}
	public int GetPacketLength() {
		return PACKET_LENGTH;
	}
	public int GetProtocol_ID() {
		return PROTOCOL_ID;
	}
	public void Read(BinaryReader br) {
		OBJECT_ID = br.ReadInt64();
	}
	public void Write(BinaryWriter bw) {
		SetPacketLength();
		bw.Write(PACKET_LENGTH);
		bw.Write(PROTOCOL_ID);
		bw.Write(OBJECT_ID);
	}
}
class Attack_C2B : IProtocol {
	//COMMON
	public int PACKET_LENGTH = 0;
	public int PROTOCOL_ID = 12;
	//MEMBER
	public long OBJECT_ID;
	public void SetPacketLength() {
		PACKET_LENGTH = sizeof(int) + sizeof(int) + sizeof(long);
	}
	public int GetPacketLength() {
		return PACKET_LENGTH;
	}
	public int GetProtocol_ID() {
		return PROTOCOL_ID;
	}
	public void Read(BinaryReader br) {
		OBJECT_ID = br.ReadInt64();
	}
	public void Write(BinaryWriter bw) {
		SetPacketLength();
		bw.Write(PACKET_LENGTH);
		bw.Write(PROTOCOL_ID);
		bw.Write(OBJECT_ID);
	}
}
