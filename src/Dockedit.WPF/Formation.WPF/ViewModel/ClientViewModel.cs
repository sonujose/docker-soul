using Formation.WPF.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Linq;

namespace Formation.WPF
{
    public class ClientViewModel :CommandBase<Client>
    {
        private DemoEntities dbContext = new DemoEntities();

        protected override void Get()
        {
            try
            {
                dbContext.Client.ToList().ForEach(monClient => dbContext.Client.Local.Add(monClient));
                Collection = dbContext.Client.Local;
            }
            catch (Exception ex)
            {      
                throw ex;
            }
            
        }
        protected override bool CanGet()
        {
            return true;
        }
        protected override void Save()
        {
            foreach (Client item in Collection)
            {
                if (dbContext.Entry(item).State == System.Data.Entity.EntityState.Added)
                {
                    dbContext.Client.Add(item);
                }
            }
            dbContext.SaveChanges();
        }
        protected override void Delete()
        {

        }
    }
}

