using Code.Shared;
using LiteNetLib;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLoopServer : IGameLoop
{
  
    private IGameMainHandler m_GameMain;
    public IGameMainHandler GameMain => m_GameMain;
    private NetWorkServer m_NetWorkServer;
    public NetWorkServer NetWorkServer => m_NetWorkServer;

    private GameWorldServer m_GameWorldServer;
    public void Init(IGameMainHandler gamemain)
    {
        m_GameMain = gamemain;
        m_NetWorkServer = new NetWorkServer();
        NetWorkServer.StartServer(10515);
        m_GameWorldServer = new GameWorldServer(NetWorkServer);

       
    }

    public void OnDestory()
    {
        NetWorkServer.OnShutDown();
    }

    public void Update()
    {
        NetWorkServer.Update();
    }
    public void OnLogicUpdate()
    {
        
        m_GameWorldServer?.OnLogicUpdate();

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
