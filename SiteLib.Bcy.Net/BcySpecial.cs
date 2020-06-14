using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bcy
{
    static class BcySpecial
    {
        //1.无水印图片地址
        static int byteimgId = 0;
        static string[] byteimgs =
            {
                "https://p1-bcy.byteimg.com/img/banciyuan/",
                "https://p3-bcy.byteimg.com/img/banciyuan/",
                "https://p6-bcy.byteimg.com/img/banciyuan/",
                "https://p9-bcy.byteimg.com/img/banciyuan/"
            };
        /// <summary>
        /// 无水印地址
        /// </summary>
        /// <param name="path">Img_Path</param>
        /// <returns>path and ext</returns>
        public static (string path, string ext) GetNoWaterMarkImgPathAndExt(string path)
        {
            string ext = ".image";

            var sp = path.Split('/');
            var s = sp[sp.Length - 1];
            var index = s.IndexOf('.');
            if (index > 0)
            {
                ext = s.Substring(index);
            }
            else if (sp[sp.Length - 2] != "banciyuan")
            {
                ext = ".jpg";
            }
            else
            {
                path = byteimgs[byteimgId++ % 4] + s + "~noop.image";
                ext = ".jpg";
            }
            return (path, ext);
        }
    }
}
