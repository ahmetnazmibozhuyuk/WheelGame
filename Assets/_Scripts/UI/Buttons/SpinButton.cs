using UnityEngine;
using UnityEngine.UI;
using Wheel.Managers;

namespace Wheel.UI
{
    public class SpinButton : MonoBehaviour
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
            _button.onClick.AddListener(SpinWheel);

        }
        private void SpinWheel()
        {
            GameManager.Instance.SpinWheel();
        }
        private void OnEnable()
        {
            GameStateHandler.OnSpinningState += DeactivateButton;
            GameStateHandler.OnGameWonState += DeactivateButton;
            GameStateHandler.OnGameAwaitingStartState += ActivateButton;
        }
        private void OnDisable()
        {
            GameStateHandler.OnSpinningState -= DeactivateButton;
            GameStateHandler.OnGameWonState -= DeactivateButton;
            GameStateHandler.OnGameAwaitingStartState -= ActivateButton;
        }
        private void DeactivateButton()
        {
            _button.interactable = false;
        }
        private void ActivateButton()
        {
            _button.interactable = true;
        }
    }
}
