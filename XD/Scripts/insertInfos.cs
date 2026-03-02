using System;
using Gtk;
using System.Data;
using XDPeople.Data;
using XDPeople.Entities;
using XDPeople.Business;

namespace XRest
{
    public class Script
    {
        private static Entry _inputData = new Entry();
        private static Window _window = new Window("");
        private static Label _title;
        private static VBox _vbox = new VBox();
        private static HBox _hbox = new HBox();
        private static Button BtnOk { get; set; }
        private static Button BtnCancel { get; set; }

        public static void Execute(ScriptHost host)
        {
            try
            {
                SetInputData("Insira o nome do cliente");
                SetConfigOfInputData();
                SetConfigOfMainWindow();
                ImplementBtns("Confirmar", "Cancelar");
                HboxPackStart();
                VboxPackStart();
                _window.Add(_vbox);
                _window.ShowAll();
                EventsOnClickInBtns();
            }
            catch (Exception e)
            {
                MessageDG.ShowExclamation(null, "Erro", Convert.ToString(e));
            }
        }

        protected static void SetInputData(string contentLabel)
        {
            _title = new Label(contentLabel);
            _inputData = new Entry();
        }

        protected static void SetConfigOfInputData()
        {
            _inputData.SetSizeRequest(300, 70);
            _inputData.Xalign = 2;
        }

        protected static void SetConfigOfMainWindow()
        {
            _window.WindowPosition = WindowPosition.CenterAlways;
        }

        protected static void VboxPackStart()
        {
            _vbox.PackStart(_title, true, true, 5);
            _vbox.PackStart(_inputData, false, false, 5);
            _vbox.PackStart(_hbox, true, true, 10);
        } 

        protected static void HboxPackStart()
        {
            _hbox.PackStart(BtnOk, true, true, 10);
            _hbox.PackStart(BtnCancel, true, true, 10);
        }

        protected static void ImplementBtns(string confirm, string cancel)
        {
            BtnOk = new Button(confirm);
            BtnCancel = new Button(cancel);
        }

        protected static void EventsOnClickInBtns()
        {
            BtnCancel.Clicked += delegate
            {
                _window.Destroy();
            };

            BtnOk.Clicked += ShowTextInsertedOnClick;
        }

        protected static void ShowTextInsertedOnClick(object sender, EventArgs args)
        {
            if (string.IsNullOrEmpty(_inputData.Text) != true)
            {
                MessageDG.ShowSuccess(null, "Sucesso", _inputData.Text);
                _window.Destroy();
            }
        }
    }
}