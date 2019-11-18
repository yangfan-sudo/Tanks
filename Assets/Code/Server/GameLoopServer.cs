﻿using Code.Shared;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLoopServer : IGameLoop
{
    private ushort _serverTick;
    public ushort Tick => _serverTick;
    private IGameMainHandler m_GameMain;
    public IGameMainHandler GameMain => m_GameMain;
    private NetWorkServer m_NetWorkServer;
    public NetWorkServer NetWorkServer => m_NetWorkServer;
    public void Init(IGameMainHandler gamemain)
    {
        m_GameMain = gamemain;
        m_NetWorkServer = new NetWorkServer();
        NetWorkServer.StartServer(10515);
    }

    public void OnDestory()
    {
        NetWorkServer.OnShutDown();
    }

    public void Update()
    {
        
    }
    public void OnLogicUpdate()
    {
        _serverTick = (ushort)((_serverTick + 1) % NetworkGeneral.MaxGameSequence);

    }

    public void OnFixedUpdate()
    {
        throw new System.NotImplementedException();
    }

    public void OnLateUpdate()
    {
        throw new System.NotImplementedException();
    }
}
