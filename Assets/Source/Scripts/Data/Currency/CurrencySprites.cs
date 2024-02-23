using Source.Scripts.Currency;
using UnityEngine;

namespace Source.Scripts.Data.Currency
{
    [CreateAssetMenu(fileName = "CurrencySprites", menuName = "CurrencySprites", order = 0)]
    public class CurrencySprites : KeyValueStorage<CurrencyType, Sprite>
    {
    }
}