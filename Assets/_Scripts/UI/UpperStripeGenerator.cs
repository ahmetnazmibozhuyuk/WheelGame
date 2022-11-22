using DG.Tweening;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Wheel.Managers;

namespace Wheel.UI
{
    public class UpperStripeGenerator : MonoBehaviour
    {
        [SerializeField] private bool validate;

        private List<RectTransform> _rectTransformList = new List<RectTransform>();
        [SerializeField] private GameObject regularStripeElement;
        [SerializeField] private GameObject safeStripeElement;
        [SerializeField] private GameObject superStripeElement;


        private readonly int _initialElementAmount = 12;

        [SerializeField] private float singleSegmentWidth = 100;
        [SerializeField] private float rectHeight = 120;

        [SerializeField]private RectTransform rectTransform;

        private float _currentXPosition;

        private void Awake()
        {
            InitializeElements();
            ResizeStripe();
        }
        private void InitializeElements()
        {
            _rectTransformList = new List<RectTransform>();

            for (int i = 1; i < _initialElementAmount; i++)
            {
                AddElement(i);

            }
            ResizeStripe();
            rectTransform.anchoredPosition = new Vector2(rectTransform.sizeDelta.x / 2- singleSegmentWidth / 2, 0);
        }
        private void ResizeStripe()
        {
            rectTransform.sizeDelta = new Vector2(singleSegmentWidth * transform.childCount, rectHeight);
            rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x + singleSegmentWidth / 2,0);
        }
        private void AddElement(int elementChildIndex)
        {
            RectTransform spawnedObjectRectTransform = Instantiate(ObjectToSpawn(elementChildIndex), transform).GetComponent<RectTransform>();
            _rectTransformList.Add(spawnedObjectRectTransform);
            spawnedObjectRectTransform.GetChild(0).GetComponent<TextMeshProUGUI>().SetText((_rectTransformList.Count).ToString());
        }
        private GameObject ObjectToSpawn(int childIndex)
        {
            if (childIndex % GameManager.Instance.SuperRoundIndex== 0)
                return superStripeElement;
            if (childIndex % GameManager.Instance.SafeRoundIndex == 0)
                return safeStripeElement;
            return regularStripeElement;
        }
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                MoveStripe();
            }
        }

        public void MoveStripe()
        {
            AddElement(_rectTransformList.Count + 1);
            ResizeStripe();
            _currentXPosition = rectTransform.anchoredPosition.x - singleSegmentWidth;
            rectTransform.DOAnchorPosX(_currentXPosition, 0.5f);
        }
    }
}
