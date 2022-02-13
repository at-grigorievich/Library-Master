using System;

namespace TutorialLogic
{
    public enum CommandStatus
    {
      None,
      Continue,
      End
    }
    
    public interface ICommand
    {
        CommandStatus Status { get; }
        void Execute();
    }
}