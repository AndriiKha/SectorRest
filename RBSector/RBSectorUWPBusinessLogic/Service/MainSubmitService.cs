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
            srv = new MainServiceClient.MainServiceClient(MainServiceClient.MainServiceClient.EndpointConfiguration.BasicHttpBinding_IMainService);
        }
        public async Task<bool> SaveResult(string json, string deleted)
        {
            if (string.IsNullOrEmpty(json)) return false;
            try
            {
                return await srv.SaveResultAsync(json, deleted);
            }
            catch (Exception e)
            {
                string exception = e.Message;
                return false;
            }
        }
        public async Task<bool> SaveOrder(string json)
        {
            if (string.IsNullOrEmpty(json)) return false;
            try
            {
                return await srv.SaveOrderAsync(json);
            }
            catch (Exception ex)
            {
                string exception = ex.Message;
                return false;
            }
        }
    }
}
