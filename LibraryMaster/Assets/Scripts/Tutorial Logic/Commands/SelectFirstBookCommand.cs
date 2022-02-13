using System.Collections;
using ATG.LevelControl;
using UnityEngine;

namespace TutorialLogic.Commands
{
    public class SelectFirstBookCommand: ICommand
    {
        private readonly TutorialElement _tutorialElement;
        private readonly ShelfBlock _shelf;

        public CommandStatus Status { get; private set; }
        
        public SelectFirstBookCommand(ShelfBlock shelf,TutorialElement tutorialElement)
        {
            _tutorialElement = tutorialElement;
            _shelf = shelf;
        }

        public void Execute()
        {
            var visualize = GameObject.Instantiate(_tutorialElement);
            
            Status = CommandStatus.Continue;

            visualize.StartCoroutine(WaitToComplete());
            
            IEnumerator WaitToComplete()
            {
                yield return new WaitUntil(() => _shelf.BooksOnShelf == 0);
                Status = CommandStatus.End;
                
                GameObject.Destroy(visualize.gameObject);
            }
        }
    }
}