using Bal;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal.DbModels
{
   public class UserDb
    {
        DbController dbCon;
        public UserDb()
        {
            dbCon = new DbController();
        }
        public List<User> Get()
        {
            //List<SqlParameter> parms = new List<SqlParameter>();
            //parms.Add(new SqlParameter("@Name", "y"));
            //SqlParameter pCount = new SqlParameter();
            //pCount.ParameterName = "@UserCount";
            //pCount.DbType = DbType.Int32;
            //pCount.Direction = ParameterDirection.Output;
            //parms.Add(pCount);
            //Dbcon.Get("spGetUserCount", typeof(int), CommandType.StoredProcedure, parms);
            //var val = pCount.Value;
            //get value from proc
            //var b  = dbCon.Get("select Count(Id) from User_Table", System.Data.CommandType.Text, null, typeof(int)).Cast<int>().ToArray();
            return dbCon.Get("select * from User_Table",System.Data.CommandType.Text,null, typeof(User)).Cast<User>().ToList();
        }

        public int Add(User u1)
        {
            List<SqlParameter> parms = new List<SqlParameter>();
            parms.Add(new SqlParameter("@FullName", u1.FullName));
            parms.Add(new SqlParameter("@Email", u1.Email));
            parms.Add(new SqlParameter("@BirthDay", u1.Birthday));
            parms.Add(new SqlParameter("@Gender", u1.Gender));
            parms.Add(new SqlParameter("@Phone", u1.Phone));
            return dbCon.DoQuery("AddUser",System.Data.CommandType.StoredProcedure, parms);
        }

      
    }
}

