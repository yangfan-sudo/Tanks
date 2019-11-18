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

    public void OnFixedUpdate()
    {
        throw new System.NotImplementedException();
    }

    public void OnLateUpdate()
    {
        throw new System.NotImplementedException();
    }

    public void OnLogicUpdate()
    {
        throw new System.NotImplementedException();
    }

    // Update is called once per frame
    public void Update()
    {
        m_NetWorkManager?.Update();
    }
}
