using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects;

namespace Data.Equilend
{
    public class DTCMapDA : IDataProvider<DTCMap>
    {
        EquilendEntities EE = new EquilendEntities();

        public void Load(out DTCMap obj, int Id)
        {
            throw new NotImplementedException();
        }

        public void Load(ICollection<DTCMap> collection)
        {
            EE.DTCMaps.ToList().ForEach(collection.Add);
        }

        public void Load(ICollection<DTCMap> collection, DateTime dateOfData)
        {
            EE.DTCMaps.Where(x => EntityFunctions.TruncateTime(x.DateEntered) == dateOfData).ToList().ForEach(collection.Add);
        }

        public void Insert(DTCMap obj)
        {
            try
            {
                EE.DTCMaps.AddObject(obj);
                EE.SaveChanges();
            }
            catch (Exception)
            {
                EE.Detach(obj);
                throw;
            }
        }

        public void Update(DTCMap obj)
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

        public void Delete(DTCMap obj)
        {
            try
            {
                EE.DTCMaps.DeleteObject(obj);
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
