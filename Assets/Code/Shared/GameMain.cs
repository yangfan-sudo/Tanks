using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class GameMain : MonoBehaviour, IGameMainHandler
{
    private GameMain m_GameMain;
    public GameMain Instance => m_GameMain;

    public IGameLoop GameLoop => m_GameLoop;

    private IGameLoop m_GameLoop;
    private void Awake()
    {
        m_GameMain = this;
        
    }
   

    // Update is called once per frame
    void Update()
    {
        m_GameLoop?.Update();
    }
    private void OnDestroy()
    {
        m_GameLoop?.OnDestory();
    }
    public void InitAsServer()
    {
        m_GameLoop = new GameLoopServer();
        m_GameLoop.Init(this);
    }
    public void InitAsClient()
    {

    }
}
