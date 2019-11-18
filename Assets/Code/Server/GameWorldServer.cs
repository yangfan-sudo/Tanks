using Code.Shared;
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
    public GameWorldServer(NetWorkServer networkserver)
    {
        m_NetWorkServer = networkserver;
    }
}
