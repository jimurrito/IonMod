namespace IonMod
{
    //
    public class IonUninitLoginException : Exception
    {
        public IonUninitLoginException() {}
        public IonUninitLoginException(string message) : base(message) {}
        public IonUninitLoginException(string message, Exception inner) : base(message, inner){}
    }
}