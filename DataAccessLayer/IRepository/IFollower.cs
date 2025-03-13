using DomainLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.IRepository
{
    public interface IFollower
    {
        /// <summary>
        /// Create a Follower
        /// </summary>
        /// <param name="Follower"></param>
        /// <returns>Object of Follower</returns>
        Follower Create(Follower Follower);

        /// <summary>
        /// Get all Followers
        /// </summary>
        /// <returns>List of Follower</returns>
        List<Follower> GetAll();

        /// <summary>
        /// Get a single Follower
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Object of Follower</returns>
        Follower? Get(int id);

        /// <summary>
        /// Update Follower
        /// </summary>
        /// <param name="Follower"></param>
        /// <returns>Object of Follower</returns>
        //Follower? Update(Follower Follower);

        /// <summary>
        /// Delete Follower
        /// </summary>
        /// <param name="Follower"></param>
        void Delete(Follower Follower);
        
    }
}
