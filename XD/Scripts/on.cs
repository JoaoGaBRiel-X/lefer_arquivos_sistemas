using System;
using XDPeople.Data;
using XDPeople.Utils;

namespace XRest{
	
	public class Script
	{

		

	    public static void Execute (ScriptHost host)
		{
			
			IniFile.TerminalConfiguration.TerminalMisc.FiscalPrinterActive = false;
            XDPeople.Data.TerminalProvider terminalProvider = new XDPeople.Data.TerminalProvider();
            terminalProvider.Save(IniFile.TerminalConfiguration, false);
			
			Db.CurrentDatabase.ExecuteCommand ("UPDATE xconfigterminals SET SatFiscalBr = true WHERE Id = 1;");
			GlobalVars.LoadTerminalConfig();
					
			MessageDG.ShowSuccess(null,"Ativo!","");

	    }
	}
}
