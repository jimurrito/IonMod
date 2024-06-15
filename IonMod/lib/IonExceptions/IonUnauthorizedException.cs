namespace IonMod
{
    //
    public class IonUnauthorizedException : Exception
    {
        public IonUnauthorizedException() {}
        public IonUnauthorizedException(string message) : base(message) {}
        public IonUnauthorizedException(string message, Exception inner) : base(message, inner){}
    }
}