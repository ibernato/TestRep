namespace Marketplace.Infrastructure
{
    public enum State : short
    {
        Active = 0,
        Deleted = 1
    }

    public class ObjectState
    {
        public ObjectState()
        {
            State = Infrastructure.State.Active;
        }

        public State State { get; set; }
    }
}
