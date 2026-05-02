using System.Collections.Generic;
using UnityEngine;

namespace CommandPattern
{
    public interface ICommand
    {
        bool Execute();
 
        void Undo();
    }
    
    public static class CommandInvoker
    {
        private static readonly Stack<ICommand> _undoStack = new Stack<ICommand>();
        private static readonly Stack<ICommand> _redoStack = new Stack<ICommand>();
        
        public static void ExecuteCommand(ICommand command)
        {
            bool success = command.Execute();

            if (success)
            {
                _undoStack.Push(command);
                _redoStack.Clear();
            }
        }
        
        public static void Undo()
        {
            if (_undoStack.Count == 0)
            {
                return;
            }

            ICommand command = _undoStack.Pop();
            command.Undo();
            _redoStack.Push(command);
        }
        
        public static void Redo()
        {
            if (_redoStack.Count == 0)
            {
                return;
            }

            ICommand command = _redoStack.Pop();
            bool success = command.Execute();

            if (success)
            {
                _undoStack.Push(command);
            }
            else
            {
                _redoStack.Push(command);
            }
        }

        public static int UndoCount => _undoStack.Count;
        public static int RedoCount => _redoStack.Count;
    }
}