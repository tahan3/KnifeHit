using Source.Scripts.Data.Profile;
using Source.Scripts.Profile;
using Source.Scripts.Save;
using UnityEngine;
using Zenject;

namespace Source.Scripts.View.Profile
{
    public class EditIconWindowHandler
    {
        private EditIconWindow _editIconWindow;
        private ProfileIcon _selectedIcon;

        [Inject] private ProfileStorage _profileStorage;
        [Inject] private IProfileHandler _profileHandler;
        
        public EditIconWindowHandler(EditIconWindow editIconWindow)
        {
            _editIconWindow = editIconWindow;
        }
        
        public void Init()
        {
            if (!_selectedIcon)
            {
                foreach (var key in _profileStorage.icons.GetKeys())
                {
                    if (key != 0)
                    {
                        if (_profileStorage.icons.TryGetValue(key, out var sprite))
                        {
                            var item = Object.Instantiate(_profileStorage.profileIconPrefab,
                                _editIconWindow.itemsParent);
                            item.icon.image.sprite = sprite;
                            item.selectedMark.SetActive(false);

                            if (_profileHandler.Profile.IconID == key)
                            {
                                item.selectedMark.SetActive(true);
                                _selectedIcon = item;
                            }

                            item.icon.onClick.AddListener(() => SelectIcon(item, key));
                        }
                    }
                }

                _editIconWindow.applyButton.onClick.AddListener(OnApplyButtonClick);
            }
        }

        private void SelectIcon(ProfileIcon icon, int iconID)
        {
            if (_selectedIcon)
            {
                _selectedIcon.selectedMark.SetActive(false);
            }

            _selectedIcon = icon;
            _selectedIcon.selectedMark.SetActive(true);

            var data = _profileHandler.Profile;
            data.IconID = iconID;
            _profileHandler.Profile = data;
        }

        private void OnApplyButtonClick()
        {
            _profileHandler.Save();
            
            _editIconWindow.profileWindow.profileEditWindow.Close();
            _editIconWindow.profileWindow.mainWindow.Open();
        }
    }
}