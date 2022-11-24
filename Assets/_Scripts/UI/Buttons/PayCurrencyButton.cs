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
        private void AssignButtonFunctionality()
        {

            _button = GetComponent<Button>();
            _button.onClick.RemoveAllListeners();
            _button.onClick.AddListener(ContinueGame);

        }
        private void ContinueGame()
        {
            GameStateHandler.ChangeState(GameState.GameAwaitingStart);
        }
    }
}
