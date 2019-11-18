using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Code.Shared;


public interface IGameWorldHandler
{
    IGameLoop GameLoop { get; }
    bool IsServer { get; }
    bool IsClient { get; }
    Transform EntityRoot { get; }
}