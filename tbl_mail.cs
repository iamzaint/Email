using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EMAIL.DBModel
{
    public partial class tbl_mail
    {

        public List<tbl_mail> getAll()
        {
            try
            {
                using (var context = new TestDBEntities())
                {
                    return context.tbl_mail.ToList();

                }
            }
            catch (Exception ex)
            {

                return null;
            }
        }
    }
}