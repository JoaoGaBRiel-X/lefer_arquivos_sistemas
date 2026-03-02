// Produto obrigatoriamente tem que estar com a unidade KG e para preencher a validade, usar o campo PLU na aba cadastro outras informacoes para informar a quantidade de dias
using System;
using XDPeople.Data;
using XDPeople.Utils;

namespace XRest{
	
	public class Script
	{
	    public static void Execute (ScriptHost host)
		{
			Db.CurrentDatabase.ExecuteCommand ("SELECT CONCAT ('01010',(LPAD(keyid,06,'0')),(LPAD((ROUND((retailprice1*100),0)),06,'0')),(LPAD(PLU,03,'0')),description) INTO OUTFILE 'c:/XDSoftware/TXITEMS.txt' LINES TERMINATED BY '\r\n' FROM items Where UnitOfSale = 2;");
			MessageDG.ShowSuccess(null,"SUCESSO","Script Executado com Sucesso. \nTodos os produtos foram exportados. \n \nLocalização: pasta raiz XDSoftware \nNome ficheiro: TXITEMS.txt");
	    }
	}
}
