using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGameLoop
{
    void Update();
    void Start();
}
public class GameMain : MonoBehaviour
{
    private IGameLoop m_GameLoop;
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        m_GameLoop?.Update();
    }
    public void StartServerLoop()
    {
        m_GameLoop = new ServerLoop();
    }
    public void StartClientLoop()
    {
        m_GameLoop = new ServerLoop();
    }
}
