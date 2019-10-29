using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGameLoop
{
    void Init();
    void Update();
    void OnDestory();

}
public class GameMain : MonoBehaviour
{
    private GameMain m_GameMain;
    public GameMain Instance => m_GameMain;
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
        m_GameLoop = new ServerLoop();
        m_GameLoop.Init();
    }
    public void InitAsClient()
    {

    }
}
