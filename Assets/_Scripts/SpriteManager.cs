using System.Collections;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.U2D;

namespace Wheel.Managers
{
    public class SpriteManager : Singleton<SpriteManager>
    {
        /*
         I decided to use an addressable sprite atlas to display the images on the wheel. The game waits
        for the async sprite loading phase before switching to game ready phase.
         */

        [SerializeField] private AssetReferenceAtlasedSprite rewardSpriteAddressable;

        private SpriteAtlas _rewardAtlas;
        private AsyncOperationHandle<SpriteAtlas> rewardSpriteLoadOperation;

        protected override void Awake()
        {
            base.Awake();
            StartCoroutine(LoadRewardSprite());
        }
        private void OnDisable()
        {
            UnloadRewardSprite();
        }
        private IEnumerator LoadRewardSprite()
        {
            rewardSpriteLoadOperation = rewardSpriteAddressable.LoadAssetAsync<SpriteAtlas>();
            yield return rewardSpriteLoadOperation;
            _rewardAtlas = rewardSpriteLoadOperation.Result;
            GameManager.Instance.StartGame();
        }
        private void UnloadRewardSprite()
        {
            _rewardAtlas = null;
            Addressables.Release(rewardSpriteLoadOperation);
        }
        public Sprite GetRewardSprite(string spriteName)
        {
            if(_rewardAtlas == null)
            {
                Debug.LogError("Failed to load reward sprite atlas.");
                return null;
            }
            return _rewardAtlas.GetSprite(spriteName);
        }
    }
}
