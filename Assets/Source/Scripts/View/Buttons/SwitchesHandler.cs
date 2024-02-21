using System.Collections.Generic;

namespace Source.Scripts.View.Buttons
{
    public class SwitchesHandler
    {
        private List<Switch> _switches;

        public SwitchesHandler(List<Switch> switches)
        {
            _switches = switches;
            
            for (var i = 0; i < _switches.Count; i++)
            {
                _switches[i].button.onClick.AddListener(DisableAll);
            }
        }

        private void DisableAll()
        {
            for (var i = 0; i < _switches.Count; i++)
            {
                _switches[i].Disable();
            }
        }
    }
}