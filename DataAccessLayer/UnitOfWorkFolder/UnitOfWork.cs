﻿using DataAccessLayer.Data;
using DataAccessLayer.Repository;
using DomainLayer.Model;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.UnitOfWorkFolder
{
    public class UnitOfWork(ApplicationDbContext applicationDbContext, UserManager<User> userManager) : IUnitOfWork
    {
        private readonly ApplicationDbContext _applicationDbContext = applicationDbContext;
        private readonly UserManager<User> _userManager = userManager;
        private UserRepository? _userRepository;
        private PostRepository? _postRepository;
        private CommentRepository? _commentRepository;
        private LikeRepository? _likeRepository;

        private FollowerRepository? _followerRepository;
        public UserRepository userRepository => _userRepository ??= new UserRepository(_userManager);

        public PostRepository postRepository => _postRepository ??= new PostRepository(_applicationDbContext);

        public CommentRepository commentRepository => _commentRepository ??= new CommentRepository(_applicationDbContext);

        public LikeRepository likeRepository => _likeRepository ??= new LikeRepository(_applicationDbContext);

        public FollowerRepository followerRepository => _followerRepository??= new FollowerRepository(_applicationDbContext);
    }
}
