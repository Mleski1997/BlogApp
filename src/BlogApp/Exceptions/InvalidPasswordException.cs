namespace BlogApp.Exceptions
{
    public class InvalidPasswordException : CustomExcpetion
    {
        public InvalidPasswordException() : base("Invalid Password")
        {
        }
    }
}
