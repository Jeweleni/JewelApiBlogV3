using DomainLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.IServices
{
    public interface IFollowerService
    {
        /// <summary>
        /// Create Follower
        /// </summary>
        /// <param name="follower"></param>
        /// <returns>Object of Follower</returns>
        Follower? CreateFollower(Follower follower, out string message);

        /// <summary>
        /// Get all Followers
        /// </summary>
        /// <returns>Object of Follower</returns>
        List<Follower> GetAllFollower();

        /// <summary>
        /// Get a single Follower
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Object of Follower</returns>
        Follower? GetFollower(int id);

        /// <summary>
        /// Delete Follower
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Boolean true or false</returns>
        bool DeleteFollower(int id, out string message);

        /// <summary>
        /// Update Follower
        /// </summary>
        /// <param name="follower"></param>
        /// <param name="message"></param>
        /// <returns>Object of Follower</returns>
        //Follower? UpdateFollower(Follower Follower, out string message);
    }
}
