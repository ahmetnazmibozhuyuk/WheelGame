using UnityEngine;
using UnityEngine.UI;
using Wheel.Managers;

namespace Wheel.UI
{
    public class WithdrawButton : MonoBehaviour
    {


        private Button _button;
        private void OnValidate()
        {
            AssignButtonFunctionality();

        }
        private void AssignButtonFunctionality()
        {

            _button = GetComponent<Button>();
            _button.onClick.RemoveAllListeners();
            _button.onClick.AddListener(ContinueGame);

        }
        private void ContinueGame()
        {
            //GameManager.Instance.
            GameStateHandler.ChangeState(GameState.GameWon);
        }



        private void OnEnable()
        {
            GameStateHandler.OnSpinningState += DeactivateButton;
            GameStateHandler.OnGameWonState += DeactivateButton;
            GameStateHandler.OnSpinningFinishedState += ActivateButton;
        }
        private void OnDisable()
        {
            GameStateHandler.OnSpinningState -= DeactivateButton;
            GameStateHandler.OnGameWonState -= DeactivateButton;
            GameStateHandler.OnSpinningFinishedState -= ActivateButton;
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
