using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EstateData
{
    public static class Admin
    {
        #region Estate
        public static void AddEstate(Estate estate)
        {
            using (RealEstateDataDataContext conn = new RealEstateDataDataContext())
            {
                conn.Estates.InsertOnSubmit(estate);
                conn.SubmitChanges();
            }
        }

        public static void UpdateEstate(Estate estate)
        {
            using (RealEstateDataDataContext conn = new RealEstateDataDataContext())
            {
                Estate est = (from e1 in conn.Estates
                              where e1.Id == estate.Id
                              select e1).FirstOrDefault();
                est.Name = estate.Name;
                est.Description = estate.Description;
                est.Fee = estate.Fee;
                est.Owner = estate.Owner;

                conn.SubmitChanges();
            }
        }

        public static void DeleteEstate(Estate estate)
        {
            using (RealEstateDataDataContext conn = new RealEstateDataDataContext())
            {
                Estate est = (from e1 in conn.Estates
                             where e1.Id == estate.Id
                             select e1).FirstOrDefault();
                conn.Estates.DeleteOnSubmit(est);
                conn.SubmitChanges();
                
            }
        }
        #endregion

        #region Client

        public static void AddClient(Client client)
        {
            using (RealEstateDataDataContext conn = new RealEstateDataDataContext())
            {
                conn.Clients.InsertOnSubmit(client);
                conn.SubmitChanges();
            }
        }

        public static void UpdateClient(Client client)
        {
            using (RealEstateDataDataContext conn = new RealEstateDataDataContext())
            {
                Client cli = (from c in conn.Clients
                              where c.Id == client.Id
                              select c).FirstOrDefault();
                cli.FirstName = client.FirstName;
                cli.LastName = client.LastName;

                conn.SubmitChanges();
            }
        }

        public static void DeleteClient(Client client)
        {
            using (RealEstateDataDataContext conn = new RealEstateDataDataContext())
            {
                Client cli = (from c in conn.Clients
                              where c.Id == client.Id
                              select c).FirstOrDefault();
                conn.Clients.DeleteOnSubmit(cli);
                conn.SubmitChanges();
            }
        }

        #endregion

        #region Transaction

        public static void AddTransaction(Transaction transaction)
        {
            using (RealEstateDataDataContext conn = new RealEstateDataDataContext())
            {
                conn.Transactions.InsertOnSubmit(transaction);
                conn.SubmitChanges();
            }
        }

        #endregion

        #region Ad

        public static void AddAd(Ad ad)
        {
            using (RealEstateDataDataContext conn = new RealEstateDataDataContext())
            {
                conn.Ads.InsertOnSubmit(ad);
                conn.SubmitChanges();
            }
        }

        #endregion
    }
}
