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
    public class FollowerRepository : IFollower
    {

        private readonly ApplicationDbContext _applicationDbContext;

        public FollowerRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public Follower Create(Follower Follower)
        {
            _applicationDbContext.Add(Follower);
            _applicationDbContext.SaveChanges();

            return Follower;
        }

        public void Delete(Follower Follower)
        {
            _applicationDbContext.Remove(Follower);
            _applicationDbContext.SaveChanges();
        }

        public Follower? Get(int id)
        {
            Follower? Follower = _applicationDbContext.Followers.Find(id);

            return Follower;
        }

        public List<Follower> GetAll()
        {
            return _applicationDbContext.Followers.ToList();
        }

        //public Follower? Update(Follower Follower)
        //{
        //    Follower? existingFollower = _applicationDbContext.Followers.Find(Follower.Id);

        //    existingFollower.
        //}
    }
}
