using System;
using System.Collections.Generic;
using DG.Tweening;
using Source.Scripts.DailyReward;
using Source.Scripts.Pool;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Source.Scripts.View.Animations
{
    public class RewardAnimations
    {
        private Dictionary<DailyRewardType, IPool<Image>> _items;

        public RewardAnimations(DiContainer container, Image prefab)
        {
            _items = new Dictionary<DailyRewardType, IPool<Image>>();
            _items.Add(DailyRewardType.Coin, new ComponentsPool<Image>(container, prefab, 10));
            _items.Add(DailyRewardType.Cash, new ComponentsPool<Image>(container, prefab, 10));
            _items.Add(DailyRewardType.Exp, new ComponentsPool<Image>(container, prefab, 10));
        }
        
        public void Animate(DailyRewardType type, Vector2 from, Vector2 to, float duration = 1f, int count = 7,
            float perItemDelay = 0.25f)
        {
            for (int i = 0; i < count; i++)
            {
                var item = _items[type].GetItem();
                item.rectTransform.anchoredPosition = from;
                item.rectTransform.localScale = Vector3.zero;
                item.gameObject.SetActive(true);

                item.rectTransform.DOScale(Vector3.one, perItemDelay).onComplete += () =>
                    item.rectTransform.DOAnchorPos(to, duration).onComplete += () => item.gameObject.SetActive(false);
            }
        }
    }
}