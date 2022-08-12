using GameSession;
using UnityEngine;

namespace Menu
{
    public class UserInterface : MonoBehaviour, IGameStartListener
    {
        [SerializeField] private UserInterfaceElement[] _userInterfaceElements;
    
        private void Awake()
        {
            EventBus.Subscribe<IGameStartListener>(this);
        
            foreach (var interfaceElement in _userInterfaceElements)
            {
                interfaceElement.Init();
                interfaceElement.Hide();
            }
        }

        private void OnDestroy()
        {
            EventBus.UnSubscribe<IGameStartListener>(this);
        }

        public void OnStartGame()
        {
            foreach (var interfaceElement in _userInterfaceElements)
            {
                interfaceElement.OnStartGame();
                interfaceElement.Show();
            }
        }
    }
}

