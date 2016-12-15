using System;
using System.Linq;
using Opmas.Data.DataContext.DataContext.UserDataContext;
using Opmas.Data.Objects.Entities.User;

namespace Opmas.Data.Factory.ApplicationManagement
{
    public class AppUserFactory
    {
        private readonly AppUserDataContext _db = new AppUserDataContext();

        /// <summary>
        ///     This method finds a yuser with the provided password and email
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <param name="institutionId"></param>
        /// <returns></returns>
        public AppUser GetAppUserByLogin(string email, string password,long institutionId)
        {
            email = email.Trim();
            var appUser =
                _db.AppUsers.FirstOrDefault(n => n.Email == email && n.Password == password && n.InstitutionId == institutionId);
            return appUser;
        }

        /// <summary>
        ///     This method checks if a user exist
        /// </summary>
        /// <param name="email"></param>
        /// <param name="matricNumber"></param>
        /// <returns></returns>
        public bool CheckIfStudentUserExist(string email, string matricNumber)
        {
            var userExist = false;
            try
            {
                var allUsers = _db.AppUsers;
                if (allUsers.Any(n => n.Email == email))
                    userExist = true;
            }
            catch (Exception)
            {
                // ignored
            }
            return userExist;
        }
        /// <summary>
        ///     This method checks if a user exist
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public bool CheckIfGeneralUserExist(string email)
        {
            var userExist = false;
            try
            {
                var allUsers = _db.AppUsers;
                if (allUsers.Any(n => n.Email == email))
                    userExist = true;
            }
            catch (Exception)
            {
                // ignored
            }
            return userExist;
        }

        /// <summary>
        ///     This method is used to retrieve a user by user email
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public AppUser GetAppUserByEmail(string email)
        {
            email = email.Trim();
            var appUser = _db.AppUsers.Single(n => n.Email == email);
            return appUser;
        }

        /// <summary>
        ///     This method retrives a user by an id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public AppUser GetAppUserById(int id)
        {
            var appUser = _db.AppUsers.Find(id);
            return appUser;
        }
    }
}