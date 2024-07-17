using System.Text;
using System.Threading.Tasks;
using WinRT;
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
                if (!WARInitializer.InitializeWAR(out int hresult))
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
