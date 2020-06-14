using bcy = SiteLib.Bcy.Net.BcyNet;
using SiteLib.Bcy.Net;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System;

namespace Bcy
{
    class Program
    {
        private static void Main(string[] args)
        {
            //https://bcy.net/tag/79203
            //D.VA Circle
            var task = CircleDownload("bcy","79203");
            task.Wait();
        }

        private static async Task CircleDownload(string savefolder,string cid)
        {
            Dictionary<string, int> gender = new Dictionary<string, int>();

            SmallFileDown down = new SmallFileDown(10, false, true);

            var json = await BcyJson.CircleFeed(cid);
            while (json.Length > BcyJson.MinValidJsonLen)
            {
                var items = CircleFeedJson.FromJson(json).Data.Items;

                foreach (var e in items)
                {
                    if (e.ItemDetail.Type != ItemDetailType.Note || e.ItemDetail.ImageList == null)
                    {
                        continue;
                    }
                    var uid = e.ItemDetail.Uid.ToString();
                    if (!gender.Keys.Contains(uid))
                    {
                        var userinfo = await bcy.Info(uid.ToString());
                        gender.Add(uid, userinfo.Sex);
                    }
                    if (gender[uid] != 0)
                    {
                        continue;
                    }

                    var uname = e.ItemDetail.Uname;
                    uname = TrimPath(uname);

                    foreach (var img in e.ItemDetail.ImageList)
                    {
                        var (url, ext) = BcySpecial.GetNoWaterMarkImgPathAndExt(img.Path.ToString());
                        string savepath = Path.Combine(savefolder, uname, img.Mid + ext);
                        down.Add(url, savepath);
                    }
                }
                var last_since = items[items.Length - 1].Since;
                Console.WriteLine($"since:{last_since}");
                json = await BcyJson.CircleFeed("79203", last_since);

            }
            await down.DownloadWork;          
        }

        public static string TrimPath(string path)
        {
            System.Text.StringBuilder str = new System.Text.StringBuilder();
            var invalidFileNameChars = System.IO.Path.GetInvalidFileNameChars();
            foreach (var c in path)
            {
                if (invalidFileNameChars.Contains(c))
                {
                    str.Append("-");
                }
                else
                {
                    str.Append(c);
                }
            }
            return str.ToString();
        }
    }
}
