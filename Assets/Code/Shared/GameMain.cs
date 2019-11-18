using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Code.Shared;


public class GameMain : MonoBehaviour, IGameMainHandler
{
    private GameMain m_GameMain;
    public GameMain Instance => m_GameMain;

    public IGameLoop GameLoop => m_GameLoop;

    private IGameLoop m_GameLoop;
    private GameLoopType m_GameLoopType;
    public GameLoopType GameLoopType => m_GameLoopType;
    private bool m_StartGame = false;


    private LogicTimer _logicTimer;
    private void Awake()
    {
        m_GameMain = this;
        _logicTimer = new LogicTimer(OnLogicUpdate);
    }
    private void OnLogicUpdate()
    {
        if (!m_StartGame)
        {
            return;
        }
        m_GameLoop?.OnLogicUpdate();
    }

    // Update is called once per frame
    void Update()
    {
        if(!m_StartGame)
        {
            return;
        }
        m_GameLoop?.Update();
        _logicTimer?.Update();
    }
    private void OnDestroy()
    {
        m_GameLoop?.OnDestory();
    }
    private void OnGameLoopStart()
    {

        if (m_StartGame)
        {
            return;
        }
        m_StartGame = true;
        _logicTimer.Start();

    }
    private void RequestGameLoop(IGameLoop gameLoop, GameLoopType looptype)
    {
        if (m_GameLoop == null && gameLoop != null)
        {
            m_GameLoop = gameLoop;
            m_GameLoopType = looptype;
            m_GameLoop.Init(this);
            if (!m_StartGame)
            {
                OnGameLoopStart();  
            }
           
        }
    }
    public void CreateGameLoop(GameLoopType looptype)
    {
        switch(looptype)
        {
            case GameLoopType.Client:
                RequestGameLoop(new GameLoopClient(), looptype);
                break;
            case GameLoopType.Server:
                RequestGameLoop(new GameLoopServer(), looptype);
                break;
        }

    }
}
