using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public interface IGameWorld
{
    void Init();
    void Update();
    void OnDestory();

}
public class GameMain : MonoBehaviour
{
    private GameMain m_GameMain;
    public GameMain Instance => m_GameMain;
    private IGameWorld m_GameWorld;
    private void Awake()
    {
        m_GameMain = this;
        
    }
   

    // Update is called once per frame
    void Update()
    {
        m_GameWorld?.Update();
    }
    private void OnDestroy()
    {
        m_GameWorld?.OnDestory();
    }
    public void InitAsServer()
    {
        m_GameWorld = new GameWorldServer();
        m_GameWorld.Init();
    }
    public void InitAsClient()
    {
        m_GameWorld = new GameWorldClient();
        m_GameWorld.Init();
    }
}
