using UnityEngine;

namespace Source.Scripts.Tutorial
{
    public class TutorialStage : MonoBehaviour
    {
        [SerializeField] private byte _stageNumber;

        private bool _isActive;

        public bool IsActive => _isActive;
        public byte StageNumber => _stageNumber;

        private void SetActiveTutor(bool mod)
        {
            gameObject.SetActive(mod);
            _isActive = mod;
        }

        public void ActivateTutor()
        {
            SetActiveTutor(true);
        }

        public void DeactivateTutor()
        {
            SetActiveTutor(false);
        }
    }
}
