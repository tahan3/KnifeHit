using Source.Scripts.View;
using UnityEngine;

namespace Source.Scripts.Data.Screen
{
    [CreateAssetMenu(fileName = "BGScreens", menuName = "BGScreens", order = 0)]
    public class BGScreens : KeyValueStorage<BGWindowType, AWindow>
    {
    }
}