using System.Collections;
using System.Collections.Generic;
using ATG.LevelControl;
using UnityEngine;
using Zenject;

namespace TutorialLogic
{
    public class TutorialService : MonoBehaviour
    {
        [Inject] private ILevelStatus _levelStatus;
        
        public void Init(Queue<ICommand> commands)
        {
            _levelStatus.OnLevelStart += (sender, args) => 
                StartCoroutine(SetCommands(commands));
        }

        private IEnumerator SetCommands(Queue<ICommand> commands)
        {
            while (commands.Count > 0)
            {
                var curCommand = commands.Dequeue();
                curCommand.Execute();

                yield return new WaitUntil(() => curCommand.Status == CommandStatus.End);
            }
        }
    }
}
