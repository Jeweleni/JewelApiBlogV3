using AutoMapper;
using BusinessLogicLayer.IServices;
using DataAccessLayer.UnitOfWorkFolder;
using DomainLayer.Model;
using DomainLayer.UserDto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using BusinessLogicLayer.Interface;

namespace BusinessLogicLayer.Service
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _imapper;
        private readonly ITokenGenerator _tokenGenerator;
        private readonly UserManager<User> _userManager;

        public UserService(IUnitOfWork unitOfWork, IMapper imapper, ITokenGenerator tokenGenerator, UserManager<User> userManager)
        {
            _unitOfWork = unitOfWork;
            _imapper = imapper;
            _tokenGenerator = tokenGenerator;
            _userManager = userManager;
        }

        public async Task<User?> CreateUser(CreateUserDto userDto)
        {
            if (string.IsNullOrEmpty(userDto.FirstName) ||
                string.IsNullOrEmpty(userDto.LastName) ||
                string.IsNullOrEmpty(userDto.Email) ||
                string.IsNullOrEmpty(userDto.Password))
            {
                return null;
            }

            var user = _imapper.Map<User>(userDto);
            user.UserName = userDto.Email;

            var createdUser = await _unitOfWork.userRepository.Create(user, userDto.Password);
            return createdUser;
        }

        public List<User> GetAllUsers() => _unitOfWork.userRepository.GetAll();

        public async Task<User?> GetUser(string id) => await _unitOfWork.userRepository.Get(id);

        public async Task<User?> UpdateUser(User user)
        {
            var existingUser = await _unitOfWork.userRepository.Get(user.Id);
            if (existingUser == null) return null;

            existingUser.FirstName = user.FirstName;
            existingUser.LastName = user.LastName;
            existingUser.Email = user.Email;

            await _unitOfWork.userRepository.Update(existingUser);
            return existingUser;
        }

        public async Task<bool> DeleteUser(string id)
        {
            var user = await _unitOfWork.userRepository.Get(id);
            if (user == null) return false;

            await _unitOfWork.userRepository.Delete(user);
            return true;
        }

        // ✅ Implement LoginUser
        public async Task<string?> LoginUser(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null || string.IsNullOrEmpty(user.Email)) return null;

            var isPasswordValid = await _userManager.CheckPasswordAsync(user, password);
            if (!isPasswordValid) return null;

            // ✅ Pass both userId and email to GenerateToken
            return _tokenGenerator.GenerateToken(user.Id, user.Email);
        }

    }
}
