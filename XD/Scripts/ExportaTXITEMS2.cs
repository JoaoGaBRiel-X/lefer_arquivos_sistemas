using System;
using XDPeople.Utils;
using System.Data;
using XDPeople.Data;
using XDPeople.Entities;
using XDPeople.Business;
using System.Collections.Generic;
using System.IO;

namespace XRest{

    public class Script
    {
        private static List<string> Rows = new List<string>();
        private static string Query { get; set; }
        private static string Path { get; set; }
        private static string FileName { get; set; }

	    public static void Execute (ScriptHost host)
		{
            SetQuery("SELECT CONCAT ('01010',(LPAD(keyid,06,'0')),(LPAD((ROUND((retailprice1*100),0)),06,'0')),(LPAD(PLU,03,'0')),description) FROM items Where UnitOfSale = 2;");
            SetFileName("TXITEMS.txt");
            SetPath(@"C:\XDSoftware\");
            AddResultQueryInRowsList();
            InsertResultQueryInFile();
        }

        private static void SetQuery(string query)
        {
            Query = query;
        }

        private static void SetPath(string path)
        {
            Path = path + FileName;
        }

        private static void SetFileName(string fileName)
        {
            FileName = fileName;
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
            catch (Exception)
            {
                MessageDG.ShowError(null, "Erro", "Ocorreu um erro ao adicionar os dados em" + FileName);
            }
        }

        private static void InsertResultQueryInFile()
        {
            try
            {
                using (StreamWriter sw = File.CreateText(Path))
                {
                    foreach (string row in Rows)
                    {
                        sw.WriteLine(row);
                    }
                }
                MessageDG.ShowSuccess(null,"SUCESSO","Script Executado com Sucesso. \nTodos os produtos foram exportados. \n \nLocalização: " + Path + "\nNome arquivo: " + FileName);
            }
            catch (Exception)
            {
                MessageDG.ShowError(null, "Erro", "Ocorreu um erro ao adicionar os dados em" + FileName);
            }
        }
	}
}
