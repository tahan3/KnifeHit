using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Source.Scripts.DailyReward;
using Source.Scripts.Pool;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using Random = UnityEngine.Random;

namespace Source.Scripts.View.Animations
{
    public class RewardAnimations
    {
        private Dictionary<DailyRewardType, IPool<Image>> _items;
        private Dictionary<DailyRewardType, RectTransform> cachedPositions;

        public RewardAnimations(DiContainer container, Dictionary<DailyRewardType, Image> prefabs,
            int initPoolSize = 10, Transform parent = null)
        {
            _items = new Dictionary<DailyRewardType, IPool<Image>>();
            cachedPositions = new Dictionary<DailyRewardType, RectTransform>();
            
            foreach (var item in prefabs)
            {
                _items.Add(item.Key, new ComponentsPool<Image>(container, item.Value, initPoolSize, parent));
            }
        }

        public void SetCachedPosition(DailyRewardType type, RectTransform rectTransform)
        {
            cachedPositions[type] = rectTransform;
        }
        
        public async UniTaskVoid Animate(DailyRewardType type, Vector2 from, Vector2 to, float moveDuration = 1f, int count = 7,
            float perItemDelay = 0.25f)
        {
            for (int i = 0; i < count; i++)
            {
                var item = _items[type].GetItem();
                item.rectTransform.anchoredPosition = from;
                item.rectTransform.localScale = Vector3.zero;
                item.gameObject.SetActive(true);

                await item.rectTransform.DOScale(Vector3.one, perItemDelay).AsyncWaitForCompletion();
                item.rectTransform.DOAnchorPos(to, moveDuration).onComplete += () =>
                {
                    item.gameObject.SetActive(false);
                };
            }
        }

        public async UniTaskVoid Animate(DailyRewardType type, Vector2 from, 
            Action perItemDeliveredAction = null,
            Action onEndAnimation = null,
            int count = 7,
            float moveDuration = 0.25f, 
            float perItemDelay = 0.25f)
        {
            if (cachedPositions.TryGetValue(type, out var rectTransform))
            {
                var pointToTravel = rectTransform.position;
                float range = 100f;
                
                for (int i = 0; i < count; i++)
                {
                    var item = _items[type].GetItem();
                    item.rectTransform.anchoredPosition = new Vector2(from.x + Random.Range(-range, range), from.y + Random.Range(-range, range));
                    item.rectTransform.localScale = Vector3.zero;
                    item.gameObject.SetActive(true);

                    await item.rectTransform.DOScale(Vector3.one, perItemDelay).AsyncWaitForCompletion();
                    item.rectTransform.DOAnchorPos(pointToTravel, moveDuration).SetEase(Ease.InExpo).onComplete += () =>
                    {
                        rectTransform.DOPunchScale(Vector3.one * 1.5f, perItemDelay);
                        perItemDeliveredAction?.Invoke();
                        item.gameObject.SetActive(false);
                    };
                }
                
                onEndAnimation?.Invoke();
            }
        }
    }
}