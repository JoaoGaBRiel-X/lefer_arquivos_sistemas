using System;
using XDPeople.Utils;

//o compo namespace é obrigatório para o script correr no XD
namespace XRest{
	//É Obrigatório o Campo class Script para executar a função Script
	public class Script
	{
		//A função Execute é obrigatória em todos os scripts. É a partir daqui que todo o código do script vai ser chamado.
	    public static void Execute(ScriptHost host)
	    {
			try
			{
				//Mostra Empregado Seleccionado
				if(GlobalVars.GlobalEmployeeNumber != null)
					 MessageDG.ShowInfo(null, "Mostra Empregado seleccionado", GlobalVars.GlobalEmployeeNumber.ToString());
				
			}
			catch(Exception e)
			{
				//Não é obrigatório inserir código aqui
				//aqui colocamos o código que pretendemos executar em caso de algum erro ter ocorrido por exemplo mostrar mensagem com o erro que ocorreu				
			}
			finally
			{
				//Não é obrigatório inserir código aqui
				//aqui colocamo o código que queremos que execute mesmo que aconteça algum erro
			}
	    }
	}
}
