using System;
using XDPeople.Data;
using XDPeople.Utils;

namespace XRest{
	//Obrigatório
	public class Script
	{
	//A função Execute é obrigatória em todos os scripts. É a partir de aqui que todo o código do script vai ser chamado.
	    public static void Execute(ScriptHost host)
	    {
			try{
				Db.CurrentDatabase.ExecuteCommand("DELETE FROM `xconfigsalezonesareaobjects` WHERE  `STATUS` IN (0, 2);");
			}
			catch(Exception e){
				
				//Mensagem de aviso
				MessageDG.ShowExclamation(null, "Erro", "O SCRIPT não foi executado");
				
			}
	    }
	}
}