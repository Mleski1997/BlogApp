namespace BlogApp.Exceptions
{
    public class InvalidUserException : CustomExcpetion
    {
        public string UserName { get; set; }
        public InvalidUserException() : base("User not found")
        {
        }

        public InvalidUserException(string userName) : base($"User with name {userName} not found")
        {
            UserName = userName;
        }
    }
}
