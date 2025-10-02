using AutoMapper;
using Core;
using Core.DTOs;
using Infrastructure.Authenticate;
using Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IJWTManager _jWTManager;
        public UserService(IUnitOfWork unitOfWork,IMapper mapper, IJWTManager jWTManager)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _jWTManager = jWTManager;
        }

        public async Task<ResponseCreateUser> CreateUser(UserDto userDto)
        {
            var userExist = await _unitOfWork.User.GetUserByEmail(userDto.Email);
            if (userExist == null)
            {
                User user = _mapper.Map<User>(userDto);
                user.Password = Encrypt.EncryptPassword(user.Password);
                await _unitOfWork.User.Add(user);
                _unitOfWork.Save();
                return new ResponseCreateUser {IdUser=user.Id, Success=true, Message = "Usuário criado com sucesso!" };
            }
            else
            {
                return new ResponseCreateUser
                {
                    Success = false,
                    Message = "Usuário já cadastrado!"
                };
            }
        }

        public async Task<bool> DeleteUser(int UserId)
        {
            if (UserId > 0)
            {
                var UserDetail = await _unitOfWork.User.GetById(UserId);
                if (UserDetail != null)
                {
                    _unitOfWork.User.Delete(UserDetail);
                    var result = _unitOfWork.Save();
                    return result > 0;
                }
            }
            return false;
        }

        public async Task<User?> GetUserById(int UserId)
        {
            if (UserId > 0)
            {
                return await _unitOfWork.User.GetUserById(UserId);
            }
            return null;
        }

        public async Task<string> UpdateUser(UserDto UserParam)
        {
            if (UserParam != null)
            {
                User user = await _unitOfWork.User.GetById(UserParam.Id);
                if (user != null)
                {
                    user.Name = UserParam.Name;
                    user.Email = UserParam.Email;

                    _unitOfWork.User.Update(user);
                    var result = _unitOfWork.Save();
                    return "Cadastro alterado com sucesso!";
                }
            }
            return "Não foi possível alterar o cadastro!";
        }
        public async Task<ResponseAuth?> Auth(UserAuthenticate userAuthenticate)
        {
            TokenJWT? token = null;
            var user = await  _unitOfWork.User .GetUserByEmail(userAuthenticate.Email);
            if (user != null && Encrypt.EncryptPassword(userAuthenticate.Password) == user.Password)
                token = await _jWTManager.Authenticate(user);

            if (user == null)
            {
                return new ResponseAuth { Message = "Usuário não encontrado!" };
            }else if(user!=null && token == null)
            {
                return new ResponseAuth { Message = "Senha invalida!" };
            }
            else
            {
                return new ResponseAuth { Message = "success",TokenJWT=token };
            }

        }
        public async Task<SportsCenterUsersDto?> GetUserSportCenterById(int userId)
        {
            return await _unitOfWork.SportsCenterUsers.GetByIdUser(userId);
        }
        public async Task  UpdateUserImage(int id, string uniqueFileName)
        {
            var user= await _unitOfWork.User.GetById(id);
            if (user != null)
            {
                user.UniqueFileName = uniqueFileName;
                _unitOfWork.User.Update(user);
                _unitOfWork.Save();
            }
        }
        public async Task<User?> GetUserByEmail(string email)
        {
            return await _unitOfWork.User.GetUserByEmail(email);
        }
    }

    public interface IUserService
    {
        Task<ResponseCreateUser> CreateUser(UserDto User);
        Task<User?> GetUserById(int UserId);
        Task<string> UpdateUser(UserDto UserParam);
        Task<bool> DeleteUser(int UserId);
        Task<ResponseAuth?> Auth(UserAuthenticate userAuthenticate);
        Task<SportsCenterUsersDto?> GetUserSportCenterById(int userId);
        Task UpdateUserImage(int id, string uniqueFileName);
        Task<User?> GetUserByEmail(string email);
    }
}
