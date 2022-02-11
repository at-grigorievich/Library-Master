using System;
using ATG.LevelControl;
using UnityEngine;
using Zenject;

namespace UILogic
{
    public class UIControlSystem : MonoBehaviour
    {
        [Inject] private ILevelStatus _levelStatus;
        [Inject] private ILevelSystem _levelSystem;
        [Inject] private IPanel[] _panels;

        private void Start()
        {
            ShowPanel<LobbyPanel>();

            _levelStatus.OnLevelStart += (sender, args) => ShowPanel<GamePanel>();
            
            _levelStatus.OnCompleteLevel += (sender, args) =>
            {
                GetPanel<DebriefPanel>().DebriefType = DebriefType.CompleteDebrief;
                ShowPanel<DebriefPanel>();
            };

            _levelStatus.OnFailedLevel += (sender, args) =>
            {
                GetPanel<DebriefPanel>().DebriefType = DebriefType.FailedDebrief;
                ShowPanel<DebriefPanel>();
            };
        }

        [ContextMenu("show panel")]
        public void ShowLobby() => ShowPanel<LobbyPanel>();

        [ContextMenu("hide panel")]
        public void HideLobby() => ShowPanel<GamePanel>();

        public T GetPanel<T>() where T : IPanel
        {
            foreach (var panel in _panels)
            {
                if (panel is T)
                {
                    return (T)panel;
                }
            }

            throw new ArgumentException($"Cant find panel with type {typeof(T)} ");
        }
        
        public void ShowPanel<T>() where T : IPanel
        {
            foreach (var panel in _panels)
            {
                if (panel is T)
                {
                    panel.Show();
                }
                else
                {
                    panel.Hide();
                }
            }
        }
    }
}
