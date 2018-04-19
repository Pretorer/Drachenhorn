﻿using DSACharacterSheet.Core.Settings;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using DSACharacterSheet.Core.Images;
using GalaSoft.MvvmLight.Ioc;

namespace DSACharacterSheet.Desktop.Views
{
    /// <summary>
    /// Interaktionslogik für SettingsView.xaml
    /// </summary>
    public partial class SettingsView : Window
    {
        public SettingsView()
        {
            InitializeComponent();

            this.Loaded += (sender, args) => { LanguageComboBox_SelectionChanged(sender, null); };
        }

        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri));
            e.Handled = true;
        }

        private void GitCommit_Click(object sender, RoutedEventArgs e)
        {
            if (!(sender is Button))
                return;

            var button = (Button)sender;
            if (button.Content.ToString() != "Application not published")
                Process.Start(new ProcessStartInfo(@"https://github.com/lightlike/DSACharacterSheet/commit/" + button.Content));
        }

        private void LanguageComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (FlagImage == null) return;

            try
            {
                var culture = SimpleIoc.Default.GetInstance<ISettings>().CurrentCulture.Name;
                var flag = typeof(Flags).GetProperty(culture.Replace('-','_'))?.GetValue(null) as byte[];
                FlagImage.Source = LoadImage(flag);
            }
            catch (IOException)
            {
                FlagImage.Source = LoadImage(Flags.invariant);
            }
        }

        private static BitmapImage LoadImage(byte[] imageData)
        {
            if (imageData == null || imageData.Length == 0) return null;
            var image = new BitmapImage();
            using (var mem = new MemoryStream(imageData))
            {
                mem.Position = 0;
                image.BeginInit();
                image.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.UriSource = null;
                image.StreamSource = mem;
                image.EndInit();
            }
            image.Freeze();
            return image;
        }
    }
}