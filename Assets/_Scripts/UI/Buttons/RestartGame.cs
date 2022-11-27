using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Wheel.UI
{
    public class RestartGame : MonoBehaviour
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
            _button.onClick.AddListener(Restart);

        }
        private void Restart()
        {
            SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
        }


    }
}
