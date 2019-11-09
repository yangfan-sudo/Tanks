using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGameLoop
{
    void Init(IGameMainHandler gamemain);
    void Update();
    void OnDestory();

}

public interface IGameMainHandler
{
    IGameLoop GameLoop { get; }
}