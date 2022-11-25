using UnityEngine;
using Wheel.Managers;
using DG.Tweening;


namespace Wheel.UI
{
    public class FailPanel : MonoBehaviour
    {
        private void OnEnable()
        {
            GameStateHandler.OnGameAwaitingStartState += InitializePanel;
            GameStateHandler.OnGameLostState += Activate;
        }
        private void OnDisable()
        {
            GameStateHandler.OnGameAwaitingStartState -= InitializePanel;
            GameStateHandler.OnGameLostState -= Activate;
        }
        private void InitializePanel()
        {
            transform.localScale = Vector3.zero;
        }
        private void Activate()
        {
            transform.DOScale(Vector3.one, 0.5f);
        }

    }
}
