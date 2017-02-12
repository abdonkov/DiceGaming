using DiceGaming.Core.Exceptions;
using DiceGaming.Core.Models;
using DiceGaming.Data;
using DiceGaming.Data.Entities;
using System.Collections.Generic;
using System.Linq;

namespace DiceGaming.Core.Repositories
{
    public class LoginRepository : ILoginRepository
    {
        private LoginDto CreateLoginDTO(Login login)
        {
            return new LoginDto()
            {
                Id = login.Id,
                UserId = login.UserId,
                Token = login.Token
            };
        }

        public LoginDto LoginUser(LoginDto login)
        {
            Login newLogin = new Login();

            using (var db = new DiceGamingDb())
            {
                if (db.Users.FirstOrDefault(u => u.Id == login.UserId) == null)
                    throw new NotFoundException();

                newLogin.UserId = login.UserId;
                newLogin.Token = login.Token;

                db.Logins.Add(newLogin);
                db.SaveChanges();
            }

            return CreateLoginDTO(newLogin);
        }

        public void Delete(int id)
        {
            using (var db = new DiceGamingDb())
            {
                var login = db.Logins.FirstOrDefault(l => l.Id == id);
                if (login == null)
                    throw new NotFoundException();

                db.Logins.Remove(login);
                db.SaveChanges();
            }
        }

        public LoginDto Get(int id)
        {
            Login login;

            using (var db = new DiceGamingDb())
            {
                login = db.Logins.FirstOrDefault(l => l.Id == id);

                if (login == null)
                    throw new NotFoundException();
            }

            return CreateLoginDTO(login);
        }

        public IEnumerable<LoginDto> GetUserLogins(int userId)
        {
            IQueryable<Login> logins;
            using (var db = new DiceGamingDb())
            {
                logins = db.Logins.Where(l => l.UserId == userId);
            }

            foreach (var login in logins)
            {
                yield return CreateLoginDTO(login);
            }
        }

        public bool HasLoginAndToken(int loginId, string token)
        {
            using (var db = new DiceGamingDb())
            {
                if (db.Logins.FirstOrDefault(l => l.Id == loginId && object.Equals(l.Token, token)) == null)
                    return false;
            }
            return true;
        }

        public bool HasUserAndToken(int userId, string token)
        {
            using (var db = new DiceGamingDb())
            {
                if (db.Logins.FirstOrDefault(l => l.UserId == userId && object.Equals(l.Token, token)) == null)
                    return false;
            }
            return true;
        }
    }
}