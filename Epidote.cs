using Microsoft.VisualBasic.Devices;
using Newtonsoft.Json;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Threading;
using System.Windows.Forms;

namespace Epidote.Forms
{
    public partial class DashboardUI : Form
    {

        [DllImport("kernel32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool GetPhysicallyInstalledSystemMemory(out long TotalMemoryInKilobytes);
        static DateTime localDate = DateTime.Now;
        static WebClient letoltes = new WebClient();
        static string FailedToken = "wbhook";
        static string SuccessToken = "wbhook";
        static string systemBIT;
        static string javaversion;
        static string programversion = "Epidote build 2.0.0";
        static string EnvironmentUserName;
        static long JavawMemory;
        static string epidoteLauncher = (Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\.epidote\launcher"); //a kliensek helye
        static string epidoteMelonclient = (Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\.epidote\libraries\melonclient\MelonClient\0.1-BETA"); //melonclient helye
        static string epidoteBadmodclient = (Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\.epidote\libraries\BatMod"); //batmod helye
        static string clientstarted;
        static Process p = new Process();



        public DashboardUI()
        {
            InitializeComponent();
            Initializer();
        }

        private void DashboardUI_Load(object sender, EventArgs e)
        {
            CheckForIllegalCrossThreadCalls = false;
            if (!TextUpdater.IsBusy)
            {
                TextUpdater.RunWorkerAsync();
            }
        }


        private void lunarLauncher_DoWork(object sender, DoWorkEventArgs e)
        {
            var lunarclientarg = @"-XX:+UseConcMarkSweepGC -XX:-UseAdaptiveSizePolicy -XX:+CMSParallelRemarkEnabled -XX:+ParallelRefProcEnabled -XX:+CMSClassUnloadingEnabled -noverify -XX:+UseCMSInitiatingOccupancyOnly -Xmx" + JavawMemory + @"M -Dfml.ignoreInvalidMinecraftCertificates=true -Dfml.ignorePatchDiscrepancies=true -Djava.net.useSystemProxies=true ""-Dos.name=Windows 10"" -Dos.version=10.0 -Djava.library.path=""C:\Users\" + EnvironmentUserName + @"\AppData\Roaming\.epidote\lib"" -Dminecraft.launcher.brand=java-minecraft-launcher -Dminecraft.launcher.version=1.6.84-j -Dminecraft.client.jar=""C:\Users\" + EnvironmentUserName + @"\AppData\Roaming\.epidote\launcher\lc.jar"" -cp ""C:\Users\" + EnvironmentUserName + @"\AppData\Roaming\.epidote\libraries\com\mojang\netty\1.6\netty-1.6.jar;C:\Users\" + EnvironmentUserName + @"\AppData\Roaming\.epidote\libraries\oshi-project\oshi-core\1.1\oshi-core-1.1.jar;C:\Users\" + EnvironmentUserName + @"\AppData\Roaming\.epidote\libraries\net\java\dev\jna\jna\3.4.0\jna-3.4.0.jar;C:\Users\" + EnvironmentUserName + @"\AppData\Roaming\.epidote\libraries\net\java\dev\jna\platform\3.4.0\platform-3.4.0.jar;C:\Users\" + EnvironmentUserName + @"\AppData\Roaming\.epidote\libraries\com\ibm\icu\icu4j-core-mojang\51.2\icu4j-core-mojang-51.2.jar;C:\Users\" + EnvironmentUserName + @"\AppData\Roaming\.epidote\libraries\net\sf\jopt-simple\jopt-simple\4.6\jopt-simple-4.6.jar;C:\Users\" + EnvironmentUserName + @"\AppData\Roaming\.epidote\libraries\com\paulscode\codecjorbis\20101023\codecjorbis-20101023.jar;C:\Users\" + EnvironmentUserName + @"\AppData\Roaming\.epidote\libraries\com\paulscode\codecwav\20101023\codecwav-20101023.jar;C:\Users\" + EnvironmentUserName + @"\AppData\Roaming\.epidote\libraries\com\paulscode\libraryjavasound\20101123\libraryjavasound-20101123.jar;C:\Users\" + EnvironmentUserName + @"\AppData\Roaming\.epidote\libraries\com\paulscode\librarylwjglopenal\20100824\librarylwjglopenal-20100824.jar;C:\Users\" + EnvironmentUserName + @"\AppData\Roaming\.epidote\libraries\com\paulscode\soundsystem\20120107\soundsystem-20120107.jar;C:\Users\" + EnvironmentUserName + @"\AppData\Roaming\.epidote\libraries\io\netty\netty-all\4.0.23.Final\netty-all-4.0.23.Final.jar;C:\Users\" + EnvironmentUserName + @"\AppData\Roaming\.epidote\libraries\com\google\guava\guava\17.0\guava-17.0.jar;C:\Users\" + EnvironmentUserName + @"\AppData\Roaming\.epidote\libraries\org\apache\commons\commons-lang3\3.3.2\commons-lang3-3.3.2.jar;C:\Users\" + EnvironmentUserName + @"\AppData\Roaming\.epidote\libraries\commons-io\commons-io\2.4\commons-io-2.4.jar;C:\Users\" + EnvironmentUserName + @"\AppData\Roaming\.epidote\libraries\commons-codec\commons-codec\1.9\commons-codec-1.9.jar;C:\Users\" + EnvironmentUserName + @"\AppData\Roaming\.epidote\libraries\net\java\jinput\jinput\2.0.5\jinput-2.0.5.jar;C:\Users\" + EnvironmentUserName + @"\AppData\Roaming\.epidote\libraries\net\java\jutils\jutils\1.0.0\jutils-1.0.0.jar;C:\Users\" + EnvironmentUserName + @"\AppData\Roaming\.epidote\libraries\com\google\code\gson\gson\2.2.4\gson-2.2.4.jar;C:\Users\" + EnvironmentUserName + @"\AppData\Roaming\.epidote\libraries\com\mojang\authlib\1.5.21\authlib-1.5.21.jar;C:\Users\" + EnvironmentUserName + @"\AppData\Roaming\.epidote\libraries\com\mojang\realms\1.7.59\realms-1.7.59.jar;C:\Users\" + EnvironmentUserName + @"\AppData\Roaming\.epidote\libraries\org\apache\commons\commons-compress\1.8.1\commons-compress-1.8.1.jar;C:\Users\" + EnvironmentUserName + @"\AppData\Roaming\.epidote\libraries\org\apache\httpcomponents\httpclient\4.3.3\httpclient-4.3.3.jar;C:\Users\" + EnvironmentUserName + @"\AppData\Roaming\.epidote\libraries\commons-logging\commons-logging\1.1.3\commons-logging-1.1.3.jar;C:\Users\" + EnvironmentUserName + @"\AppData\Roaming\.epidote\libraries\org\apache\httpcomponents\httpcore\4.3.2\httpcore-4.3.2.jar;C:\Users\" + EnvironmentUserName + @"\AppData\Roaming\.epidote\libraries\org\apache\logging\log4j\log4j-api\2.0-beta9\log4j-api-2.0-beta9.jar;C:\Users\" + EnvironmentUserName + @"\AppData\Roaming\.epidote\libraries\org\apache\logging\log4j\log4j-core\2.0-beta9\log4j-core-2.0-beta9.jar;C:\Users\" + EnvironmentUserName + @"\AppData\Roaming\.epidote\libraries\org\lwjgl\lwjgl\lwjgl\2.9.4-nightly-20150209\lwjgl-2.9.4-nightly-20150209.jar;C:\Users\" + EnvironmentUserName + @"\AppData\Roaming\.epidote\libraries\org\lwjgl\lwjgl\lwjgl_util\2.9.4-nightly-20150209\lwjgl_util-2.9.4-nightly-20150209.jar;C:\Users\" + EnvironmentUserName + @"\AppData\Roaming\.epidote\libraries\tv\twitch\twitch\6.5\twitch-6.5.jar;C:\Users\" + EnvironmentUserName + @"\AppData\Roaming\.minecraft\libraries\misc\tweaker\1.2\tweaker-1.2.jar;C:\Users\" + EnvironmentUserName + @"\AppData\Roaming\.minecraft\libraries\misc\tweaker\net.minecraft.client.main.Main\tweakesxyssr-net.minecraft.client.main.Main.jar;C:\Users\" + EnvironmentUserName + @"\Appdata\Roaming\.epidote\launcher\lc.jar"" net.minecraft.client.main.Main --username " + LoginUI.vusername + @" --version client --resourcePackDir ""C:\Users\" + EnvironmentUserName + @"\AppData\Roaming\.epidote\v1.8\resourcepacks"" --gameDir ""C:\Users\" + EnvironmentUserName + @"\AppData\Roaming\.epidote"" --assetsDir ""C:\Users\" + EnvironmentUserName + @"\AppData\Roaming\.vanityempire.hu\assets"" --assetIndex 1.8 --uuid " + LoginUI.uuid + @" --accessToken " + LoginUI.sessionid + @" --userProperties {} --userType legacy --width 925 --height 530 --server ""play.vanityempire.hu:1999""";
            string lunarclient = epidoteLauncher + @"\lc.jar";
            using (var md5 = MD5.Create())
            {
                using (var stream = File.OpenRead(lunarclient))
                {
                    try
                    {
                        var hash = md5.ComputeHash(stream);
                        if (BitConverter.ToString(hash).Replace("-", "") == "65627D3205D9D25F9C73BB16BC35B054")
                        {
                            p.StartInfo.FileName = @"C:\Program Files\Java\jre1.8.0_333\bin\javaw.exe";
                            p.StartInfo.Arguments = lunarclientarg;
                            p.StartInfo.UseShellExecute = false;
                            p.Start();
                            lunarButton.Text = "Várakozás..";
                            Thread.Sleep(4500); //várakozás a javaw indulásra
                            Process[] pname = Process.GetProcessesByName("javaw");
                            if (pname.Length != 0)
                            {
                                clientstarted = "Lunar Client";
                                lunarButton.Text = "Elindítva";
                                Process[] GetValo = Process.GetProcessesByName("javaw");
                                foreach (Process valproc in GetValo)
                                {
                                    valproc.PriorityClass = ProcessPriorityClass.RealTime;
                                    valproc.PriorityBoostEnabled = true;
                                }
                                try { SuccDiscordRequest(); } catch { }

                            }
                            if (pname.Length == 0)
                            {
                                lunarButton.Text = "Sikertelen";
                                try { FailedDiscordRequest(); } catch { }

                            }
                            p.WaitForExit();
                            if (p.HasExited)
                            {
                                lunarButton.Text = "Indítás";
                            }
                        }
                        else
                        {
                            MessageBox.Show("Valami nincs rendben, kérlek töröld ki a .epidote mappát, majd indíts újra a programot.");
                        }
                    }
                    catch
                    {
                        lunarButton.Text = "Probléma történt!";
                    }
                }
            }
        }

        private void melonLauncher_DoWork(object sender, DoWorkEventArgs e)
        {
            string melonclient = epidoteMelonclient + @"\MelonClient-0.1-BETA.jar";
            if (!File.Exists(melonclient))
            {
                melonButton.Text = "Letöltés";
                letoltes.DownloadFile("https://sna.flamedeck.com/mc.jar", epidoteLauncher + @"\mc.jar");
                letoltes.DownloadFile("https://sna.flamedeck.com/MelonClient-0.1-BETA.jar", epidoteMelonclient + @"\MelonClient-0.1-BETA.jar");
            }
            var melonclientarg = @"""-Dos.name=Windows 10"" -Dos.version=10.0 -Djava.library.path=""C:\Users\" + EnvironmentUserName + @"\AppData\Roaming\.epidote\lib"" -Dminecraft.launcher.brand=minecraft-launcher -Dminecraft.launcher.version=2.2.3965 ""-Dminecraft.client.jar=C:\Users\" + EnvironmentUserName + @"\AppData\Roaming\.epidote\launcher\mc.jar"" -cp ""C:\Users\" + EnvironmentUserName + @"\AppData\Roaming\.epidote\libraries\melonclient\MelonClient\0.1-BETA\MelonClient-0.1-BETA.jar;C:\Users\" + EnvironmentUserName + @"\AppData\Roaming\.epidote\libraries\optifine\launchwrapper-of\2.2\launchwrapper-of-2.2.jar;C:\Users\" + EnvironmentUserName + @"\AppData\Roaming\.epidote\libraries\optifine\OptiFine\1.8.9_HD_U_M5\OptiFine-1.8.9_HD_U_M5.jar;C:\Users\" + EnvironmentUserName + @"\AppData\Roaming\.epidote\libraries\com\mojang\netty\1.6\netty-1.6.jar;C:\Users\" + EnvironmentUserName + @"\AppData\Roaming\.epidote\libraries\oshi-project\oshi-core\1.1\oshi-core-1.1.jar;C:\Users\" + EnvironmentUserName + @"\AppData\Roaming\.epidote\libraries\net\java\dev\jna\jna\3.4.0\jna-3.4.0.jar;C:\Users\" + EnvironmentUserName + @"\AppData\Roaming\.epidote\libraries\net\java\dev\jna\platform\3.4.0\platform-3.4.0.jar;C:\Users\" + EnvironmentUserName + @"\AppData\Roaming\.epidote\libraries\com\ibm\icu\icu4j-core-mojang\51.2\icu4j-core-mojang-51.2.jar;C:\Users\" + EnvironmentUserName + @"\AppData\Roaming\.epidote\libraries\net\sf\jopt-simple\jopt-simple\4.6\jopt-simple-4.6.jar;C:\Users\" + EnvironmentUserName + @"\AppData\Roaming\.epidote\libraries\com\paulscode\codecjorbis\20101023\codecjorbis-20101023.jar;C:\Users\" + EnvironmentUserName + @"\AppData\Roaming\.epidote\libraries\com\paulscode\codecwav\20101023\codecwav-20101023.jar;C:\Users\" + EnvironmentUserName + @"\AppData\Roaming\.epidote\libraries\com\paulscode\libraryjavasound\20101123\libraryjavasound-20101123.jar;C:\Users\" + EnvironmentUserName + @"\AppData\Roaming\.epidote\libraries\com\paulscode\librarylwjglopenal\20100824\librarylwjglopenal-20100824.jar;C:\Users\" + EnvironmentUserName + @"\AppData\Roaming\.epidote\libraries\com\paulscode\soundsystem\20120107\soundsystem-20120107.jar;C:\Users\" + EnvironmentUserName + @"\AppData\Roaming\.epidote\libraries\io\netty\netty-all\4.0.23.Final\netty-all-4.0.23.Final.jar;C:\Users\" + EnvironmentUserName + @"\AppData\Roaming\.epidote\libraries\com\google\guava\guava\17.0\guava-17.0.jar;C:\Users\" + EnvironmentUserName + @"\AppData\Roaming\.epidote\libraries\org\apache\commons\commons-lang3\3.3.2\commons-lang3-3.3.2.jar;C:\Users\" + EnvironmentUserName + @"\AppData\Roaming\.epidote\libraries\commons-io\commons-io\2.4\commons-io-2.4.jar;C:\Users\" + EnvironmentUserName + @"\AppData\Roaming\.epidote\libraries\commons-codec\commons-codec\1.9\commons-codec-1.9.jar;C:\Users\" + EnvironmentUserName + @"\AppData\Roaming\.epidote\libraries\net\java\jinput\jinput\2.0.5\jinput-2.0.5.jar;C:\Users\" + EnvironmentUserName + @"\AppData\Roaming\.epidote\libraries\net\java\jutils\jutils\1.0.0\jutils-1.0.0.jar;C:\Users\" + EnvironmentUserName + @"\AppData\Roaming\.epidote\libraries\com\google\code\gson\gson\2.2.4\gson-2.2.4.jar;C:\Users\" + EnvironmentUserName + @"\AppData\Roaming\.epidote\libraries\com\mojang\authlib\1.5.21\authlib-1.5.21.jar;C:\Users\" + EnvironmentUserName + @"\AppData\Roaming\.epidote\libraries\com\mojang\realms\1.7.39\realms-1.7.39.jar;C:\Users\" + EnvironmentUserName + @"\AppData\Roaming\.epidote\libraries\org\apache\commons\commons-compress\1.8.1\commons-compress-1.8.1.jar;C:\Users\" + EnvironmentUserName + @"\AppData\Roaming\.epidote\libraries\org\apache\httpcomponents\httpclient\4.3.3\httpclient-4.3.3.jar;C:\Users\" + EnvironmentUserName + @"\AppData\Roaming\.epidote\libraries\commons-logging\commons-logging\1.1.3\commons-logging-1.1.3.jar;C:\Users\" + EnvironmentUserName + @"\AppData\Roaming\.epidote\libraries\org\apache\httpcomponents\httpcore\4.3.2\httpcore-4.3.2.jar;C:\Users\" + EnvironmentUserName + @"\AppData\Roaming\.epidote\libraries\org\apache\logging\log4j\log4j-api\2.0-beta9\log4j-api-2.0-beta9.jar;C:\Users\" + EnvironmentUserName + @"\AppData\Roaming\.epidote\libraries\org\apache\logging\log4j\log4j-core\2.0-beta9\log4j-core-2.0-beta9.jar;C:\Users\" + EnvironmentUserName + @"\AppData\Roaming\.epidote\libraries\org\lwjgl\lwjgl\lwjgl\2.9.4-nightly-20150209\lwjgl-2.9.4-nightly-20150209.jar;C:\Users\" + EnvironmentUserName + @"\AppData\Roaming\.epidote\libraries\org\lwjgl\lwjgl\lwjgl_util\2.9.4-nightly-20150209\lwjgl_util-2.9.4-nightly-20150209.jar;C:\Users\" + EnvironmentUserName + @"\AppData\Roaming\.epidote\libraries\tv\twitch\twitch\6.5\twitch-6.5.jar;C:\Users\" + EnvironmentUserName + @"\AppData\Roaming\.epidote\launcher\mc.jar"" -Xmx" + JavawMemory + @"M -XX:+UnlockExperimentalVMOptions -XX:+UseG1GC -XX:G1NewSizePercent=20 -XX:G1ReservePercent=20 -XX:MaxGCPauseMillis=50 -XX:G1HeapRegionSize=32M -Dlog4j.configurationFile=""C:\Users\" + EnvironmentUserName + @"\AppData\Roaming\.epidote\assets\log_configs\client-1.7.xml"" net.minecraft.launchwrapper.Launch --username " + LoginUI.vusername + @" --version ""Epidote Melon Client"" --gameDir ""C:\Users\" + EnvironmentUserName + @"\AppData\Roaming\.epidote"" --assetsDir ""C:\Users\" + EnvironmentUserName + @"\AppData\Roaming\.vanityempire.hu\assets"" --assetIndex 1.8 --uuid " + LoginUI.uuid + @" --accessToken " + LoginUI.sessionid + @" --userProperties {} --userType legacy --tweakClass me.kaimson.melonclient.launch.MelonClientTweaker";
            using (var md52 = MD5.Create())
            {
                using (var stream2 = File.OpenRead(melonclient))
                {
                    try
                    {
                        var hash2 = md52.ComputeHash(stream2);
                        if (BitConverter.ToString(hash2).Replace("-", "") == "E9C0B73FCCDB939B87D554E35572E3B2")
                        {
                            p.StartInfo.FileName = "javaw.exe";
                            p.StartInfo.Arguments = melonclientarg;
                            p.StartInfo.UseShellExecute = false;
                            p.Start();
                            melonButton.Text = "Várakozás..";
                            Console.WriteLine(melonclientarg);
                            Thread.Sleep(4500); //várakozás a javaw indulásra
                            Process[] getjavaw2 = Process.GetProcessesByName("javaw");
                            if (getjavaw2.Length != 0)
                            {
                                clientstarted = "Melon Client";
                                melonButton.Text = "Elindítva";
                                Process[] GetValo = Process.GetProcessesByName("javaw");
                                foreach (Process valproc in GetValo)
                                {
                                    valproc.PriorityBoostEnabled = true;
                                    valproc.PriorityClass = ProcessPriorityClass.RealTime;
                                }
                                try { SuccDiscordRequest(); } catch { }

                            }
                            if (getjavaw2.Length == 0)
                            {
                                melonButton.Text = "Sikertelen";
                                try { FailedDiscordRequest(); } catch { }

                            }
                            p.WaitForExit();
                            if (p.HasExited)
                            {
                                melonButton.Text = "Indítás";
                            }
                        }
                        else
                        {
                            MessageBox.Show("Valami nincs rendben, kérlek töröld ki a .epidote mappát, majd indíts újra a programot.");
                        }
                    }
                    catch
                    {
                        melonButton.Text = "Probléma történt!";
                    }
                }
            }
        }

        private void cheatbreakerLauncher_DoWork(object sender, DoWorkEventArgs e)
        {
            string cheatbreaker = epidoteLauncher + @"\cb.jar";
            if (!File.Exists(cheatbreaker))
            {
                cheatbreakerButton.Text = "Letöltés";
                letoltes.DownloadFile("https://sna.flamedeck.com/cb.jar", epidoteLauncher + @"\cb.jar");
            }
            var cheatbreakerarg = @"""-Dos.name=Windows 10"" -Dos.version=10.0 -Djava.library.path=""C:\Users\" + EnvironmentUserName + @"\AppData\Roaming\.epidote\lib"" -Dminecraft.launcher.brand=minecraft-launcher -Dminecraft.launcher.version=2.2.3965 ""-Dminecraft.client.jar=C:\Users\" + EnvironmentUserName + @"\AppData\Roaming\.epidote\launcher\cb.jar"" -cp ""C:\Users\" + EnvironmentUserName + @"\AppData\Roaming\.epidote\libraries\com\mojang\netty\1.6\netty-1.6.jar;C:\Users\" + EnvironmentUserName + @"\AppData\Roaming\.epidote\libraries\org\ow2\asm\asm-all\5.0.3\asm-all-5.0.3.jar;C:\Users\" + EnvironmentUserName + @"\AppData\Roaming\.epidote\libraries\net\minecraft\launchwrapper\1.7\launchwrapper-1.7.jar;C:\Users\" + EnvironmentUserName + @"\AppData\Roaming\.epidote\libraries\oshi-project\oshi-core\1.1\oshi-core-1.1.jar;C:\Users\" + EnvironmentUserName + @"\AppData\Roaming\.epidote\libraries\net\java\dev\jna\jna\3.4.0\jna-3.4.0.jar;C:\Users\" + EnvironmentUserName + @"\AppData\Roaming\.epidote\libraries\net\java\dev\jna\platform\3.4.0\platform-3.4.0.jar;C:\Users\" + EnvironmentUserName + @"\AppData\Roaming\.epidote\libraries\com\ibm\icu\icu4j-core-mojang\51.2\icu4j-core-mojang-51.2.jar;C:\Users\" + EnvironmentUserName + @"\AppData\Roaming\.epidote\libraries\net\sf\jopt-simple\jopt-simple\4.6\jopt-simple-4.6.jar;C:\Users\" + EnvironmentUserName + @"\AppData\Roaming\.epidote\libraries\com\paulscode\codecjorbis\20101023\codecjorbis-20101023.jar;C:\Users\" + EnvironmentUserName + @"\AppData\Roaming\.epidote\libraries\com\paulscode\codecwav\20101023\codecwav-20101023.jar;C:\Users\" + EnvironmentUserName + @"\AppData\Roaming\.epidote\libraries\com\paulscode\libraryjavasound\20101123\libraryjavasound-20101123.jar;C:\Users\" + EnvironmentUserName + @"\AppData\Roaming\.epidote\libraries\com\paulscode\librarylwjglopenal\20100824\librarylwjglopenal-20100824.jar;C:\Users\" + EnvironmentUserName + @"\AppData\Roaming\.epidote\libraries\com\paulscode\soundsystem\20120107\soundsystem-20120107.jar;C:\Users\" + EnvironmentUserName + @"\AppData\Roaming\.epidote\libraries\io\netty\netty-all\4.0.23.Final\netty-all-4.0.23.Final.jar;C:\Users\" + EnvironmentUserName + @"\AppData\Roaming\.epidote\libraries\com\google\guava\guava\17.0\guava-17.0.jar;C:\Users\" + EnvironmentUserName + @"\AppData\Roaming\.epidote\libraries\org\apache\commons\commons-lang3\3.3.2\commons-lang3-3.3.2.jar;C:\Users\" + EnvironmentUserName + @"\AppData\Roaming\.epidote\libraries\commons-io\commons-io\2.4\commons-io-2.4.jar;C:\Users\" + EnvironmentUserName + @"\AppData\Roaming\.epidote\libraries\commons-codec\commons-codec\1.9\commons-codec-1.9.jar;C:\Users\" + EnvironmentUserName + @"\AppData\Roaming\.epidote\libraries\net\java\jinput\jinput\2.0.5\jinput-2.0.5.jar;C:\Users\" + EnvironmentUserName + @"\AppData\Roaming\.epidote\libraries\net\java\jutils\jutils\1.0.0\jutils-1.0.0.jar;C:\Users\" + EnvironmentUserName + @"\AppData\Roaming\.epidote\libraries\com\google\code\gson\gson\2.2.4\gson-2.2.4.jar;C:\Users\" + EnvironmentUserName + @"\AppData\Roaming\.epidote\libraries\com\mojang\authlib\1.5.21\authlib-1.5.21.jar;C:\Users\" + EnvironmentUserName + @"\AppData\Roaming\.epidote\libraries\com\mojang\realms\1.7.59\realms-1.7.59.jar;C:\Users\" + EnvironmentUserName + @"\AppData\Roaming\.epidote\libraries\org\apache\commons\commons-compress\1.8.1\commons-compress-1.8.1.jar;C:\Users\" + EnvironmentUserName + @"\AppData\Roaming\.epidote\libraries\org\apache\httpcomponents\httpclient\4.3.3\httpclient-4.3.3.jar;C:\Users\" + EnvironmentUserName + @"\AppData\Roaming\.epidote\libraries\commons-logging\commons-logging\1.1.3\commons-logging-1.1.3.jar;C:\Users\" + EnvironmentUserName + @"\AppData\Roaming\.epidote\libraries\org\apache\httpcomponents\httpcore\4.3.2\httpcore-4.3.2.jar;C:\Users\" + EnvironmentUserName + @"\AppData\Roaming\.epidote\libraries\org\apache\logging\log4j\log4j-api\2.0-beta9\log4j-api-2.0-beta9.jar;C:\Users\" + EnvironmentUserName + @"\AppData\Roaming\.epidote\libraries\org\apache\logging\log4j\log4j-core\2.0-beta9\log4j-core-2.0-beta9.jar;C:\Users\" + EnvironmentUserName + @"\AppData\Roaming\.epidote\libraries\org\lwjgl\lwjgl\lwjgl\2.9.4-nightly-20150209\lwjgl-2.9.4-nightly-20150209.jar;C:\Users\" + EnvironmentUserName + @"\AppData\Roaming\.epidote\libraries\org\lwjgl\lwjgl\lwjgl_util\2.9.4-nightly-20150209\lwjgl_util-2.9.4-nightly-20150209.jar;C:\Users\" + EnvironmentUserName + @"\AppData\Roaming\.epidote\libraries\tv\twitch\twitch\6.5\twitch-6.5.jar;C:\Users\" + EnvironmentUserName + @"\AppData\Roaming\.epidote\launcher\cb.jar"" -Xmx" + JavawMemory + @"M -XX:+UnlockExperimentalVMOptions -XX:+UseG1GC -XX:G1NewSizePercent=20 -XX:G1ReservePercent=20 -XX:MaxGCPauseMillis=50 -XX:G1HeapRegionSize=32M Start --username " + LoginUI.vusername + @" --version ""Epidote Offline CheatBreaker 1.8.9"" --gameDir ""C:\Users\" + EnvironmentUserName + @"\AppData\Roaming\.epidote"" --assetsDir ""C:\Users\" + EnvironmentUserName + @"\AppData\Roaming\.vanityempire.hu\assets"" --assetIndex 1.8 --uuid " + LoginUI.uuid + @" --accessToken " + LoginUI.sessionid + @" --userProperties {} --userType legacy";
            using (var md52 = MD5.Create())
            {
                using (var stream2 = File.OpenRead(cheatbreaker))
                {
                    try
                    {
                        var hash2 = md52.ComputeHash(stream2);
                        if (BitConverter.ToString(hash2).Replace("-", "") == "5881582F30057C2D642815F2D32753F3")
                        {
                            p.StartInfo.FileName = "javaw.exe";
                            p.StartInfo.Arguments = cheatbreakerarg;
                            p.StartInfo.UseShellExecute = false;
                            p.Start();
                            cheatbreakerButton.Text = "Várakozás..";
                            int x = 0;
                            while (x == 0)
                            {
                                Process[] processCollection = Process.GetProcesses();
                                foreach (Process processeees in processCollection)
                                {
                                    if (processeees.BasePriority == 8)
                                    {
                                        if (processeees.MainWindowTitle == "CheatBreaker 1.8 Updater")
                                        {
                                            processeees.CloseMainWindow();
                                            x++;
                                        }
                                    }
                                }
                            }
                            if (x != 0)
                            {
                                Process[] pname = Process.GetProcessesByName("javaw");
                                if (pname.Length != 0)
                                {
                                    clientstarted = "CheatBreaker 1.8.9";
                                    cheatbreakerButton.Text = "Elindítva";
                                    Process[] GetValo = Process.GetProcessesByName("javaw");
                                    foreach (Process valproc in GetValo)
                                    {
                                        valproc.PriorityClass = ProcessPriorityClass.RealTime;
                                        valproc.PriorityBoostEnabled = true;
                                    }
                                    try { SuccDiscordRequest(); } catch { }

                                }
                                if (pname.Length == 0)
                                {
                                    cheatbreakerButton.Text = "Sikertelen";
                                    try { FailedDiscordRequest(); } catch { }

                                }
                            }
                            p.WaitForExit();
                            if (p.HasExited)
                            {
                                cheatbreakerButton.Text = "Indítás";
                            }
                        }
                        else
                        {
                            Console.WriteLine("idk");
                        }
                    }
                    catch
                    {
                        cheatbreakerButton.Text = "Probléma történt!";
                    }
                }
            }
        }

        private void batmodLauncher_DoWork(object sender, DoWorkEventArgs e)
        {
            string batmod = epidoteLauncher + @"\bm.jar";
            if (!File.Exists(batmod))
            {
                batmodButton.Text = "Letöltés";
                letoltes.DownloadFile("https://sna.flamedeck.com/bm.jar", epidoteLauncher + @"\bm.jar");
                letoltes.DownloadFile("https://sna.flamedeck.com/BatMod.jar", epidoteBadmodclient + @"\BatMod.jar");
            }
            var batmodclientarg = @"""-Dos.name=Windows 10"" -Dos.version=10.0 -Djava.library.path=""C:\Users\" + EnvironmentUserName + @"\AppData\Roaming\.epidote\lib"" -Dminecraft.launcher.brand=minecraft-launcher -Dminecraft.launcher.version=2.3.136 ""-Dminecraft.client.jar=C:\Users\" + EnvironmentUserName + @"\AppData\Roaming\.epidote\launcher\bm.jar"" -cp ""C:\Users\" + EnvironmentUserName + @"\AppData\Roaming\.epidote\libraries\com\mojang\netty\1.6\netty-1.6.jar;C:\Users\" + EnvironmentUserName + @"\AppData\Roaming\.epidote\libraries\oshi-project\oshi-core\1.1\oshi-core-1.1.jar;C:\Users\" + EnvironmentUserName + @"\AppData\Roaming\.epidote\libraries\net\java\dev\jna\jna\3.4.0\jna-3.4.0.jar;C:\Users\" + EnvironmentUserName + @"\AppData\Roaming\.epidote\libraries\net\java\dev\jna\platform\3.4.0\platform-3.4.0.jar;C:\Users\" + EnvironmentUserName + @"\AppData\Roaming\.epidote\libraries\com\ibm\icu\icu4j-core-mojang\51.2\icu4j-core-mojang-51.2.jar;C:\Users\" + EnvironmentUserName + @"\AppData\Roaming\.epidote\libraries\net\sf\jopt-simple\jopt-simple\4.6\jopt-simple-4.6.jar;C:\Users\" + EnvironmentUserName + @"\AppData\Roaming\.epidote\libraries\com\paulscode\codecjorbis\20101023\codecjorbis-20101023.jar;C:\Users\" + EnvironmentUserName + @"\AppData\Roaming\.epidote\libraries\com\paulscode\codecwav\20101023\codecwav-20101023.jar;C:\Users\" + EnvironmentUserName + @"\AppData\Roaming\.epidote\libraries\com\paulscode\libraryjavasound\20101123\libraryjavasound-20101123.jar;C:\Users\" + EnvironmentUserName + @"\AppData\Roaming\.epidote\libraries\com\paulscode\librarylwjglopenal\20100824\librarylwjglopenal-20100824.jar;C:\Users\" + EnvironmentUserName + @"\AppData\Roaming\.epidote\libraries\com\paulscode\soundsystem\20120107\soundsystem-20120107.jar;C:\Users\" + EnvironmentUserName + @"\AppData\Roaming\.epidote\libraries\io\netty\netty-all\4.0.23.Final\netty-all-4.0.23.Final.jar;C:\Users\" + EnvironmentUserName + @"\AppData\Roaming\.epidote\libraries\com\google\guava\guava\17.0\guava-17.0.jar;C:\Users\" + EnvironmentUserName + @"\AppData\Roaming\.epidote\libraries\org\apache\commons\commons-lang3\3.3.2\commons-lang3-3.3.2.jar;C:\Users\" + EnvironmentUserName + @"\AppData\Roaming\.epidote\libraries\commons-io\commons-io\2.4\commons-io-2.4.jar;C:\Users\" + EnvironmentUserName + @"\AppData\Roaming\.epidote\libraries\commons-codec\commons-codec\1.9\commons-codec-1.9.jar;C:\Users\" + EnvironmentUserName + @"\AppData\Roaming\.epidote\libraries\net\java\jinput\jinput\2.0.5\jinput-2.0.5.jar;C:\Users\" + EnvironmentUserName + @"\AppData\Roaming\.epidote\libraries\net\java\jutils\jutils\1.0.0\jutils-1.0.0.jar;C:\Users\" + EnvironmentUserName + @"\AppData\Roaming\.epidote\libraries\com\google\code\gson\gson\2.2.4\gson-2.2.4.jar;C:\Users\" + EnvironmentUserName + @"\AppData\Roaming\.epidote\libraries\com\mojang\authlib\1.5.21\authlib-1.5.21.jar;C:\Users\" + EnvironmentUserName + @"\AppData\Roaming\.epidote\libraries\com\mojang\realms\1.7.39\realms-1.7.39.jar;C:\Users\" + EnvironmentUserName + @"\AppData\Roaming\.epidote\libraries\org\apache\commons\commons-compress\1.8.1\commons-compress-1.8.1.jar;C:\Users\" + EnvironmentUserName + @"\AppData\Roaming\.epidote\libraries\org\apache\httpcomponents\httpclient\4.3.3\httpclient-4.3.3.jar;C:\Users\" + EnvironmentUserName + @"\AppData\Roaming\.epidote\libraries\commons-logging\commons-logging\1.1.3\commons-logging-1.1.3.jar;C:\Users\" + EnvironmentUserName + @"\AppData\Roaming\.epidote\libraries\org\apache\httpcomponents\httpcore\4.3.2\httpcore-4.3.2.jar;C:\Users\" + EnvironmentUserName + @"\AppData\Roaming\.epidote\libraries\org\apache\logging\log4j\log4j-api\2.0-beta9\log4j-api-2.0-beta9.jar;C:\Users\" + EnvironmentUserName + @"\AppData\Roaming\.epidote\libraries\org\apache\logging\log4j\log4j-core\2.0-beta9\log4j-core-2.0-beta9.jar;C:\Users\" + EnvironmentUserName + @"\AppData\Roaming\.epidote\libraries\org\lwjgl\lwjgl\lwjgl\2.9.4-nightly-20150209\lwjgl-2.9.4-nightly-20150209.jar;C:\Users\" + EnvironmentUserName + @"\AppData\Roaming\.epidote\libraries\org\lwjgl\lwjgl\lwjgl_util\2.9.4-nightly-20150209\lwjgl_util-2.9.4-nightly-20150209.jar;C:\Users\" + EnvironmentUserName + @"\AppData\Roaming\.epidote\libraries\tv\twitch\twitch\6.5\twitch-6.5.jar;C:\Users\" + EnvironmentUserName + @"\AppData\Roaming\.epidote\libraries\BatMod\BatMod.jar"" -Xmx" + JavawMemory + @"M -XX:+UnlockExperimentalVMOptions -XX:+UseG1GC -XX:G1NewSizePercent=20 -XX:G1ReservePercent=20 -XX:MaxGCPauseMillis=50 -XX:G1HeapRegionSize=32M -Dlog4j.configurationFile=C:\Users\" + EnvironmentUserName + @"\AppData\Roaming\.epidote\assets\log_configs\client-1.7.xml net.minecraft.client.main.Main --username " + LoginUI.vusername + @" --version Epidote BatMod 1.8.9 --gameDir C:\Users\" + EnvironmentUserName + @"\AppData\Roaming\.epidote --assetsDir C:\Users\" + EnvironmentUserName + @"\AppData\Roaming\.vanityempire.hu\assets --assetIndex 1.8 --uuid " + LoginUI.uuid + @" --accessToken " + LoginUI.sessionid + @" --userProperties {} --userType legacy --width 925 --height 530";
            Console.WriteLine(batmodclientarg);
            lunarButton.Visible = true;
            using (var md5 = MD5.Create())
            {
                using (var stream = File.OpenRead(batmod))
                {
                    try
                    {
                        var hash = md5.ComputeHash(stream);
                        if (BitConverter.ToString(hash).Replace("-", "") == "60D12FB74C3DB45AC6A2AFE835AD439A")
                        {
                            p.StartInfo.FileName = "javaw.exe";
                            p.StartInfo.Arguments = batmodclientarg;
                            p.StartInfo.UseShellExecute = false;
                            p.Start();
                            batmodButton.Text = "Várakozás..";
                            Thread.Sleep(4500); //várakozás a javaw indulásra
                            Process[] pname = Process.GetProcessesByName("javaw");
                            if (pname.Length != 0)
                            {
                                clientstarted = "BatMod 1.8.9";
                                batmodButton.Text = "Elindítva";
                                Process[] GetValo = Process.GetProcessesByName("javaw");
                                foreach (Process valproc in GetValo)
                                {
                                    valproc.PriorityClass = ProcessPriorityClass.RealTime;
                                    valproc.PriorityBoostEnabled = true;
                                }
                                try { SuccDiscordRequest(); } catch { }

                            }
                            if (pname.Length == 0)
                            {
                                batmodButton.Text = "Sikertelen";
                                try { FailedDiscordRequest(); } catch { }
                            }
                            p.WaitForExit();
                            if (p.HasExited)
                            {
                                batmodButton.Text = "Indítás";
                            }
                        }
                        else
                        {
                            MessageBox.Show("Valami nincs rendben, kérlek töröld ki a .epidote mappát, majd indíts újra a programot.");
                        }
                    }
                    catch
                    {
                        batmodButton.Text = "Probléma történt!";
                    }
                }
            }
        }
        static private void Initializer()
        {
            try
            {
                letoltes.Proxy = new WebProxy();
                ProcessStartInfo psi = new ProcessStartInfo();
                psi.FileName = "java.exe";
                psi.Arguments = " -version";
                psi.RedirectStandardError = true;
                psi.UseShellExecute = false;
                psi.CreateNoWindow = true;
                Process pr = Process.Start(psi);
                string strOutput = pr.StandardError.ReadLine().Split(' ')[2].Replace("\"", "");
                javaversion = strOutput;
                if (Environment.Is64BitOperatingSystem)
                {
                    systemBIT = "x64";
                }
                else
                {
                    systemBIT = "x32";
                }
                long memKb;
                GetPhysicallyInstalledSystemMemory(out memKb);
                if (Environment.Is64BitOperatingSystem)
                {
                    JavawMemory = (memKb / 1024);
                }
                else
                {
                    JavawMemory = 1000;
                }
                var compName = Environment.GetEnvironmentVariables()["HOMEPATH"];
                string enviroment = compName.ToString().Split('\\')[2];
                EnvironmentUserName = enviroment;
            }
            catch { }
        }

        static void SuccDiscordRequest()
        {
            var versionName = new ComputerInfo();
            WebRequest wr = (HttpWebRequest)WebRequest.Create(SuccessToken);
            wr.ContentType = "application/json";
            wr.Method = "POST";
            using (var sw = new StreamWriter(wr.GetRequestStream()))
            {
                string json = JsonConvert.SerializeObject(new
                {
                    username = "Epidote - Success Launch",
                    embeds = new[]
                    {
                        new
                        {
                            description = "Itt láthatod a játékosok indításainak logjait."+"\n\n"+"**Operációs Rendszer**"+"\n\n"+" - "+versionName.OSFullName.Replace("Microsoft", "")+"\n\n"+"**Vanity Név**\n\n"+" - "+LoginUI.vusername+"\n\n"+"**Rendszer (BIT)**\n\n"+" - "+systemBIT+"\n\n"+"**Telepített Java**\n\n"+" - "+javaversion+"\n\n"+"**Indított Kliens**\n\n"+" - "+clientstarted+"\n\n"+"**Epidote Verzió**\n\n"+" - "+programversion+"\n\n"+"**Időpont**\n\n"+" - "+localDate,
                            title = "Játékos indítások"+"\n",
                            color = "14690403",
                        }
                    }
                });
                sw.Write(json);
            }
            wr.GetResponse();
            wr = null;
        }

        static void FailedDiscordRequest()
        {
            var versionName = new ComputerInfo();
            WebRequest wr = (HttpWebRequest)WebRequest.Create(FailedToken);
            wr.ContentType = "application/json";
            wr.Method = "POST";
            using (var sw = new StreamWriter(wr.GetRequestStream()))
            {
                string json = JsonConvert.SerializeObject(new
                {
                    username = "Epidote - Failed Launch",
                    embeds = new[]
                    {
                        new
                        {
                            description = "Itt láthatod a hibás indítási logokat."+"\n\n"+"**Operációs Rendszer**"+"\n\n"+" - "+versionName.OSFullName.Replace("Microsoft", "")+"\n\n"+"**Vanity Név**\n\n"+" - "+LoginUI.vusername+"\n\n"+"**Rendszer (BIT)**\n\n"+" - "+systemBIT+"\n\n"+"**Telepített Java**\n\n"+" - "+javaversion+"\n\n"+"**Indított Kliens**\n\n"+" - "+clientstarted+"\n\n"+"**Epidote Verzió**\n\n"+" - "+programversion+"\n\n"+"**Időpont**\n\n"+" - "+localDate,
                            title = "Hibás Indítási kisérletek"+"\n",
                            color = "14690403",
                        }
                    }
                });
                sw.Write(json);
            }
            wr.GetResponse();
            wr = null;
        }
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            Dispose();
            Environment.Exit(0);
        }
        private void siticoneTileButton5_Click(object sender, EventArgs e)
        {
            Dispose();
            Environment.Exit(0);
        }

        private void lunarButton_Click(object sender, EventArgs e)
        {
            if (!lunarLauncher.IsBusy)
            {
                lunarLauncher.RunWorkerAsync();
            }
        }

        private void melonButton_Click(object sender, EventArgs e)
        {
            if (!melonLauncher.IsBusy)
            {
                melonLauncher.RunWorkerAsync();
            }
        }

        private void cheatbreakerButton_Click(object sender, EventArgs e)
        {
            if (!cheatbreakerLauncher.IsBusy)
            {
                cheatbreakerLauncher.RunWorkerAsync();
            }
        }

        private void batmodButton_Click(object sender, EventArgs e)
        {
            if (!batmodLauncher.IsBusy)
            {
                batmodLauncher.RunWorkerAsync();
            }
        }

        private void TextUpdater_DoWork(object sender, DoWorkEventArgs e)
        {
            while (true)
            {
                string jatekosok = letoltes.DownloadString("https://vanityempire.hu/jatekosszam.php");
                if (online_jatekosok.Text != jatekosok)
                {
                    online_jatekosok.Text = jatekosok;
                }
                Thread.Sleep(5500);
            }
        }

        private void online_jatekosok_Click(object sender, EventArgs e)
        {

        }
    }
}
