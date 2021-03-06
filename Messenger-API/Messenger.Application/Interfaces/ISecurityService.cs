﻿using System.Threading.Tasks;

namespace Messenger.Application.Interfaces
{
    public interface ISecurityService
    {
        Task<string> HashPassword(string password);
        Task<bool> VerifyPassword(string password, string hash);
        
    }
}