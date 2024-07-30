using System.Text;
using System.Threading.Tasks;
using Windows;
using WinRT;
using Microsoft.Windows.ApplicationModel.DynamicDependency;
using Microsoft.UI.Xaml;
using WASLoaderTestApp;

namespace WASLoaderTest
{
    public static class Program
    {
        internal static void Main(string[] args)
        {
            if (!RuntimeHelper.IsMSIX)
            {
                if (!WARInitializerCs.InitializeWAR(out int hresult))
                {
                    System.Environment.Exit(hresult);
                }
            }
            ComWrappersSupport.InitializeComWrappers();
            Application.Start((p) => new App());
            return;
        }
    }
}
