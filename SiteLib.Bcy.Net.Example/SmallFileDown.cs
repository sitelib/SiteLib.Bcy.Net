using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Bcy
{
    public class SmallFileDown
    {
        public int MaxTaskNum { get; set; }
        public Queue<(string url, string savepath)> UnDownLoadList { get; } = new Queue<(string url, string savepath)>();
        public List<string> History { get; } = null;
        public Queue<(string url, string savepath)> FailedList { get; } = new Queue<(string url, string savepath)>();
        public Task DownloadWork = null;

        public delegate void BlockedCallBack(SmallFileDown s);
        public BlockedCallBack Callback { set; private get; } = null;

        bool isWorking = false;
        public bool Retry { get; set; }

        public SmallFileDown(int tasknum = 5, bool jumpHistory = false, bool retryError = false)
        {
            this.MaxTaskNum = tasknum;
            if (jumpHistory)
            {
                History = new List<string>();
            }
            Retry = retryError;
        }

        public void Add(string url, string savepath)
        {
            if (History != null)
            {
                if (History.Contains(url))
                {
                    return;
                }
                else
                {
                    History.Add(url);
                }
            }
            UnDownLoadList.Enqueue((url, savepath));
            if (!isWorking)
            {
                DownloadWork = Work();
            }
        }

        private async Task Work()
        {
            isWorking = true;
#if DEBUG
            Console.WriteLine("     Work In!");
#endif
            List<Task> tasks = new List<Task>();

            while (UnDownLoadList.Count > 0 || (Retry && FailedList.Count > 0) || tasks.Count > 0)
            {
#if DEBUG
                Console.WriteLine("UnDownLoadList:" + UnDownLoadList.Count.ToString());
#endif
                int timeout = 20000;//任务超时
                if (UnDownLoadList.Count > 0)
                {
                    var d = UnDownLoadList.Dequeue();
                    var down_task = Down(d.url, d.savepath, FailedList);
                    tasks.Add(down_task);
                }
                else if (Retry && FailedList.Count > 0)
                {
                    var d = FailedList.Dequeue();
#if DEBUG
                    Console.WriteLine("ReDowning:" + d.savepath);
#endif
                    var down_task = Down(d.url, d.savepath);
                    tasks.Add(down_task);
                }
                else
                {
                    timeout = 2000;//最大再次检查时间
                }

                if ((UnDownLoadList.Count == 0 && !Retry) || (UnDownLoadList.Count == 0 && Retry && FailedList.Count == 0) || tasks.Count > MaxTaskNum)
                {
                    var timeout_task = Task.Delay(timeout);
                    tasks.Add(timeout_task);
                    var t = await Task.WhenAny(tasks);
                    tasks.Remove(t);
                    if (t != timeout_task)
                    {
                        tasks.Remove(timeout_task);
                    }
                    if (tasks.Count > MaxTaskNum * 2)//超时任务数大于最大任务2倍
                    {
#if DEBUG
                        Console.WriteLine("  ----InBlock----");
#endif
                        Callback?.Invoke(this);
                        var block_out = await Task.WhenAny(tasks);
                        tasks.Remove(block_out);
#if DEBUG
                        Console.WriteLine("  ----UnBlock----");
#endif
                    }
                }
            }
            isWorking = false;
#if DEBUG
            Console.WriteLine("     Work Out!");
            Console.WriteLine("UnDownLoadList:" + UnDownLoadList.Count.ToString());
#endif
        }

        public static async Task Down(string url, string savepath, Queue<(string url, string savepath)> FailedList = null)
        {
            if (File.Exists(savepath))
            {
                return;
            }
            var rq = HttpWebRequest.Create(url);
            MemoryStream ms = new MemoryStream();
            try
            {
                var rs = await rq.GetResponseAsync() as HttpWebResponse;
                var s = rs.GetResponseStream();

                await s.CopyToAsync(ms);
                s.Close();
                rs.Close();
            }
            catch (Exception e)
            {
#if DEBUG
                Console.WriteLine(e.ToString());
#endif
                if (FailedList != null)
                {
                    FailedList.Enqueue((url, savepath));
                }
                return;
            }

            string dir = Path.GetDirectoryName(savepath);
            if (dir != "" && !Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
            if (!File.Exists(savepath))
            {
                FileStream fs = new FileStream(savepath, FileMode.Create);
                ms.Seek(0, SeekOrigin.Begin);
                await ms.CopyToAsync(fs);
                fs.Close();
            }
            ms.Close();
#if DEBUG
            Console.WriteLine("Downed:" + savepath);
#endif
        }
    }
}
