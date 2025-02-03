using Microsoft.Identity.Client;

namespace BlogApp.Exceptions
{
    public class NotFoundPostByIdException : CustomExcpetion
    {
        public int Id { get;}
        public NotFoundPostByIdException() : base("Post with ID not found")
        {
        }

        public NotFoundPostByIdException(int id) : base($"Post with ID :{id} not found ")
        {
            Id = id; 
            
        }
    }
}
