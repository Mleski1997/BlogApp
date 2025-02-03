using Microsoft.Identity.Client;

namespace BlogApp.Exceptions
{
    public class NotFoundPostByIdException : CustomExcpetion
    {
        public Guid Id { get;}
        public NotFoundPostByIdException() : base("Post with ID not found")
        {
        }

        public NotFoundPostByIdException(Guid id) : base($"Post with ID :{id} not found ")
        {
            Id = id; 
            
        }
    }
}
