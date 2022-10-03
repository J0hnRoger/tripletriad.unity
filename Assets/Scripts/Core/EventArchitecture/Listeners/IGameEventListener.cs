namespace TripleTriad.Core.EventArchitecture.Listeners
{
    public interface IGameEventListener<T>
    {
        void OnEventRaise(T value);
    }
}