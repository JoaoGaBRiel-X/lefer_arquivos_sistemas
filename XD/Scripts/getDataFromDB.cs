using System;
using XDPeople.Utils;
using System.Data;
using XDPeople.Data;
using XDPeople.Entities;
using XDPeople.Business;
using System.Collections.Generic;

namespace XRest{
	
	public class Script
	{
        private static List<string> Rows = new List<string>();
        private static string Query { get; set; }

        public static void Execute(ScriptHost host)
        {
            /*
             * Os dados retornados pela consulta estão sendo armazenados dentro da lista Rows
             * 
             * foreach (string row in Rows)
             * {
             *      MessageDG.ShowSuccess(null, "Teste", row);
             * }
            */

            SetQuery("SELECT Description FROM items;");
            AddResultQueryInRowsList();
        }

        private static void SetQuery(string query)
        {
            Query = query;
        }

        private static void AddResultQueryInRowsList()
        {
            try
            {
                System.Data.DataTable dataTable = Db.CurrentDatabase.GetDataTable(Query);
                foreach (DataRow row in dataTable.Rows)
                {
                    Rows.Add(row[0].ToString());
                }
            }
            catch (Exception e)
            {
                MessageDG.ShowError(null, "Erro", Convert.ToString(e));
            }
        }
    }
}
