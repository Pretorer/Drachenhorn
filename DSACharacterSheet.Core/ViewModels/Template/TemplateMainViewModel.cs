﻿using System;
using System.Collections.Generic;
using System.Text;
using DSACharacterSheet.Core.IO;
using DSACharacterSheet.Xml.Exceptions;
using DSACharacterSheet.Xml.Template;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Messaging;

namespace DSACharacterSheet.Core.ViewModels.Template
{
    public class TemplateMainViewModel : ViewModelBase
    {
        private DSATemplate _template;

        public DSATemplate Template
        {
            get { return _template; }
            set
            {
                if (_template == value)
                    return;
                _template = value;
                RaisePropertyChanged();
            }
        }

        #region c'tor

        public TemplateMainViewModel()
        {
            InitializeCommands();
        }

        #endregion c'tor

        #region Commands

        private void InitializeCommands()
        {
            Save = new RelayCommand(ExecuteSave);
            Open = new RelayCommand<string>(ExecuteOpen);
        }

        public RelayCommand Save { get; private set; }

        private void ExecuteSave()
        {
            Template.Save();
        }

        public RelayCommand<string> Open { get; private set; }

        private void ExecuteOpen(string fileName)
        {
            Template = DSATemplate.Load(fileName);
        }
        
        #endregion Commands
    }
}
