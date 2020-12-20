using ErsSapAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Threading.Tasks;

namespace ErsSapAPI.Services
{
    public interface IHotPoService
    {
        [OperationContract]
        string Test(string s);
        [OperationContract]
        void XmlMethod(System.Xml.Linq.XElement xml);
        [OperationContract]
        HotPurchaseOrder TestCustomModel(HotPurchaseOrder inputModel);
    }
}
