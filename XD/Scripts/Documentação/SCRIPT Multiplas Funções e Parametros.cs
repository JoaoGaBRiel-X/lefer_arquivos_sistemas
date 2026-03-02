using System;
using System.IO;

namespace XRest
{
    public class Script
    {
        public static void Execute(ScriptHost host)
        {
            KeyboardFunctionStruct buttonParameters = new KeyboardFunctionStruct();
            buttonParameters.Parameter = "5";
            buttonParameters.Function = "MESA_N";
            ItemsSelectionWT.GetInstance().ButtonClicked(buttonParameters, new EventArgs());

 KeyboardFunctionStruct buttonParameters2 = new KeyboardFunctionStruct();
            buttonParameters2.Parameter = "23";
            buttonParameters2.Function = "ITEM_N";
            ItemsSelectionWT.GetInstance().ButtonClicked(buttonParameters2, new EventArgs());


 KeyboardFunctionStruct buttonParameters3 = new KeyboardFunctionStruct();
            buttonParameters3.Parameter = "26";
            buttonParameters3.Function = "ITEM_N";
            ItemsSelectionWT.GetInstance().ButtonClicked(buttonParameters3, new EventArgs());

            KeyboardFunctionStruct buttonParameters4 = new KeyboardFunctionStruct();
            buttonParameters4.Parameter = "1";
            buttonParameters4.Function = "FCONTA_N";
            ItemsSelectionWT.GetInstance().ButtonClicked(buttonParameters4, new EventArgs());




        }
    }
}