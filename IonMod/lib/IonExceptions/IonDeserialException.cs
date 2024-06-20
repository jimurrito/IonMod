namespace IonMod
{
    //
    public class IonDeserialException : Exception
    {
        public IonDeserialException() {}
        public IonDeserialException(string message) : base(message) {}
        public IonDeserialException(string message, Exception inner) : base(message, inner){}
    }
}