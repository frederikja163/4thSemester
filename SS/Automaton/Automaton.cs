using System.Collections.ObjectModel;

namespace Automaton;

class Automaton <TState, TAlphabet>
    where TAlphabet : notnull
    where TState : notnull
{
    
    private readonly IReadOnlyDictionary<TState, Dictionary<TAlphabet, TState>> _transitionCache;
    public TState[] States { get; init; }
    public IReadOnlyCollection<TAlphabet> Alphabet { get; init; }
    public HashSet<TState> AcceptStates { get; init; }
    public TState InitialState { get; init; }
    public readonly Func<TState, TAlphabet, TState>? Transition = null;

    public Automaton(TState[] states, TAlphabet[] alphabet, Func<TState, TAlphabet, TState> transition, TState initial,
        TState[] acceptStates)
    {
        AcceptStates = acceptStates.ToHashSet();
        States = states;
        _transitionCache = new ReadOnlyDictionary<TState, Dictionary<TAlphabet, TState>>(
            states.ToDictionary(
                s => s,
                _ => new Dictionary<TAlphabet, TState>()));
        
        Alphabet = alphabet;
        Transition = transition;
        InitialState = initial;
    }

    public Automaton(TState[] states, TAlphabet[] alphabet, IReadOnlyDictionary<TState, Dictionary<TAlphabet, TState>> transition, TState initial,
        TState[] acceptStates)
    {
        AcceptStates = acceptStates.ToHashSet();
        States = states;
        _transitionCache = transition;
        Transition = (state, symbol) => _transitionCache[state][symbol];
        
        Alphabet = alphabet;
        InitialState = initial;
    }

    private TState GetNextState(TState state, TAlphabet next)
    {
        if (!_transitionCache[state].TryGetValue(next, out TState? nextState))
        {
            nextState = Transition.Invoke(state, next);
            _transitionCache[state][next] = nextState;
        }

        return nextState;
    }

    public IEnumerable<TState> Visit(params TAlphabet[] str)
    {
        TState state = InitialState;
        yield return state;
        foreach (TAlphabet next in str)
        {
            state = GetNextState(state, next);
            yield return state;
        }
    }

    public bool Accepts(params TAlphabet[] str)
    {
        return AcceptStates.Contains(Visit(str).Last());
    }
}