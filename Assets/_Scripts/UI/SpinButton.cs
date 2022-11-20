using UnityEngine;
using UnityEngine.UI;
using Wheel.Managers;

namespace Wheel.UI
{
    public class SpinButton : MonoBehaviour
    {
        private Button _button;

        [SerializeField] private int a;

        private void OnValidate()
        {
            AssignButtonFunctionality();

        }
        private void AssignButtonFunctionality()
        {

            _button = GetComponent<Button>();
            _button.onClick.RemoveAllListeners();
            _button.onClick.AddListener(SpinWheel);
            Debug.Log("button functionality assigned");

        }
        private void SpinWheel()
        {
            Debug.Log("deneme");
            GameManager.Instance.SpinWheel();
        }
    }
}
