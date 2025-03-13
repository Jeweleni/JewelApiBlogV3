using DataAccessLayer.IRepository;
using DomainLayer.Model;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repository
{
    public class UserRepository : IUser
    {
        private readonly UserManager<User> _userManager;

        public UserRepository(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        //funciton to create a user
        public async Task<User> Create(User user, string password)
        {
            IdentityResult result = await _userManager.CreateAsync(user, password);

            if (!result.Succeeded)
            {
                throw new InvalidOperationException("User creation failed.");
            }

            return user;
        }


        //function to delete a user
        // public  async Task<bool> Delete(User user)
        // {
        //     IdentityResult result = await _userManager.DeleteAsync(user);

        //     if (result.Succeeded)
        //     {
        //         return true;
        //     }

        //     return false;
        // }

        public async Task<bool> Delete(User user)
        {
            IdentityResult result = await _userManager.DeleteAsync(user);
            return result.Succeeded;
        }
        //function to get a user
        public async Task<User?> Get(string id)
        {
            return await _userManager.FindByIdAsync(id);
        }

        //funciton to get all users
        public List<User> GetAll()
        {
            return _userManager.Users.ToList();
        }

        //function to update a user
        // public async Task<User?> Update(User user)
        // {
        //     IdentityResult result = await _userManager.UpdateAsync(user);

        //     if (result.Succeeded)
        //     {
        //         return null;
        //     }

        //     return user;
        // }


         public async Task<User?> Update(User user)
        {
            IdentityResult result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                throw new InvalidOperationException("User update failed: " + string.Join(", ", result.Errors.Select(e => e.Description)));
            }

            return user; // Return the updated user
        }
    }

}
