namespace IonMod
{
    //
    public class IonMissingArgException : Exception
    {
        public IonMissingArgException() {}
        public IonMissingArgException(string message) : base(message) {}
        public IonMissingArgException(string message, Exception inner) : base(message, inner){}
    }
}