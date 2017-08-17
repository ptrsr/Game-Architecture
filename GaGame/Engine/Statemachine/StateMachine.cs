using System.Collections;
using System.Collections.Generic;
using GaGame;

public class StateMachine
{
    public enum Command
    {
        Pauze,
        Score,
        Reset,
    }

    private static State playState = new PlayState();
    private static State scoreState = new ScoreState();
    private static State pauzeState = new PauzeState();
    private static State resetState = new ResetState();

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

    Dictionary<StateTransition, State> transitions;
    public State CurrentState { get; private set; }

    public StateMachine(State startState)
    {
        CurrentState = startState;

        transitions = new Dictionary<StateTransition, State>
        {
            { new StateTransition(playState,  Command.Pauze), pauzeState },
            { new StateTransition(pauzeState, Command.Pauze), playState  },

            { new StateTransition(playState,  Command.Reset), resetState },
            { new StateTransition(pauzeState, Command.Reset), resetState },

            { new StateTransition(resetState,  Command.Reset), playState },
        };
    }

    private State GetNext(Command command)
    {
        StateTransition transition = new StateTransition(CurrentState, command);
        State NextState = null;

        if (!transitions.TryGetValue(transition, out NextState))
            System.Console.WriteLine("Invalid Transition: " + CurrentState + " -> " + command);
        return NextState;
    }

    public void MoveNext(Command command)
    {
        State nextState = GetNext(command);

        if (nextState != null)
        {
            CurrentState.DeActivate();
            CurrentState = nextState;
            CurrentState.Activate();
        }
    }
}
