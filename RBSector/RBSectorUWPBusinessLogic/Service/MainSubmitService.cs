using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RBSectorUWPBusinessLogic.Service
{
    public class MainSubmitService
    {
        private MainServiceClient.MainServiceClient srv;
        public MainSubmitService()
        {
            srv= new MainServiceClient.MainServiceClient(MainServiceClient.MainServiceClient.EndpointConfiguration.BasicHttpBinding_IMainService);
        }
        public bool SaveResult(string json, string deleted)
        {
            try
            {
                return srv.SaveResultAsync(json, deleted).Result;
            }
            catch(Exception e)
            {
                string exception = e.Message;
                return false;
            }
        }
    }
}
