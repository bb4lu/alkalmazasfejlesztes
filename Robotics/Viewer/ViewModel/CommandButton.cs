﻿using System;
using RobotBrain;
using RobotBrain.State;

namespace Viewer.ViewModel
{
    public class CommandButton : System.Windows.Input.ICommand
    {
        private readonly WallsAndLinesDemoBrain brain;
        private readonly IState targetState;
        public string Title { get; private set; }

        public CommandButton(string title, WallsAndLinesDemoBrain brain, IState targetState)
        {
            this.Title = title;
            this.brain = brain;
            this.targetState = targetState;
        }

        public event EventHandler CanExecuteChanged
        {
            // This event never happens as CanExecute is constant.
            add { }
            remove { }
        }

        public bool CanExecute(object parameter) => true;

        public void Execute(object parameter)
        {
            brain.CurrentState = targetState;
        }
    }
}
