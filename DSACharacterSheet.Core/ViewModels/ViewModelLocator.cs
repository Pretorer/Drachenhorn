using DSACharacterSheet.Core.Lang;
using GalaSoft.MvvmLight.Ioc;

namespace DSACharacterSheet.Core.ViewModels
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// </summary>
    public class ViewModelLocator
    {
        /// <summary>
        /// Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        public ViewModelLocator()
        {
            if (ViewModelBase.IsInDesignModeStatic)
            {
                SimpleIoc.Default.Register<LanguageManager>();
            }
            else
            {
                SimpleIoc.Default.Register(() => new LanguageManager());
            }

            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<PrintViewModel>();
            SimpleIoc.Default.Register<SettingsViewModel>();
            SimpleIoc.Default.Register<CoatOfArmsViewModel>();
        }

        public MainViewModel MainView => SimpleIoc.Default.GetInstance<MainViewModel>();

        public PrintViewModel PrintView => SimpleIoc.Default.GetInstance<PrintViewModel>();

        public SettingsViewModel SettingsView => SimpleIoc.Default.GetInstance<SettingsViewModel>();

        public CoatOfArmsViewModel CoatOfArmsView => SimpleIoc.Default.GetInstance<CoatOfArmsViewModel>();

        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}