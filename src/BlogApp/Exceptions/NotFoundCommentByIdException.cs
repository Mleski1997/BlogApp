

namespace BlogApp.Exceptions
{
    public class NotFoundCommentByIdException : CustomExcpetion
    {
        public Guid Id { get;}
        public NotFoundCommentByIdException() : base("Comment with this ID not found")
        {
        }
        public NotFoundCommentByIdException(Guid id) : base($"Comment with this ID: {id} not found")
        {
            Id = id;   
        }
    }
}
