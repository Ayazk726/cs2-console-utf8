using System.Runtime.InteropServices;
using System.Text;
using CounterStrikeSharp.API.Core;
using Microsoft.Extensions.Logging;

namespace ConsoleUtf8;

public class ConsoleUtf8Plugin : BasePlugin
{
    public override string ModuleName => "ConsoleUtf8";
    public override string ModuleVersion => "1.0.1";
    public override string ModuleAuthor => "Amaryo";
    public override string ModuleDescription => "Switches the server console to UTF-8 so Chinese/Unicode text displays correctly.";

    private const uint Utf8CodePage = 65001;

    private bool _logged;

    [DllImport("kernel32.dll", SetLastError = true)]
    private static extern bool SetConsoleOutputCP(uint codePage);

    [DllImport("kernel32.dll", SetLastError = true)]
    private static extern bool SetConsoleCP(uint codePage);

    public override void Load(bool hotReload) => Apply();

    public override void OnAllPluginsLoaded(bool hotReload) => Apply();

    public override void Unload(bool hotReload)
    {
    }

    // cs2.exe creates its own console with the system code page (936 on zh-CN).
    // This plugin runs inside that process and switches it to UTF-8; the .NET
    // console writers are updated too so plugin logs stay readable.
    private void Apply()
    {
        try
        {
            SetConsoleOutputCP(Utf8CodePage);
            SetConsoleCP(Utf8CodePage);

            var utf8 = new UTF8Encoding(encoderShouldEmitUTF8Identifier: false);
            Console.OutputEncoding = utf8;
            Console.InputEncoding = utf8;

            if (!_logged)
            {
                _logged = true;
                Logger.LogInformation("[ConsoleUtf8] plugin loaded");
            }
        }
        catch (Exception ex)
        {
            Logger.LogWarning("[ConsoleUtf8] could not switch the console code page: {0}", ex.Message);
        }
    }
}
