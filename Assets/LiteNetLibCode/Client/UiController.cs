﻿using Code.Server;
using LiteNetLib;
using UnityEngine;
using UnityEngine.UI;

namespace Code.Client
{
    public class UiController : MonoBehaviour
    {
        [SerializeField] private GameObject _uiObject;
        [SerializeField] private ClientLogic _clientLogic;
        [SerializeField] private ServerLogic _serverLogic;
        [SerializeField] private InputField _ipField;
        [SerializeField] private GameMain m_GameMain;

        private void Awake()
        {
            _ipField.text = NetUtils.GetLocalIp(LocalAddrType.IPv4);
        }

        public void OnHostClick()
        {
            //_serverLogic.StartServer();
            ////_clientLogic.Connect("localhost");
           
            m_GameMain.CreateGameLoop(GameLoopType.Server);
            _clientLogic.Connect(_ipField.text);
            _uiObject.SetActive(false);
        }

        public void OnConnectClick()
        {
            _clientLogic.Connect(_ipField.text);
            _uiObject.SetActive(false);
        }
    }
}
