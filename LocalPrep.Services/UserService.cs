using LocalPrep.Data.Uow;
using LocalPrep.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using LocalPrep.Data.db;

namespace LocalPrep.Services
{
   public class UserService:IUserService
   {
        UnitOfWork uow = new UnitOfWork();




        public UserModel CreateUser(UserEntity userEntity)
        {
            AspNetUser user = new AspNetUser();
            UserModel objUserEntity = new UserModel();
            //string UserId = string.Empty;
            if (string.IsNullOrEmpty(userEntity.Id))
            {
                Guid g = Guid.NewGuid();
                userEntity.Id = g.ToString();
                
            }
            Mapper.CreateMap<UserEntity, AspNetUser>();
            AspNetUser aspNetUser = Mapper.Map<AspNetUser>(userEntity);
            uow.UserRepository.Insert(aspNetUser);
            uow.Save();
            //UserId = userEntity.Id;
            //return UserId;
            Mapper.CreateMap<AspNetUser, UserModel>();
            objUserEntity = Mapper.Map<UserModel>(aspNetUser);
            objUserEntity.Step = 1;


            //UserId = userEntity.Id;
            return objUserEntity;
        }

        public UserModel UpdateUser(UserEntity userEntity)
        {
            AspNetUser user = new AspNetUser();
            UserModel objUserEntity = new UserModel();
            if (!string.IsNullOrEmpty(userEntity.Id) && userEntity.Step==2)
            {
                user = uow.UserRepository.GetAll().Where(a => a.Id == userEntity.Id).FirstOrDefault();               
                user.PhoneNumber = userEntity.PhoneNumber;               
                user.Zip = userEntity.Zip;
                user.StateId = userEntity.StateId;
                user.City = userEntity.City;
                user.Address2 = userEntity.Address2;
                user.Address1 = userEntity.Address1;
                user.Website = userEntity.Website;                
                uow.UserRepository.Edit(user);
                uow.Save();
                

            }

            else if(!string.IsNullOrEmpty(userEntity.Id) && userEntity.Step == 3)
            {
                user = uow.UserRepository.GetAll().Where(a => a.Id == userEntity.Id).FirstOrDefault();
                user.DietsSelected = userEntity.DietsSelected;
                user.CuisinesSelected = userEntity.CuisinesSelected;            
                uow.UserRepository.Edit(user);
                uow.Save();
            }
            Mapper.CreateMap<AspNetUser, UserModel>();
            objUserEntity = Mapper.Map<UserModel>(user);

            objUserEntity.Step = userEntity.Step;
            //UserId = userEntity.Id;
            return objUserEntity;
        }

        public UserEntity GetValidUser(string UserName, string Password)
        {
            UserEntity userEntity = new UserEntity();
            if (!string.IsNullOrEmpty(UserName) && !string.IsNullOrEmpty(Password))
            {
               // var user=
                var user = uow.UserRepository.GetAll().Any(a => (string.Equals(a.UserName, UserName, StringComparison.OrdinalIgnoreCase)) && (string.Equals(a.PasswordHash, Password, StringComparison.OrdinalIgnoreCase)));
                Mapper.CreateMap<AspNetUser, UserEntity>();
                userEntity= Mapper.Map<UserEntity>(user);
            }
            return userEntity;
        }


        public Vendormodel CreateVendor(VendorsEntity objvendorEntity)
        {
            Vendor objtblvendor = new Vendor();
            Vendormodel objVendorModal = new Vendormodel();
           
            
            
            
            Mapper.CreateMap<VendorsEntity, Vendor>();
            Vendor objvendor = Mapper.Map<Vendor>(objvendorEntity);
            uow.VendorRepository.Insert(objvendor);
            uow.Save();
            Mapper.CreateMap<Vendor, Vendormodel>();
            objVendorModal = Mapper.Map<Vendormodel>(objvendor);
            objVendorModal.plan = "Free";
            return objVendorModal;
        }

    }
}
