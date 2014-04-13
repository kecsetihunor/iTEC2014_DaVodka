using iTEC.BL.Interfaces.DTOs;
using iTEC.BL.Interfaces.Managers;
using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace iTEC.Services
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "DelightService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select DelightService.svc or DelightService.svc.cs at the Solution Explorer and start debugging.
    public class DelightService : IDelightService
    {
        private IAccountManager _accountManager;
        private ICategoryManager _categoryManager;
        private IProductManager _productManager;
        private IUserGroupManager _userGroupManager;
        private IUserManager _userManager;
        private IVoteManager _voteManager;
        private ISessionManager _sessionManager;
        private IMessageManager _messageManager;

        public DelightService()
        {
            IoCBootstrapper.Boot();
            _accountManager = ObjectFactory.GetInstance<IAccountManager>();
            _categoryManager = ObjectFactory.GetInstance<ICategoryManager>();
            _productManager = ObjectFactory.GetInstance<IProductManager>();
            _userGroupManager = ObjectFactory.GetInstance<IUserGroupManager>();
            _voteManager = ObjectFactory.GetInstance<IVoteManager>();
            _userManager = ObjectFactory.GetInstance<IUserManager>();
            _sessionManager = ObjectFactory.GetInstance<ISessionManager>();
            _messageManager = ObjectFactory.GetInstance<IMessageManager>();
        }

        #region Account
        public IEnumerable<AccountAvatarDTO> GetAccounts()
        {
            return _accountManager.GetAccounts();
        }

        public AccountAvatarDTO GetUserStateInfo(int id)
        {
            return _accountManager.GetUserStateInfo(id);
        }

        public int Authenticate(string username, string password, bool remember)
        {
            return _accountManager.Authenticate(username, password, remember);
        }

        public void RegisterUser(string username, string password, string email)
        {
            _accountManager.RegisterUser(username, password, email);
        }

        public void RegisterOwner(string username, string password, string email)
        {
            _accountManager.RegisterOwner(username, password, email);
        }

        public int GetAccountId(string username)
        {
            return _accountManager.GetAccountId(username);
        } 
        #endregion

        #region Category
        public IEnumerable<CategoryDTO> GetCategories()
        {
            return _categoryManager.GetCategories();
        }

        public CategoryDTO GetCategory(int id)
        {
            return _categoryManager.GetCategory(id);
        }

        public IEnumerable<VotedCategoryDTO> GetVotedCategories(String username)
        {
            var result = new List<VotedCategoryDTO>();
            var sessionId = _sessionManager.GetActiveSessionId();
            var categories = _categoryManager.GetCategories();
            foreach (var category in categories)
            {
                result.Add(new VotedCategoryDTO()
                {
                    Name = category.Name,
                    Count = category.Count,
                    Id = category.Id,
                    Points = _voteManager.GetAllPointsFromCategory(username, category.Id, sessionId)
                });
            }

            return result;
        }

        public IEnumerable<VotedProductDTO> GetProductsFromCategory(String username, int id)
        {
            var result = new List<VotedProductDTO>();
            var sessionId = _sessionManager.GetActiveSessionId();
            var products = _categoryManager.GetProductsFromCategory(id);
            foreach (var product in products)
            {
                result.Add(new VotedProductDTO()
                {
                    Name = product.Name,
                    Id = product.Id,
                    Points = _voteManager.GetAllPointsFromProduct(username, product.Id, sessionId)
                });
            }

            return result;
        }

        public void AddProductToCategory(int categoryId, int productId)
        {
            _categoryManager.AddProductToCategory(categoryId, productId);
        }

        public void RemoveProductFromCategory(int productId)
        {
            _categoryManager.RemoveProductFromCategory(productId);
        }

        public void DeleteCategory(int id)
        {
            _categoryManager.DeleteCategory(id);
        }

        public Int32 SaveCategory(CategoryDTO userGroup)
        {
            return _categoryManager.SaveCategory(userGroup);
        } 
        #endregion

        #region Product
        public IEnumerable<PricedProductDTO> GetProducts()
        {
            return _productManager.GetProducts();
        }

        public PricedProductDTO GetProduct(int id)
        {
            return _productManager.GetProduct(id);
        }
        
        public IEnumerable<ProductFromCategoryDTO> GetAllProductsCompareToSpecificCategory(int categoryId)
        {
            return _productManager.GetAllProductsCompareToSpecificCategory(categoryId);
        }

        public void DeleteProduct(int id)
        {
            _productManager.DeleteProduct(id);
        }

        public Int32 SaveProduct(PricedProductDTO userGroup)
        {
            return _productManager.SaveProduct(userGroup);
        } 
        #endregion

        #region UserGroup
        public IEnumerable<UserGroupDTO> GetUserGroups()
        {
            return _userGroupManager.GetUserGroups();
        }

        public UserGroupDTO GetUserGroup(int id)
        {
            return _userGroupManager.GetUserGroup(id);
        }

        public IEnumerable<UserDTO> GetUsersFromGroup(int id)
        {
            return _userGroupManager.GetUsersFromGroup(id);
        }

        public void AddUserToGroup(int groupId, int userId)
        {
            _userGroupManager.AddUserToGroup(groupId, userId);
        }

        public void RemoveUserFromGroup(int userId)
        {
            _userGroupManager.RemoveUserFromGroup(userId);
        }

        public void DeleteUserGroup(int id)
        {
            _userGroupManager.DeleteUserGroup(id);
        }

        public Int32 SaveUserGroup(UserGroupDTO userGroup)
        {
            return _userGroupManager.SaveUserGroup(userGroup);
        } 
        #endregion

        #region User
        public IEnumerable<UserDTO> GetUsers()
        {
            return _userManager.GetUsers();
        }

        public UserDTO GetUser(int id)
        {
            return _userManager.GetUser(id);
        }

        public IEnumerable<UserFromGroupDTO> GetAllUsersCompareToSpecificGroup(int id)
        {
            return _userManager.GetAllUsersCompareToSpecificGroup(id);
        }

        public void DeleteUser(int id)
        {
            _userManager.DeleteUser(id);
        }

        public Int32 SaveUser(UserDTO userGroup)
        {
            return _userManager.SaveUser(userGroup);
        }
        #endregion

        #region Vote
        public IEnumerable<VotedProductDTO> GetVotes()
        {
            int sessionId = _sessionManager.GetActiveSessionId();
            return _voteManager.GetVotes(sessionId);
        }

        public VoteDTO GetVote(int id)
        {
            return _voteManager.GetVote(id);
        }

        public void DeleteVote(int id)
        {
            _voteManager.DeleteVote(id);
        }

        public Int32 SaveVote(VoteDTO vote)
        {
            return _voteManager.SaveVote(vote); 
        }

        public Int32 Vote(String username, Int32 productId, Int32 points)
        {
            int sessionId = _sessionManager.GetActiveSessionId();
            return _voteManager.Vote(username, productId, points, sessionId);
        }

        public Int32 GetAllVotedPoints()
        {
            return GetAllVotedPoints();
        }

        public Int32 GetAvailablePoints(String username)
        {
            int sessionId = _sessionManager.GetActiveSessionId();
            return _voteManager.GetAvailablePoints(username, sessionId);
        }

        public IEnumerable<BudgetDTO> GetBudget(Int32 budget)
        {
            int sessionId = _sessionManager.GetActiveSessionId();
            return _voteManager.CalculateBudget(budget, sessionId);
        }
        #endregion

        #region Report
        public ProductVoteReportChart GetProductVoteReport()
        {
            var sessionId = _sessionManager.GetActiveSessionId();
            var chart = new ProductVoteReportChart();
            chart.Generate(_voteManager.GetVotes(sessionId).ToArray());
            return chart;
        }
        #endregion

        public void StartSession()
        {
            _sessionManager.StartSession();
        }

        #region Message
        public MessageDTO GetMessage()
        {
            return _messageManager.GetMessage();
        }

        public void SaveMessage(MessageDTO message)
        {
            _messageManager.SaveMessage(message);
        }
        #endregion
    }
}
