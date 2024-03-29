using System.Collections.Generic;
using Code.Shared;
using LiteNetLib;
using UnityEngine;

namespace Code.Server
{
    public class ServerPlayerManager : BasePlayerManager
    {
        private readonly GameWorldServer _serverLogic;
        private readonly ServerPlayer[] _players;
        private readonly AntilagSystem _antilagSystem;
        
        public readonly PlayerState[] PlayerStates;
        private int _playersCount;
        
        
        public override int Count => _playersCount;

        public ServerPlayerManager(GameWorldServer serverLogic)
        {
            _serverLogic = serverLogic;
            _antilagSystem = new AntilagSystem(60, ServerLogic.MaxPlayers);
            _players = new ServerPlayer[ServerLogic.MaxPlayers];
            PlayerStates = new PlayerState[ServerLogic.MaxPlayers];
        }

        public bool EnableAntilag(ServerPlayer forPlayer)
        {
            return _antilagSystem.TryApplyAntilag(_players, _serverLogic.Tick, forPlayer.AssociatedPeer.Id);
        }

        public void DisableAntilag()
        {
            _antilagSystem.RevertAntilag(_players);            
        }

        public override IEnumerator<BasePlayer> GetEnumerator()
        {
            int i = 0;
            while (i < _playersCount)
            {
                yield return _players[i];
                i++;
            }
        }

        public override void OnShoot(BasePlayer from, Vector2 to, BasePlayer hit)
        {
            var serverPlayer = (ServerPlayer) from;
            ShootPacket sp = new ShootPacket
            {
                FromPlayer = serverPlayer.Id,
                CommandId = serverPlayer.LastProcessedCommandId,
                ServerTick = _serverLogic.Tick,
                Hit = to
            };
            _serverLogic.SendShoot(ref sp);
        }

        public void AddPlayer(ServerPlayer player)
        {
            _players[_playersCount] = player;
            _playersCount++;
        }

        public override void LogicUpdate()
        {
            for (int i = 0; i < _playersCount; i++)
            {
                var p = _players[i];
                p.Update(LogicTimer.FixedDelta);
                PlayerStates[i] = p.NetworkState;
            }
        }
    }
}