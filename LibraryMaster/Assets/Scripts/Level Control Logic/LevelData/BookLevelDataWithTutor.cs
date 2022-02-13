using System.Collections.Generic;
using System.Linq;
using TutorialLogic;
using TutorialLogic.Commands;
using UnityEngine;

namespace ATG.LevelControl
{
    [CreateAssetMenu(fileName = "Level Data", menuName = "Levels/New Books Level with tutor", order = 0)]
    public class BookLevelDataWithTutor: BooksLevelData
    {
        [SerializeField] private TutorialElement[] _tutorialElements;
        public override T[] GetAnsInstantiateLevelBlocks<T>(ICreateLevelBehaviour createLevel)
        {
            var spawnedBlocks = base.GetAnsInstantiateLevelBlocks<T>(createLevel);

            var shelfs = spawnedBlocks.Cast<ShelfBlock>().ToArray();
            if (shelfs != null)
            {
                InitTutorial(shelfs);
            }

            return spawnedBlocks;
        }

        private void InitTutorial(ShelfBlock[] shelfs)
        {
            var ts = FindObjectOfType<TutorialService>();
            if (ts != null)
            {
                Queue<ICommand> commands = new Queue<ICommand>();
                commands.Enqueue(new SelectFirstBookCommand(shelfs.Last(),_tutorialElements[0]));
                commands.Enqueue(new PlaceFirstBookCommand(shelfs.First(),_tutorialElements[1]));
                ts.Init(commands);
            }
        }
    }
}