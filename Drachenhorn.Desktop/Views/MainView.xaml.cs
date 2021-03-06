﻿using Drachenhorn.Core.Printing;
using Drachenhorn.Core.ViewModels;
using Drachenhorn.Xml.Sheet;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using Drachenhorn.Core.Lang;
using Drachenhorn.Core.ViewModels.Common;
using Drachenhorn.Core.ViewModels.Sheet;
using Drachenhorn.Desktop.UI.Dialogs;
using Fluent;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;

namespace Drachenhorn.Desktop.Views
{
    /// <inheritdoc cref="Window" />
    /// <summary>
    /// Interaktionslogik für MainView.xaml
    /// </summary>
    public partial class MainView : RibbonWindow
    {
        public MainView(string path)
        {
            InitializeComponent();

            //Menu.Background = SystemParameters.WindowGlassBrush != null ? SystemParameters.WindowGlassBrush : new SolidColorBrush(Colors.Green);

            if (!String.IsNullOrEmpty(path)) this.Loaded += (sender, args) => OpenFile(path);

            Messenger.Default.Register<NotificationMessage>(this, RecieveMessage);


            SimpleIoc.Default.GetInstance<LanguageManager>().LanguageChanged += (sender, args) =>
            {
                RibbonLocalization.Current.Culture = args.NewCulture;
            };
        }

        public void OpenFile(string path)
        {
            var temp = new Uri(path).LocalPath;
            if (temp.EndsWith(CharacterSheet.Extension))
            {
                if (this.DataContext is MainViewModel)
                {
                    var model = (MainViewModel)this.DataContext;
                    var sheetModel = new CharacterSheetViewModel(CharacterSheet.Load(path));
                    model.CharacterSheetViewModels.Add(sheetModel);
                    model.CurrentSheetViewModel = sheetModel;
                }
            }

            this.WindowState = WindowState.Maximized;
            this.Activate();
        }

        private void RecieveMessage(NotificationMessage message)
        {
            if (message.Notification == "ShowSettingsView")
                new SettingsView().ShowDialog();
            else if (message.Notification == "ShowPrintView")
            {
                if (this.DataContext is MainViewModel)
                {
                    var model = (MainViewModel)this.DataContext;
                    var print = PrintingManager.GenerateHtml(model.CurrentSheetViewModel.CurrentSheet);
                    new PrintView(print).ShowDialog();
                }
            }
            else if (message.Notification == "ShowOpenTemplates")
                new TemplateSelectorDialog().ShowDialog();
        }

        private void MainView_OnClosing(object sender, CancelEventArgs e)
        {
            if (this.DataContext is MainViewModel)
            {
                var model = (MainViewModel) this.DataContext;

                if (!model.CharacterSheetViewModels.Any(x => x.CurrentSheet.HasChanged)) return;

                var task = SimpleIoc.Default.GetInstance<IDialogService>().ShowMessage(
                    LanguageManager.Translate("UI.SouldCloseBunch"),
                    LanguageManager.Translate("UI.SouldCloseBunch.Caption"),
                    LanguageManager.Translate("UI.Yes"),
                    LanguageManager.Translate("UI.No"), null);

                if (!task.Result)
                    e.Cancel = true;
            }
        }
    }
}