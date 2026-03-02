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

				//Mensagem a pedir para confirmar
				if(MessageDG.ShowQuestionYesNo(null,"Atenção", "Deseja alterar o CNPJ da software?") == MessageDG.MessageResponse.Yes){
					
					//Altera CNPJ da software house
                    Db.CurrentDatabase.ExecuteCommand("UPDATE config SET Data = replace(Data,'\"CompanyCnpj\":\"23538714000153\"', '\"CompanyCnpj\":\"46400996000123\"') where Id = \"SatConfig\";");
                    MessageDG.ShowExclamation(null, "Sucesso", "O CNPJ da Software House foi alterado!");

				}
			}
			catch(Exception e){
				
				//Mensagem de aviso
				MessageDG.ShowExclamation(null, "Erro", "O CNPJ da Software House não foi alterado!");
				
			}
	    }
	}
}