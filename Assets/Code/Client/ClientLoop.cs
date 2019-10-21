using Code.Common;
using LiteNetLib;
using LiteNetLib.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientLoop: IGameLoop
{
    public static LogicTimer LogicTimer { get; private set; }
   
    private NetWorkManagerClient m_NetWorkClient;
    public void Update()
    {
        
    }
    void IGameLoop.Start()
    {
        LogicTimer = new LogicTimer(OnLogicUpdate);
        m_NetWorkClient = new NetWorkManagerClient();
        m_NetWorkClient.InitNewWorkClient();
    }
    private void OnLogicUpdate()
    {
       
    }
}
