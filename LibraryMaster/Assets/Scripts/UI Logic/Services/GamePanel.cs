using ATG.PlayerData;
using TMPro;
using UnityEngine;
using Zenject;

namespace UILogic
{
    public class GamePanel : UIPanel
    {
        [SerializeField] private TextMeshProUGUI _rectText;

        [Inject] private PlayerData _playerData;

        public override void Show()
        {
            _rectText.SetText(_playerData.CurrentLevel.ToString());
            
            base.Show();
            
            foreach (var panelElement in elements)
            {
                panelElement.ElementEnable();
            }
        }
        
        public override void Hide()
        {
            foreach (var panelElement in elements)
            {
                panelElement.ElementDisable();
            }
            base.Hide();
        }
    }
}