using RBSector.DataBase.Models;
using System.Collections.Generic;
using System.ServiceModel;

namespace RBSector.WCF.IService
{
    [ServiceContract]
    public interface IUserService
    {
        [OperationContract]
        string isLogIn(string pin);

        [OperationContract]
        bool UpdateUser(Usersdata user);

        [OperationContract]
        Usersdata GetUser(int recId);

        [OperationContract]
        bool AddUser(string login, string password, string lname, string fname, string email, string role);

        [OperationContract]
        bool DeleteUser(int recId);

        [OperationContract]
        List<Usersdata> GetAllUsers();
    }
}
