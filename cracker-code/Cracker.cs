using System;
using System.Linq;
using System.Drawing;
using System.Threading;
using System.Diagnostics;
using System.Windows.Forms;
using System.Runtime.InteropServices;

public static class Cracker
{
    static Process chrome;
    static Form console;
    static RichTextBox rtb;

    /// <summary>
    /// Botão esquerdo do cursor é pressionado
    /// </summary>
    public static void MouseLeftDown()
    {
        const int MOUSEEVENTF_LEFTDOWN = 0x02;

        uint ClickX = Convert.ToUInt32(Cursor.Position.X);
        uint ClickY = Convert.ToUInt32(Cursor.Position.Y);
        mouse_event(MOUSEEVENTF_LEFTDOWN, ClickX, ClickY, 0, 0);
    }

    /// <summary>
    /// Botão esquerdo do cursor é liberado
    /// </summary>
    public static void MouseLeftUp()
    {
        const int MOUSEEVENTF_LEFTUP = 0x04;

        uint ClickX = Convert.ToUInt32(Cursor.Position.X);
        uint ClickY = Convert.ToUInt32(Cursor.Position.Y);
        mouse_event(MOUSEEVENTF_LEFTUP, ClickX, ClickY, 0, 0);
    }

    /// <summary>
    /// Botão direito do cursor é pressionado
    /// </summary>
    public static void MouseRightDown()
    {
        const int MOUSEEVENTF_RIGHTDOWN = 0x08;

        uint ClickX = Convert.ToUInt32(Cursor.Position.X);
        uint ClickY = Convert.ToUInt32(Cursor.Position.Y);
        mouse_event(MOUSEEVENTF_RIGHTDOWN, ClickX, ClickY, 0, 0);
    }

    /// <summary>
    /// Botão direito do cursor é liberado
    /// </summary>
    public static void MouseRightUp()
    {
        const int MOUSEEVENTF_RIGHTUP = 0x10;

        uint ClickX = Convert.ToUInt32(Cursor.Position.X);
        uint ClickY = Convert.ToUInt32(Cursor.Position.Y);
        mouse_event(MOUSEEVENTF_RIGHTUP, ClickX, ClickY, 0, 0);
    }

    /// <summary>
    /// Inicializa a aplicação
    /// </summary>
    public static void Init(Action impl)
    {
        Thread.CurrentThread.SetApartmentState(ApartmentState.Unknown);
        Thread.CurrentThread.SetApartmentState(ApartmentState.STA);

        ApplicationConfiguration.Initialize();

        console = new Form();
        console.TopMost = true;
        console.Width = 600;
        console.Height = 400;
        console.StartPosition = FormStartPosition.Manual;
        console.Location = new Point(Screen.PrimaryScreen.Bounds.Width - console.Width - 50, 50);
        console.BackColor = Color.Black;

        rtb = new RichTextBox();
        rtb.ForeColor = Color.White;
        rtb.BackColor = Color.Black;
        rtb.ReadOnly = true;
        rtb.Dock = DockStyle.Fill;
        console.Controls.Add(rtb);

        console.Load += delegate
        {
            Process cmd = new Process();
            cmd.StartInfo.FileName = "cmd.exe";
            cmd.StartInfo.RedirectStandardInput = true;
            cmd.StartInfo.RedirectStandardOutput = true;
            cmd.StartInfo.CreateNoWindow = true;
            cmd.StartInfo.UseShellExecute = false;
            cmd.Start();

            cmd.StandardInput.WriteLine(@$"start chrome {Environment.CurrentDirectory}\..\real-page\index.html");

            while (chrome is null)
                chrome = getChrome();
            SetForegroundWindow(chrome.MainWindowHandle);

            impl();
        };

        Application.Run(console);
    }

    /// <summary>
    /// Print no console
    /// </summary>
    public static void Print(object text)
        => rtb.Text += text.ToString() + "\n";

    /// <summary>
    /// Espere um tempo
    /// </summary>
    public static void Wait(int milli = 50)
        => Thread.Sleep(milli);

    /// <summary>
    /// Mova o cursor para
    /// </summary>
    public static void MoveTo(int x, int y)
        => Cursor.Position = new Point(x, y);

    /// <summary>
    /// Pegue a posição do cursor
    /// </summary>
    public static (int x, int y) GetPosition()
        => (Cursor.Position.X, Cursor.Position.Y);

    /// <summary>
    /// Copie algo para a clipboard
    /// </summary>
    public static void Copy(string text)
        => Clipboard.SetText(text);

    /// <summary>
    /// Cole algo da clipboard
    /// </summary>
    public static void Paste()
    {
        var text = Clipboard.GetText();
        Write(text);
    }

    /// <summary>
    /// Digite algo com o teclado
    /// </summary>
    public static void Write(string text)
    {
        SetForegroundWindow(chrome.MainWindowHandle);
        SendKeys.SendWait(text);
    }

    /// <summary>
    /// Encerre a aplicação imediatamente
    /// </summary>
    public static void Exit()
        => console.Close();

    static Process getChrome()
    {
        var processes = Process.GetProcessesByName("chrome");
        var chrome = processes.FirstOrDefault();

        return chrome;
    }

    [DllImport("user32.dll")]
    static extern int SetForegroundWindow(IntPtr hWnd);

    [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
    static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint cButtons, uint dwExtraInfo);


}