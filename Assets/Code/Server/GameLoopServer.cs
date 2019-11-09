using Code.Shared;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLoopServer : IGameLoop
{
    private NetWorkServer m_NetWorkServer;
    private LogicTimer _logicTimer;
    private IGameMainHandler m_GameMain;
    private ushort _serverTick;
    public ushort Tick => _serverTick;
    public void Init(IGameMainHandler gamemain)
    {
        m_GameMain = gamemain;
        _logicTimer = new LogicTimer(OnLogicUpdate);
    }

    public void OnDestory()
    {
        
    }

    // Update is called once per frame
    public void Update()
    {
        m_NetWorkServer.Update(); //要注意先后顺序，先处理网络事件，在执行logicupdate。
                                  //（因为_logicTimer 的_action在执行游戏逻辑之前要先拿到数据）
        _logicTimer.Update();
    }
    private void OnLogicUpdate()
    {
        _serverTick = (ushort)((_serverTick + 1) % NetworkGeneral.MaxGameSequence);
       
    }


}
