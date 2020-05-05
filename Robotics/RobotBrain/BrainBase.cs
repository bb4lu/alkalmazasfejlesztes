﻿using Robot;
using RobotBrain.Command;
using RobotBrain.LogEntry;
using RobotBrain.State;

namespace RobotBrain
{
    public class BrainBase : IBrain
    {
        ICommand currentCommand = null;

        private IState currentState = new IdleState();
        public IState CurrentState 
        {
            get => currentState;
            set
            {
                currentState = value;
                currentState.Brain = this;
                currentState.Enter();
            }
        }

        public IRobot Robot { get; private set; }

        public BrainBase(IRobot robot)
        {
            Robot = robot;
            robot.OnTick += Robot_OnTick;
        }

        #region Handle simulator events
        protected virtual void Robot_OnTick()
        {
            // Note: currentState is changing often. An OnTick event would need
            //  many subscriptions and unsubscriptions.
            currentState.Tick();

            Log(new TickLogEntry());

            if (currentCommand?.IsComplete() ?? false)
                Log(new CommandCompleteLogEntry(currentCommand));
        }
        #endregion

        public void AddCommand(ICommand cmd)
        {
            Log(new GenericLogEntry($"New command received"));
            currentCommand = cmd;
            currentCommand.Brain = this;
            cmd.Execute();
        }

        protected void Log(ILogEntry entry)
        {
            OnLoggedEvent?.Invoke(entry);
        }

        public event OnLoggedEventDelegate OnLoggedEvent;
    }
}
