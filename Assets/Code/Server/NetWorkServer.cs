using Code.Shared;
using LiteNetLib;
using LiteNetLib.Utils;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using UnityEngine;

public class NetWorkServer: INetEventListener
{
    private NetManager _netManager;
    private NetPacketProcessor _packetProcessor;
    private readonly NetDataWriter _cachedWriter = new NetDataWriter();
    public void InitNetWork()
    {
        _packetProcessor = new NetPacketProcessor();
        _netManager = new NetManager(this) {
            AutoRecycle = true
        };
    }
    #region LiteNetLib Interface
    public void OnConnectionRequest(ConnectionRequest request)
    {
        throw new System.NotImplementedException();
    }

    public void OnNetworkError(IPEndPoint endPoint, SocketError socketError)
    {
        throw new System.NotImplementedException();
    }

    public void OnNetworkLatencyUpdate(NetPeer peer, int latency)
    {
        throw new System.NotImplementedException();
    }

    public void OnNetworkReceive(NetPeer peer, NetPacketReader reader, DeliveryMethod deliveryMethod)
    {
        throw new System.NotImplementedException();
    }

    public void OnNetworkReceiveUnconnected(IPEndPoint remoteEndPoint, NetPacketReader reader, UnconnectedMessageType messageType)
    {
        throw new System.NotImplementedException();
    }

    public void OnPeerConnected(NetPeer peer)
    {
        throw new System.NotImplementedException();
    }

    public void OnPeerDisconnected(NetPeer peer, DisconnectInfo disconnectInfo)
    {
        throw new System.NotImplementedException();
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
    // Update is called once per frame
    public void Update()
    {
        _netManager.PollEvents();
    }
}
