using Code.Shared;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLoopClient : IGameLoop
{
    private ushort _serverTick;
    public ushort Tick => _serverTick;
    private IGameMainHandler m_GameMain;
    public void Init(IGameMainHandler gamemain)
    {
        m_GameMain = gamemain;
    }

    public void OnDestory()
    {
        
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
