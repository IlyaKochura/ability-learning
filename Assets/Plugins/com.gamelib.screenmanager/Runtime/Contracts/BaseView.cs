using UnityEngine;

namespace ScreenManager.Runtime.Contracts
{
    public abstract class BaseView : MonoBehaviour
    {
        public void Show()
        {
            if (gameObject.activeSelf)
            {
                return;
            }
            
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            if (!gameObject.activeSelf)
            {
                return;
            }
            
            gameObject.SetActive(false);
        }
    }
}