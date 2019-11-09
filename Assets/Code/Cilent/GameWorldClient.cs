using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Code.Shared;

public class GameWorldClient : IGameLoop
{
    private ClientNetWorkManager m_NetWorkManager;
    //public LogicTimer 
    public void Init(IGameMainHandler gamemain)
    {
        m_NetWorkManager = new ClientNetWorkManager();
        m_NetWorkManager.Init();
    }

    public void OnDestory()
    {
        m_NetWorkManager?.OnDestory();
    }

    // Update is called once per frame
    public void Update()
    {
        m_NetWorkManager?.Update();
    }
}
