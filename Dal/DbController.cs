using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Data;
using Bal.InterFaces;

namespace Dal
{
    public class DbController:IDisposable
    {
        SqlConnection Con;

        public DbController()
        {
            Con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConStr"].ConnectionString);
        }

        SqlConnection GetCon()
        {
            if (Con.State == System.Data.ConnectionState.Closed)
                Con.Open();
            return Con;
        }
        void CloseCon()
        {
            if (Con.State == System.Data.ConnectionState.Open)
                Con.Close();
        }

        public IEnumerable Get(string Text,System.Data.CommandType Type,List<SqlParameter> Params,Type objectType)
        {
            DataTable tb = new DataTable();
            var reader = GetCommand(Text, Type, Params).ExecuteReader();
            tb.Load(reader);
            Dispose();
            return GetObjectList(FillRows(tb), objectType);
        }

        public int DoQuery(string Text, System.Data.CommandType Type, List<SqlParameter> Params)
        {
            int res =  GetCommand(Text, Type, Params).ExecuteNonQuery();
            Dispose();
            return res;
        }

        IEnumerable GetObjectList(List<Dictionary<string, object>> dicRows,Type objectType)
        {
            List<object> objList = new List<object>();
            foreach (var row in dicRows)
            {
                var obj = Activator.CreateInstance(objectType);
                if (obj is IDbFiller)
                    objList.Add(((IDbFiller)obj).FillRow(row));
                else
                    objList.Add(row);
            }
            return (IEnumerable)objList;
        }

        private List<Dictionary<string,object>> FillRows(DataTable tb)
        {
            var DicRows = new List<Dictionary<string, object>>();
            foreach (DataRow row in tb.Rows)
            {
                var DicRow = new Dictionary<string, object>();
                foreach (DataColumn col in tb.Columns)
                    DicRow.Add(col.ColumnName, row[col]);
                DicRows.Add(DicRow);
            }
            return DicRows;
        }

        SqlCommand GetCommand(string Text, System.Data.CommandType Type,List<SqlParameter> Params)
        {
            var cmd = new SqlCommand(Text, GetCon());
            cmd.CommandType = Type;
            if (Params != null)
                cmd.Parameters.AddRange(Params.ToArray());
            return cmd;
        }
        public void Dispose()
        {
            CloseCon();
        }
    }
}
