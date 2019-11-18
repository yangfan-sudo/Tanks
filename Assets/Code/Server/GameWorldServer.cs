using Code.Server;
using Code.Shared;
using LiteNetLib;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameWorldServer : IGameWorldHandler
{
    private IGameLoop m_GameLoop;
    public IGameLoop GameLoop => m_GameLoop;

    public bool IsServer => true;

    public bool IsClient => false;
    private Transform m_EntityRoot;

    public Transform EntityRoot => m_EntityRoot;

    private NetWorkServer m_NetWorkServer;
    public NetWorkServer NetWorkServer => m_NetWorkServer;

    private ServerPlayerManager _playerManager;

    private PlayerInputPacket _cachedCommand = new PlayerInputPacket();

    private ushort _serverTick;
    public ushort Tick => _serverTick;
    private ServerState _serverState;

    public const int MaxPlayers = 64;
    public GameWorldServer(NetWorkServer networkserver)
    {
        m_NetWorkServer = networkserver;

        NetWorkServer.RegisterNestedType<PlayerState>();

        NetWorkServer.SubscribeReusable<JoinPacket, NetPeer>(OnJoinReceived);
        NetWorkServer.RegisterInputReceive(OnInputReceived);

        _playerManager = new ServerPlayerManager(this);
    }

    public void OnLogicUpdate()
    {
        _serverTick = (ushort)((_serverTick + 1) % NetworkGeneral.MaxGameSequence);
        _playerManager.LogicUpdate();
        if (_serverTick % 2 == 0)
        {
            _serverState.Tick = _serverTick;
            _serverState.PlayerStates = _playerManager.PlayerStates;
            int pCount = _playerManager.Count;

            foreach (ServerPlayer p in _playerManager)
            {
                int statesMax = p.AssociatedPeer.GetMaxSinglePacketSize(DeliveryMethod.Unreliable) - ServerState.HeaderSize;
                statesMax /= PlayerState.Size;

                for (int s = 0; s < (pCount - 1) / statesMax + 1; s++)
                {
                    //TODO: divide
                    _serverState.LastProcessedCommand = p.LastProcessedCommandId;
                    _serverState.PlayerStatesCount = pCount;
                    _serverState.StartState = s * statesMax;
                    p.AssociatedPeer.Send(NetWorkServer.WriteSerializable(PacketType.ServerState, _serverState), DeliveryMethod.Unreliable);
                }
            }
        }
    }

    private void OnJoinReceived(JoinPacket joinPacket, NetPeer peer)
    {
        Debug.Log("[S] Join packet received: " + joinPacket.UserName);
        var player = new ServerPlayer(_playerManager, joinPacket.UserName, peer);
        _playerManager.AddPlayer(player);

        player.Spawn(new Vector2(Random.Range(-2f, 2f), Random.Range(-2f, 2f)));

        //Send join accept
        var ja = new JoinAcceptPacket { Id = player.Id };
        peer.Send(NetWorkServer.WritePacket(ja), DeliveryMethod.ReliableOrdered);

        //Send to old players info about new player
        var pj = new PlayerJoinedPacket();
        pj.UserName = joinPacket.UserName;
        pj.NewPlayer = true;
        pj.InitialPlayerState = player.NetworkState;
        pj.ServerTick = _serverTick;
        NetWorkServer.SendToAll(NetWorkServer.WritePacket(pj), DeliveryMethod.ReliableOrdered, peer);

        //Send to new player info about old players
        pj.NewPlayer = false;
        foreach (ServerPlayer otherPlayer in _playerManager)
        {
            if (otherPlayer == player)
                continue;
            pj.UserName = otherPlayer.Name;
            pj.InitialPlayerState = otherPlayer.NetworkState;
            peer.Send(NetWorkServer.WritePacket(pj), DeliveryMethod.ReliableOrdered);
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
    public void SendShoot(ref ShootPacket sp)
    {
        NetWorkServer.SendToAll(NetWorkServer.WriteSerializable(PacketType.Shoot, sp), DeliveryMethod.ReliableUnordered);
    }
}
