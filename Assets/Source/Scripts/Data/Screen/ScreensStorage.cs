using Source.Scripts.View;
using UnityEngine;

namespace Source.Scripts.Data.Screen
{
    [CreateAssetMenu(fileName = "ScreensStorage", menuName = "ScreensStorage", order = 0)]
    public class ScreensStorage : KeyValueStorage<WindowType, AWindow>
    {
    }
}