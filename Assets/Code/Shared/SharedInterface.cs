using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameLoopType
{
    Unknown,
    Client,
    Server
}
public interface IGameLoop
{
    void Init(IGameMainHandler gamemain);
    void Update();
    void OnDestory();
    void OnLogicUpdate();
    void OnFixedUpdate();
    void OnLateUpdate();



}

public interface IGameMainHandler
{
    IGameLoop GameLoop { get; }
}