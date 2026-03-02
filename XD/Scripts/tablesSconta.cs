using System;
using System.IO;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using Gtk;
using XDPeople;
using XDPeople.Business;
using XDPeople.Data;
using XDPeople.Entities;
using XDPeople.Entities.Brazil;
using XDPeople.License;
using XDPeople.Scales;
using XDPeople.Utils;
using XRest;
using XRest.Util;
using System.Text.RegularExpressions;
using GLib;
using Pango;


namespace XRest
{
    public class Script
    {
        private static TablesSubtotalUI tablesSubtotalUI;

        public Script()
        {
            tablesSubtotalUI = new TablesSubtotalUI();
        }

        public static void Execute(ScriptHost host)
        {
            try
            {
                new Script();                
            }
            catch (Exception e)
            {
                MessageDG.ShowError(null, "Erro", Convert.ToString(e));
            }
        }
    }

    public class QueryRecord
    {
        private string _query;

        public QueryRecord(string query)
        {
            _query = query;
        }

        public List<DataRow> GetDataRow()
        {
            try
            {
                List<DataRow> Rows = new List<DataRow>();
                System.Data.DataTable dataTable = Db.CurrentDatabase.GetDataTable(_query);
                foreach (DataRow row in dataTable.Rows)
                {
                    Rows.Add(row);
                }

                if (Rows.Count != 0)
                {
                    return Rows;
                }
                return null;
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }

    public class TablesSubtotal
    {
        private string[] _tables;
        
        public TablesSubtotal(string insertedTables)
        {
            _tables = insertedTables.Split(',');
        }

        public List<DataRow> GetValidTable(int table)
        {
            try
            {
                string query = "SELECT CreationUserId, Terminal " +
                "FROM tmpdocumentstables " +
                "WHERE SaleZoneAreaObjectId = " + table + ";";

                return new QueryRecord(query).GetDataRow();
            }
            catch(Exception e)
            {
                return null;
            }            
        }

        public void Subtotal()
        {
            List<int> unopenedTables = new List<int>();
            foreach (string table in _tables)
            {
                int tableNumber = 0;
                int.TryParse(table, out tableNumber);

                List<DataRow> dataRow = GetValidTable(tableNumber);

                if (dataRow != null)
                {
                    int employeeNumber;
                    int terminal;

                    int.TryParse(dataRow[0][0].ToString(), out employeeNumber);
                    int.TryParse(dataRow[0][1].ToString(), out terminal);

                    ItemsSelectionWT.GetInstance().SCONTA(tableNumber, employeeNumber, false, terminal, null, false, null);
                }
                else
                {
                    unopenedTables.Add(tableNumber);
                }                
            }

            ShowUnopenedTables(unopenedTables);
        }

        private void ShowUnopenedTables(List<int> unopenedTables)
        {
            string unopenedTablesFormated = "";
            int count = 0;
            foreach (int unopenedTable in unopenedTables)
            {
                count++;
                if (count == unopenedTables.Count && unopenedTables.Count != 1)
                {
                    unopenedTablesFormated += "e " + unopenedTable;
                }
                else if(count == unopenedTables.Count - 1)
                {
                    unopenedTablesFormated += unopenedTable + " ";
                }
                else if(count != 1)
                {
                    unopenedTablesFormated += unopenedTable + ", ";
                }
                else
                {
                    unopenedTablesFormated += unopenedTable;
                }
            }

            if (unopenedTables.Count == 1)
            {
                MessageDG.ShowExclamation(null, "Aviso", "A Mesa " + unopenedTablesFormated + " não está aberta!");
            }
            else if(unopenedTables.Count != 0)
            {
                MessageDG.ShowExclamation(null, "Aviso", "As Mesas " + unopenedTablesFormated + " não estão abertas!");
            }
        }
    }

    public class TablesSubtotalUI
    {
        private Window _windowSubtotal;
        private Label _labelTitle;
        private Label _labelExample;
        private VBox _vboxMain;
        private HBox _hboxExample;
        private HBox _hboxButtons;
        private HBox _hboxEntryTables;
        private EntryXD _entryTables;
        private XDctrButtonWT _btnOk;
        private XDctrButtonWT _btnCancel;              
        private Fixed _fix;

        public TablesSubtotalUI()
        {
            Build();
        }

        private void SetConfigOfWindowSubtotal()
        {
            _windowSubtotal = new Window("");
            _fix = new Fixed();
            SkinUtilsUI.Background.SetFixedBackgroundColor(_fix, 700, 500);
            _windowSubtotal.Add(_fix);
            _windowSubtotal.WindowPosition = WindowPosition.CenterAlways;            
            _windowSubtotal.DefaultSize = new Gdk.Size(700, 500);
            _windowSubtotal.ModifyBg(Gtk.StateType.Normal, SkinUtilsUI.LoadedSkin.SkinBox.BackgroundColor);
            _windowSubtotal.Decorated = false;
        }

        private void SetConfigOfLabels()
        {
            _labelTitle = new Label();
            _labelTitle.LabelProp = string.Format("<b>" + "Pré-Conta de Mesas" + "</b>");
            _labelTitle.UseMarkup = true;
            SkinUtilsUI.Font.SetLabelFont(_labelTitle, false, SkinUtilsUI.Font.FontSize.H1);
            SkinUtilsUI.Font.SetLabelFontColor(_labelTitle, "White");

            _labelExample = new Label();
            _labelExample.Text = "Exemplo: 1,2,3,4,5,6,7...";
            _labelExample.ModifyFont(FontDescription.FromString(SkinUtilsUI.LoadedSkin.SkinFontName + " " + 12));
            SkinUtilsUI.Font.SetLabelFontColor(_labelExample, "White");
        }

        private void SetConfigOfEntrys()
        {
            _entryTables = new EntryXD();
            _entryTables.MaxLength = 100;
            _entryTables.SetSizeRequest(500, 50);
            _entryTables.ModifyFont(FontDescription.FromString(SkinUtilsUI.LoadedSkin.SkinFontName + " " + 17));
            Gdk.Color cursorColor = new Gdk.Color(0,0,10);
            Gdk.Color textColor = new Gdk.Color(0, 0, 0);
            _entryTables.ModifyCursor(cursorColor, textColor);
            _entryTables.ModifyBase(Gtk.StateType.Normal, new Gdk.Color(255, 255, 255));
        }        

        private void ImplementBtns()
        {
            _btnOk = new XDctrButtonWT(ButtonType.Ok);
            _btnOk.Text = "Confirmar";
            _btnOk.FontSize = 13;
            _btnOk.HeightRequest = 50;
            _btnCancel = new XDctrButtonWT(ButtonType.Exit);
            _btnCancel.Text = "Cancelar";            
            _btnCancel.FontSize = 13;
            _btnCancel.LabelBackground = new Gdk.Color(238, 119, 27);
        }

        private void HboxPackStart()
        {
            _hboxEntryTables = new HBox();            
            _hboxEntryTables.PackStart(_entryTables, true, true, 20);            
            _hboxButtons = new HBox();
            _hboxButtons.PackStart(_btnOk, true, true, 20);
            _hboxButtons.PackStart(_btnCancel, true, true, 20);
            _hboxExample = new HBox();
            _hboxExample.PackStart(_labelExample, false, false, 20);
        }

        private void VboxPackStart()
        {
            _vboxMain = new VBox();
            _vboxMain.PackStart(_labelTitle, true, true, 30);
            _vboxMain.PackStart(_hboxExample, false, false, 30);
            _vboxMain.PackStart(_hboxEntryTables, true, true, 0);
            _vboxMain.PackStart(_hboxButtons, true, true, 45);           
        }

        private void EventHandlers()
        {
            _btnCancel.ButtonClicked += OnCLickBtnCancel;
            _btnOk.ButtonClicked += OnClickBtnOk;
            _windowSubtotal.KeyReleaseEvent += OnKeyPressEscape;
            _entryTables.Activated += OnActiveEntryCustomerName;
        }

        protected virtual void OnClickBtnOk(object sender, EventArgs args)
        {
            if (!string.IsNullOrEmpty(_entryTables.Text))
            {
                _windowSubtotal.Hide();
                TablesSubtotal tablesSubtotal = new TablesSubtotal(_entryTables.Text);
                tablesSubtotal.Subtotal();
                _windowSubtotal.ShowAll();
            }
            else
            {
                _windowSubtotal.Hide();
                MessageDG.ShowExclamation(null, "Aviso", "Insira pelo menos uma mesa!");
                _windowSubtotal.Show();
            } 
        }

        protected virtual void OnCLickBtnCancel(object sender, EventArgs args)
        {
            _windowSubtotal.Destroy();
        }

        protected virtual void OnKeyPressEnter(object sender, KeyReleaseEventArgs args)
        {            
            if (args.Event.Key == Gdk.Key.Return)
            {
                _entryTables.KeyReleaseEvent -= OnKeyPressEnter;
                OnClickBtnOk(null, null);                
            }
        }

        protected virtual void OnKeyPressEscape(object sender, KeyReleaseEventArgs args)
        {
            if (args.Event.Key == Gdk.Key.Escape)
            {
                _windowSubtotal.Destroy();
            }
        }

        protected virtual void OnActiveEntryCustomerName(object sender, EventArgs args)
        {
            _entryTables.KeyReleaseEvent += OnKeyPressEnter;
        }

        private void Build()
        {
            SetConfigOfLabels();
            SetConfigOfEntrys();
            SetConfigOfWindowSubtotal();
            ImplementBtns();
            HboxPackStart();
            VboxPackStart();
            EventHandlers();
            _fix.Put(_vboxMain, 70, 50);
            _windowSubtotal.ShowAll();            
        }
    }

}