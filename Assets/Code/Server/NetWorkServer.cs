using Code.Shared;
using LiteNetLib;
using LiteNetLib.Utils;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using UnityEngine;
using Code.Server;

public class NetWorkServer: INetEventListener
{
    private NetManager _netManager;
    private NetPacketProcessor _packetProcessor;
    private readonly NetDataWriter _cachedWriter = new NetDataWriter();
    private ServerPlayerManager _playerManager;
    private PlayerInputPacket _cachedCommand = new PlayerInputPacket();
    private ServerState _serverState;
    private ushort _serverTick;
    public ushort Tick => _serverTick;
    public NetWorkServer()
    {
        _packetProcessor = new NetPacketProcessor();
        _netManager = new NetManager(this) {
            AutoRecycle = true
        };
    }
    public void StartServer(int port)
    {
        if (_netManager.IsRunning)
            return;
        _netManager.Start(10515);
    }
    // Update is called once per frame
    public void Update()
    {
        _netManager.PollEvents();
    }
    public void OnShutDown()
    {
        _netManager.Stop();
    }
    #region LiteNetLib Interface
    public void OnConnectionRequest(ConnectionRequest request)
    {
        request.AcceptIfKey("ExampleGame");
    }

    public void OnNetworkError(IPEndPoint endPoint, SocketError socketError)
    {
        Debug.Log("[S] NetworkError: " + socketError);
    }

    public void OnNetworkLatencyUpdate(NetPeer peer, int latency)
    {
        if (peer.Tag != null)
        {
            var p = (ServerPlayer)peer.Tag;
            p.Ping = latency;
        }
    }

    public void OnNetworkReceive(NetPeer peer, NetPacketReader reader, DeliveryMethod deliveryMethod)
    {
        byte packetType = reader.GetByte();
        if (packetType >= NetworkGeneral.PacketTypesCount)
            return;
        PacketType pt = (PacketType)packetType;
        switch (pt)
        {
            case PacketType.Movement:
                OnInputReceived(reader, peer);
                break;
            case PacketType.Serialized:
                _packetProcessor.ReadAllPackets(reader, peer);
                break;
            default:
                Debug.Log("Unhandled packet: " + pt);
                break;
        }
    }
    private void OnInputReceived(NetPacketReader reader, NetPeer peer)
    {
        if (peer.Tag == null)
            return;
        _cachedCommand.Deserialize(reader);
        var player = (ServerPlayer)peer.Tag;

        bool antilagApplied = _playerManager.EnableAntilag(player);
        player.ApplyInput(_cachedCommand, LogicTimer.FixedDelta);
        if (antilagApplied)
            _playerManager.DisableAntilag();
    }
    public void OnNetworkReceiveUnconnected(IPEndPoint remoteEndPoint, NetPacketReader reader, UnconnectedMessageType messageType)
    {
        
    }

    public void OnPeerConnected(NetPeer peer)
    {
        Debug.Log("[S] Player connected: " + peer.EndPoint);
    }

    public void OnPeerDisconnected(NetPeer peer, DisconnectInfo disconnectInfo)
    {
        Debug.Log("[S] Player disconnected: " + disconnectInfo.Reason);

        if (peer.Tag != null)
        {
            var plp = new PlayerLeavedPacket { Id = (byte)peer.Id };
            _netManager.SendToAll(WritePacket(plp), DeliveryMethod.ReliableOrdered);
        }
    }
    #endregion
    #region Serializable  func
    private NetDataWriter WriteSerializable<T>(PacketType type, T packet) where T : struct, INetSerializable
    {
        _cachedWriter.Reset();
        _cachedWriter.Put((byte)type);
        packet.Serialize(_cachedWriter);
        return _cachedWriter;
    }

    private NetDataWriter WritePacket<T>(T packet) where T : class, new()
    {
        _cachedWriter.Reset();
        _cachedWriter.Put((byte)PacketType.Serialized);
        _packetProcessor.Write(_cachedWriter, packet);
        return _cachedWriter;
    }
    #endregion
  
}
