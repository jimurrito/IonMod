namespace IonMod
{
    //
    public class IonUninitException : Exception
    {
        public IonUninitException() {}
        public IonUninitException(string message) : base(message) {}
        public IonUninitException(string message, Exception inner) : base(message, inner){}
    }
}