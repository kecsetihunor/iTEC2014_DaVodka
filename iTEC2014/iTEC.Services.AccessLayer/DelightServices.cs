using iTEC.Services.AccessLayer.DelightService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iTEC.Services.AccessLayer
{
    public class DelightServices : IDelightServices
    {
        #region Account
        public IEnumerable<AccountAvatarDTO> GetAccounts()
        {
            using (DelightServiceClient service = new DelightServiceClient())
            {
                var accounts = service.GetAccounts();
                return accounts;
            }
        }

        public AccountAvatarDTO GetUserStateInfo(int id)
        {
            using (DelightServiceClient service = new DelightServiceClient())
            {
                var userInfo = service.GetUserStateInfo(id);
                return userInfo;
            }
        }

        public int Authenticate(string username, string password, bool remember)
        {
            using (DelightServiceClient service = new DelightServiceClient())
            {
                var authenticateId = service.Authenticate(username, password, remember);
                return authenticateId;
            }
        }

        public void RegisterUser(string username, string password, string email)
        {
            DelightServiceClient service = new DelightServiceClient();
            service.RegisterUser(username, password, email);
            service.Close();
        }

        public void RegisterOwner(string username, string password, string email)
        {
            using (DelightServiceClient service = new DelightServiceClient())
            {
                service.RegisterOwner(username, password, email);
            }
        }

        public int GetAccountId(string username)
        {
            using (DelightServiceClient service = new DelightServiceClient())
            {
                var accountId = service.GetAccountId(username);
                return accountId;
            }
        } 
        #endregion

        #region Category
        public IEnumerable<CategoryDTO> GetCategories()
        {
            using (DelightServiceClient service = new DelightServiceClient())
            {
                var categories = service.GetCategories();
                return categories;
            }
        }

        public CategoryDTO GetCategory(int id)
        {
            using (DelightServiceClient service = new DelightServiceClient())
            {
                var category = service.GetCategory(id);
                return category;
            }
        }

        public IEnumerable<VotedProductDTO> GetProductsFromCategory(String username, int id)
        {
            using (DelightServiceClient service = new DelightServiceClient())
            {
                var product = service.GetProductsFromCategory(username, id);
                return product;
            }
        }

        public IEnumerable<VotedCategoryDTO> GetVotedCategories(String username)
        {
            using (DelightServiceClient service = new DelightServiceClient())
            {
                return service.GetVotedCategories(username);
            }
        }

        public void AddProductToCategory(int categoryId, int productId)
        {
            using (DelightServiceClient service = new DelightServiceClient())
            {
                service.AddProductToCategory(categoryId, productId);
            }
        }

        public void RemoveProductFromCategory(int productId)
        {
            using (DelightServiceClient service = new DelightServiceClient())
            {
                service.RemoveProductFromCategory(productId);
            }
        }

        public void DeleteCategory(int id)
        {
            using (DelightServiceClient service = new DelightServiceClient())
            {
                service.DeleteCategory(id);
            }
        }

        public Int32 SaveCategory(CategoryDTO category)
        {
            using (DelightServiceClient service = new DelightServiceClient())
            {
                return service.SaveCategory(category);
            }
        }
        
        #endregion

        #region Product
        public IEnumerable<ProductDTO> GetProducts()
        {
            using (DelightServiceClient service = new DelightServiceClient())
            {
                var products = service.GetProducts();
                return products;
            }
        }

        public PricedProductDTO GetProduct(int id)
        {
            using (DelightServiceClient service = new DelightServiceClient())
            {
                var product = service.GetProduct(id);
                return product;
            }
        }

        public IEnumerable<ProductFromCategoryDTO> GetAllProductsCompareToSpecificCategory(int categoryId)
        {
            using (DelightServiceClient service = new DelightServiceClient())
            {
                var products = service.GetAllProductsCompareToSpecificCategory(categoryId);
                return products;
            }
        }

        public void DeleteProduct(int id)
        {
            using (DelightServiceClient service = new DelightServiceClient())
            {
                service.DeleteProduct(id);
            }
        }

        public Int32 SaveProduct(PricedProductDTO product)
        {
            using (DelightServiceClient service = new DelightServiceClient())
            {
                return service.SaveProduct(product);
            }
        }
        
        #endregion

        #region UserGroup
        public IEnumerable<UserGroupDTO> GetUserGroups()
        {
            using (DelightServiceClient service = new DelightServiceClient())
            {
                var ugs = service.GetUserGroups();
                return ugs;
            }
        }

        public UserGroupDTO GetUserGroup(int id)
        {
            using (DelightServiceClient service = new DelightServiceClient())
            {
                var ug = service.GetUserGroup(id);
                return ug;
            }
        }

        public IEnumerable<UserDTO> GetUsersFromGroup(int id)
        {
            using (DelightServiceClient service = new DelightServiceClient())
            {
                return service.GetUsersFromGroup(id);
            }
        }

        public void AddUserToGroup(int userGroupId, int userId)
        {
            using (DelightServiceClient service = new DelightServiceClient())
            {
                service.AddUserToGroup(userGroupId, userId);
            }
        }

        public void RemoveUserFromGroup(int userId)
        {
            using (DelightServiceClient service = new DelightServiceClient())
            {
                service.RemoveUserFromGroup(userId);
            }
        }

        public void DeleteUserGroup(int id)
        {
            using (DelightServiceClient service = new DelightServiceClient())
            {
                service.DeleteUserGroup(id);
            }
        }

        public Int32 SaveUserGroup(UserGroupDTO userGroup)
        {
            using (DelightServiceClient service = new DelightServiceClient())
            {
                return service.SaveUserGroup(userGroup);
            }
        } 
        #endregion

        #region User
        public IEnumerable<UserDTO> GetUsers()
        {
            using (DelightServiceClient service = new DelightServiceClient())
            {
                var us = service.GetUsers();
                return us;
            }
        }

        public UserDTO GetUser(int id)
        {
            using (DelightServiceClient service = new DelightServiceClient())
            {
                var ug = service.GetUser(id);
                return ug;
            }
        }

        public IEnumerable<UserFromGroupDTO> GetAllUsersCompareToSpecificGroup(int id)
        {
            using (DelightServiceClient service = new DelightServiceClient())
            {
                return service.GetAllUsersCompareToSpecificGroup(id);
            }
        }

        public void DeleteUser(int id)
        {
            using (DelightServiceClient service = new DelightServiceClient())
            {
                service.DeleteUser(id);
            }
        }

        public Int32 SaveUser(UserDTO user)
        {
            using (DelightServiceClient service = new DelightServiceClient())
            {
                return service.SaveUser(user);
            }
        } 
        #endregion

        #region Vote
        public IEnumerable<VotedProductDTO> GetVotes()
        {
            using (DelightServiceClient service = new DelightServiceClient())
            {
                var votes = service.GetVotes();
                return votes;
            }
        }

        public VoteDTO GetVote(int id)
        {
            using (DelightServiceClient service = new DelightServiceClient())
            {
                var vote = service.GetVote(id);
                return vote;
            }
        }

        public void DeleteVote(int id)
        {
            using (DelightServiceClient service = new DelightServiceClient())
            {
                service.DeleteVote(id);
            }
        }

        public Int32 SaveVote(VoteDTO vote)
        {
            using (DelightServiceClient service = new DelightServiceClient())
            {
                return service.SaveVote(vote);
            }
        }

        public Int32 Vote(String username, Int32 productId, Int32 points)
        {
            using (DelightServiceClient service = new DelightServiceClient())
            {
                return service.Vote(username, productId, points);
            }
        }

        public Int32 GetAllVotedPoints()
        {
            using (DelightServiceClient service = new DelightServiceClient())
            {
                return service.GetAllVotedPoints();
            }
        }

        public Int32 GetAvailablePoints(String username)
        {
            using (DelightServiceClient service = new DelightServiceClient())
            {
                return service.GetAvailablePoints(username);
            }
        }
        #endregion

        #region Report
        public ProductVoteReportChart GetProductVoteReport()
        {
            using (DelightServiceClient service = new DelightServiceClient())
            {
                return service.GetProductVoteReport();
            }
        }

        public IEnumerable<BudgetDTO> GetBudget(Int32 budget)
        {
            using (DelightServiceClient service = new DelightServiceClient())
            {
                return service.GetBudget(budget);
            }
        }
        #endregion

        public void StartSession()
        {
            using (DelightServiceClient service = new DelightServiceClient())
            {
                service.StartSession();
            }
        }

        #region Message
        public MessageDTO GetMessage()
        {
            using (DelightServiceClient service = new DelightServiceClient())
            {
                return service.GetMessage();
            }
        }
        public void SaveMessage(MessageDTO message)
        {
            using (DelightServiceClient service = new DelightServiceClient())
            {
                service.SaveMessage(message);
            }
        }
        #endregion
    }
}
