using UnityEngine;

namespace GameSession.UserInterface
{
    public abstract class UserInterfaceElement : MonoBehaviour
    {
        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
        
        public abstract void Init();

        public abstract void OnStartGame();
    }
}