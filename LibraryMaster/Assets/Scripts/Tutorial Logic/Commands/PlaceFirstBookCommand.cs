using System.Collections;
using ATG.LevelControl;
using UnityEngine;

namespace TutorialLogic.Commands
{
    public class PlaceFirstBookCommand: ICommand
    {
        private readonly TutorialElement _tutorialElement;
        private readonly ShelfBlock _shelf;

        private int _defaultBookCount;
        
        public CommandStatus Status { get; private set; }
        
        public PlaceFirstBookCommand(ShelfBlock shelf,TutorialElement element )
        {
            _tutorialElement = element;
            _shelf = shelf;

            _defaultBookCount = _shelf.BooksOnShelf;
        }

        
        public void Execute()
        {
            var visualize = GameObject.Instantiate(_tutorialElement);
            
            Status = CommandStatus.Continue;

            visualize.StartCoroutine(WaitToComplete());
            
            IEnumerator WaitToComplete()
            {
                yield return new WaitUntil(() => _shelf.BooksOnShelf != _defaultBookCount);
                Status = CommandStatus.End;
                
                GameObject.Destroy(visualize.gameObject);
            }
        }
    }
}