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
        private void AssignButtonFunctionality()
        {

            _button = GetComponent<Button>();
            _button.onClick.RemoveAllListeners();
            _button.onClick.AddListener(Restart);

        }
        private void Restart()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }


    }
}
