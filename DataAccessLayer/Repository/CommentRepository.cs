using DataAccessLayer.Data;
using DataAccessLayer.IRepository;
using DomainLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repository
{
    public class CommentRepository : IComment
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public CommentRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        //functiont to create a comment
        public Comment Create(Comment comment)
        {
            _applicationDbContext.Add(comment);
            _applicationDbContext.SaveChanges();

            return comment;
        }

        //function to delete a comment
        public void Delete(Comment comment)
        {
            _applicationDbContext.Remove(comment);
            _applicationDbContext.SaveChanges();
        }

        //function to get a single comment
        public Comment? Get(int id)
        {
            Comment? comment = _applicationDbContext.Comments.Find(id);

            return comment;
        }

        //function to get all comments
        public List<Comment> GetAll()
        {
            return _applicationDbContext.Comments.ToList();
        }

        //function to update a comment
        public Comment? Update(Comment comment)
        {
            ///
            
            Comment? existingComment = _applicationDbContext.Comments.Find(comment.Id);

            if (existingComment == null)
            {
                return null; // Or throw an exception if appropriate
            }
            existingComment.Content = comment.Content;

            _applicationDbContext.Comments.Update(existingComment);
            _applicationDbContext.SaveChanges();

///
        /// <summary>
        /// This method will be used to update the Comment with the provided Id, 
        /// it will also check if the provided comment exists in the database, 
        /// and if it does, it will update the content.
        /// </summary>
        /// <param name="id"></param>
            return existingComment;
 
        }
    }
}
