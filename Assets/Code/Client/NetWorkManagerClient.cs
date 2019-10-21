using LiteNetLib;
using LiteNetLib.Utils;
using System.Net;
using System.Net.Sockets;

public class NetWorkManagerClient : INetEventListener
{
    private NetDataWriter _writer;
    private NetPacketProcessor _packetProcessor;
    private NetManager _netManager;
    public void InitNewWorkClient()
    {
        _writer = new NetDataWriter();

        _packetProcessor = new NetPacketProcessor();
        //_packetProcessor.RegisterNestedType((w, v) => w.Put(v), reader => reader.GetVector2());
        //_packetProcessor.RegisterNestedType<PlayerState>();
        //_packetProcessor.SubscribeReusable<PlayerJoinedPacket>(OnPlayerJoined);
        //_packetProcessor.SubscribeReusable<JoinAcceptPacket>(OnJoinAccept);
        //_packetProcessor.SubscribeReusable<PlayerLeavedPacket>(OnPlayerLeaved);
        _netManager = new NetManager(this)
        {
            AutoRecycle = true
        };
        _netManager.Start();
    }
    public void OnConnectionRequest(ConnectionRequest request)
    {
        
    }

    public void OnNetworkError(IPEndPoint endPoint, SocketError socketError)
    {
        
    }

    public void OnNetworkLatencyUpdate(NetPeer peer, int latency)
    {
        
    }

    public void OnNetworkReceive(NetPeer peer, NetPacketReader reader, DeliveryMethod deliveryMethod)
    {
        
    }

    public void OnNetworkReceiveUnconnected(IPEndPoint remoteEndPoint, NetPacketReader reader, UnconnectedMessageType messageType)
    {
        
    }

    public void OnPeerConnected(NetPeer peer)
    {
       
    }

    public void OnPeerDisconnected(NetPeer peer, DisconnectInfo disconnectInfo)
    {
        
    }
}
