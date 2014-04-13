using iTEC.Providers;
using iTEC.Storage.Entities;
using iTEC.Storage.Repositories.Contexts;
using iTEC.Storage.Repositories.DTO;
using iTEC.Storage.Repositories.Exceptions;
using System;
using System.IO;
using System.Linq;

namespace iTEC.Storage.Repositories
{
    public class ReservationRepository : Repository<PickPlaceContext>
    {
        public PlaceBasicInformationDTO[] SearchPlacesByName(String criteria, Int32 space, DateTime from, DateTime until, Int32 skip, Int32 take)
        {
            var places = Enumerable.Range(0, 250)
                .Select(x => new PlaceBasicInformationDTO()
                {
                    Id = x,
                    Name = String.Format("Place name {0}", x),
                    Address = String.Format("Unknown generated address {0}", x),
                    Rating = (Byte)(x % 6)
                });
            if (!String.IsNullOrEmpty(criteria))
            {
                places = places.Where(x => x.Name.ToLower().Contains(criteria.ToLower()));
            }
            return places.Skip(skip).Take(take).ToArray();
        }

        #region Helpers

        protected Account GetUser(Int32 id)
        {
            return GetAll<Account>().FirstOrDefault(x => x.Id == id);
        }

        protected Account GetUser(String username)
        {
            return GetAll<Account>().FirstOrDefault(x => x.Username.Equals(username, StringComparison.InvariantCultureIgnoreCase));
        }

        protected IQueryable<PlaceOwnership> GetOwnerships(Account account)
        {
            return GetAll<PlaceOwnership>().Where(x => x.Account.Id == account.Id);
        }

        #endregion
    }

    public static class ValidationExtensions
    {
        public static Account Validate(this Account account)
        {
            if (account == null)
            {
                throw new InvalidAccountException();
            }
            return account;
        }
    }
}