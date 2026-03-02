using System;
using XDPeople.Utils;
using System.Data;
using XDPeople.Data;
using XDPeople.Entities;
using XDPeople.Business;
using System.Collections.Generic;

namespace XRest
{
	public class Script
	{
        public static void Execute(ScriptHost host)
	    {
			try
			{
                if (IsDisable(22) == true)
				{
                    Db.CurrentDatabase.ExecuteCommand("INSERT INTO xconfigprinters(Id, ConfigId, Driver, Port, ReportConfiguration, Redirected, UsePrinter, InvertPrinting, Terminal, DriverId, NumberCopies, SyncStamp) VALUES (22, 1, 'OSPRINTER', 'Jetway JP-500', 'BRG_EXTRATO_SAT.DOK', NULL, 1, 0, 1, 1, 1, NOW())");
                    MessageDG.ShowSuccess(null, "Sucesso", "A impressora de fechamento foi habilitada com sucesso!");
                }
				else
				{
                    Db.CurrentDatabase.ExecuteCommand("DELETE FROM xconfigprinters WHERE id = 22");
                    MessageDG.ShowSuccess(null, "Sucesso", "A impressora de fechamento foi desabilitada com sucesso!");
                }
                
			}
			catch(Exception e)
			{
				MessageDG.ShowError(null, "Erro", "Ocorreu um erro: " + Convert.ToString(e));
			}
	    }

		private static bool IsDisable(int id)
		{
			string query = "SELECT DriverId FROM xconfigprinters WHERE Id = " + Convert.ToString(id) + ";";
            System.Data.DataTable dataTable = Db.CurrentDatabase.GetDataTable(query);

			if (dataTable.Rows.Count == 0)
			{
				return true;
			}
			else
			{
				return false;
			}
        }
	}
}