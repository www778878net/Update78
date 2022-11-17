using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using www778878net.Log;
using www778878net.net;
namespace www778878net.update
{
    /// <summary>
    /// 自动更新功能(只能自动更新别的程序 类似守护)
    /// </summary>
    public class Update78
    {
        #region 属性
        public delegate void UpatevChangedHandler(string debugvnew );
        public event UpatevChangedHandler? UpatevChanged;
        /// <summary>
        /// 是否自动
        /// </summary>
        public bool IsAuto { get; set; }
        /// <summary>
        /// 更新目录
        /// </summary>
        public string updatepath = "update\\";
        /// <summary>
        /// 版本号文件
        /// </summary>
        public string vfile = "v.config";

        /// <summary>
        /// 软件下载目录 
        /// </summary>
        readonly string urldown;
        /// <summary>
        /// 软件本地目录 
        /// </summary>
        readonly string menupath;
        /// <summary>
        /// 5分钟更新一次
        /// </summary>
        private DateTime dDown = DateTime.Now.AddMinutes(-1);

        /// <summary>
        /// 自动更新
        /// </summary>
        readonly System.Timers.Timer timer = new  ();
  
        private string debugv = "";//对比V
        #endregion
        /// <summary>
        /// 
        /// </summary>
        /// <param name="menupath">软件本地目录</param>
        /// <param name="urldown">软件下载url</param>
        /// <param name="debugini">初始版本号</param>
        public Update78(string menuin, string url, string debugini)
        {
            if (menuin.Substring(menuin.Length - 1, 1) != "\\")
                menuin += "\\";
            if (url.Substring(url.Length - 1, 1) != "/")
                url += "/";
            this.menupath = menuin;
            this.urldown = url;
            debugv = debugini;

            timer.Interval = 60000;
            timer.Enabled = false;
            timer.Elapsed += new System.Timers.ElapsedEventHandler(Timer1_Elapsed);
            this.debugv = debugini;
        }

        public void Start()
        {
            IsAuto = true;
            timer.Enabled = true;
        }

        public void Stop()
        {
            IsAuto = false;
            timer.Enabled = false;
        }

        public void Test()
        {
            down();
        }

        void Timer1_Elapsed(object? sender, System.Timers.ElapsedEventArgs e)
        {
            if (!IsAuto)
            {
                return;
            }
            down();
        }

        private void OnUpatevChanged(string debugvnew)
        {
            if (UpatevChanged != null)
            {
                UpatevChanged(debugvnew);
            }
        }

        private async void down()
        {
            if (!System.IO.Directory.Exists(menupath))
                System.IO.Directory.CreateDirectory(menupath);
            if (!System.IO.Directory.Exists(menupath + updatepath))
                System.IO.Directory.CreateDirectory(menupath + updatepath);
            if (DateTime.Now < dDown) return;
            dDown = DateTime.Now.AddMinutes(5);
            #region 下载最新版客户端
            try
            {
            
               await HttpClient78.Client78.DownFile(urldown + vfile
                    , menupath + updatepath + vfile);

                string s = "";
                if (File.Exists(menupath + updatepath + vfile))
                {
                    using StreamReader sr = new(menupath + updatepath + vfile , Encoding.Default);
                    s = sr.ReadLine();
                    sr.Close();
                }
                //分割一下 大版本号为0 或大小版本号与存的一致就不更新
                //if (!ValidateHelper.IsNumber(s)) return;
                string[] vs = s.Split('.');
                if (s!="" && vs.Length != 2) return;
                if (vs[0] == "0") return;
                if (s == debugv) return;

                //如果大版本号一致 就更新到分隔符 否则全部更新
                string[] vs2 = debugv.Split('.');
                bool isall = true;
                if (vs[0] == vs2[0]) isall = false;
                debugv = s;
                using (StreamReader sr = new(menupath + updatepath + vfile, Encoding.Default))
                {
                    while ((s = sr.ReadLine()) != null)
                    {
                        if (s == "") break;
                        if (s == debugv) continue;
                        if (s == "----")
                        {
                            if (isall) continue;
                            else break;
                        }
                        try
                        {
                           await HttpClient78.Client78.DownFile(urldown + s
                                , menupath + updatepath + s);
                        }
                        catch { }
                    }
                    sr.Close();
                }
                OnUpatevChanged(debugv);
               
            }
            catch (Exception ex)
            {
                Log78.client.Error(ex);
            }
            #endregion
        }
    }
}
