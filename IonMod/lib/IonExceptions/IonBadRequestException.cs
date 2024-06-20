namespace IonMod
{
    //
    public class IonBadRequestException : Exception
    {
        public IonBadRequestException() {}
        public IonBadRequestException(string message) : base(message) {}
        public IonBadRequestException(string message, Exception inner) : base(message, inner){}
    }
}