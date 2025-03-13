using BusinessLogicLayer.IServices;
using DataAccessLayer.IRepository;
using DataAccessLayer.UnitOfWorkFolder;
using DomainLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Service
{
    public class FollowerService : IFollowerService
    {

        private readonly IUnitOfWork _unitOfWork;

        public FollowerService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

       

         public Follower? CreateFollower(Follower follower, out string message)
        {
            //checking if the user enters values
            if (string.IsNullOrEmpty(follower.UserId))
            {
                message = "User can not be empty";
                return null;
            }


            message = "Follower created successfully";
            return _unitOfWork.followerRepository.Create(follower);
        }

        public bool DeleteFollower(int id, out string message)
        {
            //checking if the id is valid
            if (id <= 0)
            {
                message = "Invalid ID";
                return false;
            }

            Follower? Follower = _unitOfWork.followerRepository.Get(id);

            //checking if comment exists
            if (Follower == null)
            {
                message = "Follower not found";
                return false;
            }

            _unitOfWork.followerRepository.Delete(Follower);

            message = "Follower successfully deleted";
            return true;

        }

        public List<Follower> GetAllFollower()
        {
            return _unitOfWork.followerRepository.GetAll();
        }

        public Follower? GetFollower(int id)
        {
            //checking if the id is valid
            if (id <= 0)
            {

                return null;
            }

            Follower? Follower = _unitOfWork.followerRepository.Get(id);

            //checking if comment exists
            if (Follower == null)
            {
                return null;
            }

            return Follower;
        }
    }
}
