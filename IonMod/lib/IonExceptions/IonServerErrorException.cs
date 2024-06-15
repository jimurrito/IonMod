namespace IonMod
{
    //
    public class IonServerErrorException : Exception
    {
        public IonServerErrorException() {}
        public IonServerErrorException(string message) : base(message) {}
        public IonServerErrorException(string message, Exception inner) : base(message, inner){}
    }
}