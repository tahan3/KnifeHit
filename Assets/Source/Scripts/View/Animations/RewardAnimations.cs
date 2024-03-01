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

        public async UniTask Animate(DailyRewardType type, Vector2 from, 
            Action perItemDeliveredAction = null,
            Action onEndAnimation = null,
            int count = 7,
            float moveDuration = 0.2f, 
            float perItemDelay = 1f)
        {
            if (cachedPositions.TryGetValue(type, out var rectTransform))
            {
                var pointToTravel = rectTransform.position;
                float range = 100f;
                float delay = perItemDelay / count;
                
                UniTask lastItem = default;
                
                for (int i = 0; i < count; i++)
                {
                    var item = _items[type].GetItem();
                    item.rectTransform.anchoredPosition = new Vector2(from.x + Random.Range(-range, range), from.y + Random.Range(-range, range));
                    item.rectTransform.localScale = Vector3.zero;
                    item.gameObject.SetActive(true);

                    await item.rectTransform.DOScale(Vector3.one, delay).AsyncWaitForCompletion();
                    
                    var tween = item.rectTransform.DOAnchorPos(pointToTravel, moveDuration).SetEase(Ease.InExpo);
                    lastItem = tween.AsyncWaitForCompletion().AsUniTask();
                    
                    tween.onComplete += () =>
                    {
                        rectTransform.DOPunchScale(Vector3.one * 1.25f, delay, 1);
                        perItemDeliveredAction?.Invoke();
                        item.gameObject.SetActive(false);
                    };
                }

                await lastItem;

                rectTransform.DOKill();
                rectTransform.localScale = Vector3.one;
                
                onEndAnimation?.Invoke();
            }
        }
    }
}