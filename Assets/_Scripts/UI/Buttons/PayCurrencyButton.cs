using UnityEngine;
using UnityEngine.UI;
using Wheel.Managers;

namespace Wheel.UI
{
    public class PayCurrencyButton : MonoBehaviour
    {

        private Button _button;
        private void OnValidate()
        {
            AssignButtonFunctionality();
        }
        /*
        OnValidate method works in editor mode. I assign the button functionalities in awake method just 
        in case OnValidate fails.
         */
        private void Awake()
        {
            AssignButtonFunctionality();
        }
        private void AssignButtonFunctionality()
        {
            if (_button != null) return;
            _button = GetComponent<Button>();
            _button.onClick.RemoveAllListeners();
            _button.onClick.AddListener(ContinueGame);

        }
        private void ContinueGame()
        {
            GameManager.Instance.RestartRound();
        }
    }
}
