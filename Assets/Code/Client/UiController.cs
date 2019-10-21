using LiteNetLib;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Code.Client
{
    public class UiController : MonoBehaviour
    {
        [SerializeField] private GameMain m_GameMain;
        [SerializeField] private GameObject _uiObject;
        [SerializeField] private InputField _ipField;

        private void Awake()
        {
            _ipField.text = NetUtils.GetLocalIp(LocalAddrType.IPv4);
        }

        public void OnHostClick()
        {
            m_GameMain.StartServerLoop();
            //_uiObject.SetActive(false);
            SceneManager.LoadSceneAsync("Game");
        }

        public void OnConnectClick()
        {
            
            m_GameMain.StartClientLoop();
            //_uiObject.SetActive(false);
            SceneManager.LoadSceneAsync("Game");
        }
    }
}
