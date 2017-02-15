using NHibernate;
using RBSector.DataBase.Models;
using RBSector.Entry.Logic;
using System;
using System.Collections.Generic;
using System.Json;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RBSector.Entry.Entry
{
    public class CategoryEntry
    {
        private ISession session;
        private UniversalCRUD<Category> category_crud;
        public CategoryEntry()
        {
            session = NHibernateConf.Session;
            category_crud = new UniversalCRUD<Category>();
        }
        public bool AddCategory(string json)
        {
            if (string.IsNullOrEmpty(json)) return false;
            Category newCat = new Category();
            var obj = (JsonObject)JsonValue.Parse(json);
            newCat.CtName = obj["CT_Name"].ToString().Trim('\"');
            return category_crud.SaveOrUpdate(newCat);
        }
    }
}
