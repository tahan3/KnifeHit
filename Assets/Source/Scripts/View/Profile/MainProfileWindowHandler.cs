using Source.Scripts.Data.Profile;
using Source.Scripts.Profile;
using UnityEngine;
using Zenject;

namespace Source.Scripts.View.Profile
{
    public class MainProfileWindowHandler
    {
        private MainProfileWindow _mainProfileWindow;

        [Inject] private ProfileStorage _profileStorage;
        [Inject] private IProfileHandler _profileHandler;

        public MainProfileWindowHandler(MainProfileWindow mainProfileWindow)
        {
            _mainProfileWindow = mainProfileWindow;
        }

        public void Init()
        {
            if (_profileStorage.icons.TryGetValue(_profileHandler.Profile.IconID, out var sprite))
            {
                _mainProfileWindow.profileImg.sprite = sprite;
            }

            _mainProfileWindow.nicknameInput.text = _profileHandler.Profile.Nickname;
            
            if (_mainProfileWindow.editButton.onClick.GetPersistentEventCount() <= 0)
            {
                _mainProfileWindow.editButton.onClick.AddListener(OnEditButtonClick);
                _mainProfileWindow.applyButton.onClick.AddListener(OnApplyButtonClick);
                _mainProfileWindow.randomNicknameButton.onClick.AddListener(GenerateRandomNickname);
                _mainProfileWindow.nicknameInput.onValueChanged.AddListener(ChangeApplyButton);
            }
        }

        private void ChangeApplyButton(string name)
        {
            _mainProfileWindow.applyButton.image.sprite = _profileStorage.applyButtonSprites.GetSprite(
                !string.IsNullOrEmpty(name) &&
                !name.Equals(_profileHandler.Profile.Nickname));
        }
        
        private void OnEditButtonClick()
        {
            _mainProfileWindow.profileWindow.mainWindow.Close();
            _mainProfileWindow.profileWindow.profileEditWindow.Open();
        }

        private void OnApplyButtonClick()
        {
            if (!string.IsNullOrEmpty(_mainProfileWindow.nicknameInput.text) &&
                !_mainProfileWindow.nicknameInput.text.Equals(_profileHandler.Profile.Nickname))
            {
                var data = _profileHandler.Profile;
                data.Nickname = _mainProfileWindow.nicknameInput.text;
                _profileHandler.Profile = data;

                _profileHandler.Save();
                _mainProfileWindow.applyButton.image.sprite = _profileStorage.applyButtonSprites.GetSprite(false);
            }
        }

        private void GenerateRandomNickname()
        {
            _mainProfileWindow.nicknameInput.text =
                _profileStorage.randomNicknames[Random.Range(0, _profileStorage.randomNicknames.Count)];
        }
    }
}