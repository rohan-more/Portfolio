namespace AISandbox
{
    public class DoNothing : State
    {
        private const string _name = "NoProgressiveState";
        public override string Name
        {
            get
            {
                return _name;
            }
        }
    }
}
