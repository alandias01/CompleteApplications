using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data.Equilend
{
    public class LegalEntityContactDA : IDataProvider<LegalEntityContact>
    {
        EquilendEntities EE = new EquilendEntities();

        public void Load(out LegalEntityContact obj, int Id)
        {
            throw new NotImplementedException();
        }

        public void Load(ICollection<LegalEntityContact> collection)
        {
            EE.LegalEntityContacts.ToList().ForEach(collection.Add);
        }

        public void Load(ICollection<LegalEntityContact> collection, DateTime dateOfData)
        {
            throw new NotImplementedException();
        }

        public void Insert(LegalEntityContact obj)
        {
            try
            {
                EE.LegalEntityContacts.AddObject(obj);
                EE.SaveChanges();
            }
            catch (Exception)
            {
                EE.Detach(obj);
                throw;
            }
        }

        public void Update(LegalEntityContact obj)
        {
            try
            {
                EE.SaveChanges();
            }
            catch (Exception)
            {
                EE.Detach(obj);
                throw;
            }
        }

        public void Delete(LegalEntityContact obj)
        {
            try
            {
                EE.LegalEntityContacts.DeleteObject(obj);
                EE.SaveChanges();
            }
            catch (Exception)
            {
                EE.Detach(obj);
                throw;
            }
        }
    }
}
