using System.Collections;
using System.Collections.Generic;
using GaGame;

public class StateMachine
{
    private enum Command
    {
        Pauze
    }

    private static State playState  = new PlayState();
    private static State pauzeState = new PauzeState();

    class StateTransition
    {
        readonly State CurrentState;
        readonly Command Command;

        public StateTransition(State currentState, Command command)
        {
            CurrentState = currentState;
            Command = command;
        }

        public override int GetHashCode()
        {
            return 17 + 31 * CurrentState.GetHashCode() + 31 * Command.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            StateTransition other = obj as StateTransition;
            return other != null && this.CurrentState == other.CurrentState && this.Command == other.Command;
        }
    }

    public State CurrentState { get; private set; }
    private Dictionary<StateTransition, State> transitions;

    public StateMachine()
    {
        CurrentState = pauzeState;
        CurrentState.Activate();

        transitions = new Dictionary<StateTransition, State>
        {
            { new StateTransition(playState,  Command.Pauze), pauzeState },
            { new StateTransition(pauzeState, Command.Pauze), playState  },
        };
    }

    private void MoveNext(Command command)
    {
        StateTransition transition = new StateTransition(CurrentState, command);
        State nextState = null;

        if (!transitions.TryGetValue(transition, out nextState))
        {
            System.Console.WriteLine("Invalid Transition: " + CurrentState + " -> " + command);
            return;
        }

        CurrentState.DeActivate();
        CurrentState = nextState;
        CurrentState.Activate();
    }

    public void Pauze()
    {
        MoveNext(Command.Pauze);
    }
}
