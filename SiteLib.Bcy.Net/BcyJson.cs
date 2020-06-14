using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

//半次元JsonAPI
namespace SiteLib.Bcy.Net
{
    public static class BcyJson
    {
        //一.常量
        public const int MinValidJsonLen = 64;//低于此值，实际无返回数据

        //二.Cookie和CsrfToken
        //1.cookie
        private static readonly Uri uri = new Uri("https://bcy.net");
        private static CookieContainer cookie = null;
        public static void SetCookie(string set_cookie)
        {
            if (cookie == null)
            {
                cookie = new CookieContainer();
            }
            cookie.SetCookies(uri, set_cookie);
        }

        public static void ClearCookie()
        {
            cookie = null;
        }

        //2._csrf_token
        private static bool csrfTokenFlag = false;
        private static string CsrfToken
        {
            get
            {
                string _csrf_token = "abcdfgh";
                if (!csrfTokenFlag)
                {
                    var c = new Cookie("_csrf_token", _csrf_token, "/", "bcy.net");
                    if (cookie == null)
                    {
                        cookie = new CookieContainer();
                    }
                    cookie.Add(c);
                    csrfTokenFlag = true;
                }
                return "_csrf_token=" + _csrf_token;
            }
        }

        //三.HTTP方法
        //1.Get方法
        private static async Task<string> HttpGet(string url)
        {
            var rq = (HttpWebRequest)WebRequest.Create(url);
            rq.Method = "Get";

            if (cookie != null)
            {
                rq.CookieContainer = cookie;
            }

            WebResponse rs = await rq.GetResponseAsync();
            var set_cookie = rs.Headers["Set-Cookie"];
            SetCookie(set_cookie);
            Stream s = rs.GetResponseStream();
            StreamReader sr = new StreamReader(s, Encoding.GetEncoding("utf-8"));
            string retString = await sr.ReadToEndAsync();
            sr.Close();
            s.Close();
            return retString;
        }

        //2.Post方法
        private static async Task<string> HttpPost(string url, string data = null, bool csrf = false)
        {
            if (data == null)
            {
                data = CsrfToken;
            }
            else if (csrf)
            {
                data += "&" + CsrfToken;
            }

            var rq = (HttpWebRequest)WebRequest.Create(url);
            rq.Method = "Post";

            if (cookie != null)
            {
                rq.CookieContainer = cookie;
            }

            byte[] buf = Encoding.UTF8.GetBytes(data);
            rq.ContentLength = Encoding.UTF8.GetByteCount(data);
            var p = await rq.GetRequestStreamAsync();
            await p.WriteAsync(buf, 0, buf.Length);

            WebResponse rs = await rq.GetResponseAsync();
            var set_cookie = rs.Headers["Set-Cookie"];
            SetCookie(set_cookie);
            Stream s = rs.GetResponseStream();
            StreamReader sr = new StreamReader(s, Encoding.GetEncoding("utf-8"));
            string retString = await sr.ReadToEndAsync();
            sr.Close();
            s.Close();
            return retString;
        }

        //四、JsonAPI
        //1.https://bcy.net/apiv3/user/info
        /// <summary>
        /// 查询用户信息
        /// </summary>
        /// <param name="uid">用户ID</param>
        /// <returns>JsonString</returns>
        public static Task<string> Info(string uid)
        {
            string url = "https://bcy.net/apiv3/user/info?uid=" + uid;
            return HttpGet(url);
        }

        //2.https://bcy.net/apiv3/user/message
        /// <summary>
        /// 我的消息
        /// </summary>
        /// <param name="isPre">"n"</param>
        /// <param name="page">1</param>
        /// <param name="pageType">"notify" or "remind" or "itemsetupdate"</param>
        /// <returns>JsonString</returns>
        public static Task<string> Message(string isPre, int page, string pageType)
        {
            string url = "https://bcy.net/apiv3/user/message";
            string data = "isPre=" + isPre;
            data += "&page=" + page.ToString();
            data += "&pageType=" + pageType;
            return HttpPost(url, data, true);
        }
        public static Task<string> Message_Notify(int page) => Message("n", page, "notify");
        public static Task<string> Message_Remind(int page) => Message("n", page, "remind");
        public static Task<string> Message_ItemSetUpdate(int page) => Message("n", page, "itemsetupdate");

        //3.https://bcy.net/apiv3/user/pcAnnounce
        /// <summary>
        /// 公告
        /// </summary>
        /// <param name="since">nullable</param>
        /// <param name="limit">数量</param>
        /// <returns>JsonString</returns>
        public static Task<string> PcAnnounce(string since, int limit)
        {
            string url = "https://bcy.net/apiv3/user/pcAnnounce?";
            if (since != null)
            {
                url += "since=" + since + "&limit=" + limit.ToString();
            }
            else
            {
                url += "limit=" + limit.ToString(); ;
            }
            return HttpGet(url);
        }
        public static Task<string> PcAnnounce() => HttpGet("https://bcy.net/apiv3/user/pcAnnounce");

        //6.https://bcy.net/apiv3/collection/getMyCollectionList
        /// <summary>
        /// 我创建的合集
        /// </summary>
        /// <param name="since">""</param>
        /// <param name="sort">0</param>
        /// <returns>JsonString</returns>
        public static Task<string> GetMyCollectionList(string since, int sort)
        {
            string url = "https://bcy.net/apiv3/collection/getMyCollectionList?";
            url += "since=" + since;
            url += "&sort=" + sort.ToString();
            return HttpGet(url);
        }
        public static Task<string> GetMyCollectionList() => HttpGet("https://bcy.net/apiv3/collection/getMyCollectionList?since=&sort=0");

        //7.https://bcy.net/apiv3/collection/getSubscribeCollectionList
        /// <summary>
        /// 我关注的合集
        /// </summary>
        /// <param name="since">""</param>
        /// <param name="sort">0</param>
        /// <returns>JsonString</returns>
        public static Task<string> GetSubscribeCollectionList(string since, int sort)
        {
            string url = "https://bcy.net/apiv3/collection/getSubscribeCollectionList?";
            url += "since=" + since;
            url += "&sort=" + sort.ToString();
            return HttpGet(url);
        }
        public static Task<string> GetSubscribeCollectionList() => HttpGet("https://bcy.net/apiv3/collection/getSubscribeCollectionList?since=&sort=0");


        //8.https://bcy.net/apiv3/user/follow-list
        /// <summary>
        /// 关注、粉丝、关注的圈子
        /// </summary>
        /// <param name="uid">self_uid</param>
        /// <param name="page">1</param>
        /// <param name="follow_type">0-3</param>
        /// <returns>JsonString</returns>
        //我的关注 follow_type=0
        //我的粉丝 follow_type=1
        //互相关注 follow_type=2
        //我关注的圈子 follow_type=3
        public static Task<string> FollowList(string uid, int page, int follow_type)
        {
            string url = "https://bcy.net/apiv3/user/follow-list?";
            url += "uid=" + uid;
            url += "&page=" + page.ToString();
            url += "&follow_type=" + follow_type.ToString();
            return HttpGet(url);
        }

        //9.https://bcy.net/apiv3/user/block-list
        /// <summary>
        /// 黑名单列表
        /// </summary>
        /// <param name="uid">self_uid</param>
        /// <param name="page">1</param>
        /// <param name="follow_type">0</param>
        /// <returns>JsonString</returns>
        public static Task<string> BlockList(string uid, int page, int follow_type = 0)
        {
            string url = "https://bcy.net/apiv3/user/block-list?";
            url += "uid=" + uid;
            url += "&page=" + page.ToString();
            url += "&follow_type=" + follow_type.ToString();
            return HttpGet(url);
        }

        //10.https://bcy.net/apiv3/user/block
        /// <summary>
        /// 加入或移出黑名单
        /// </summary>
        /// <param name="actionType">1 or 2</param>
        /// <param name="bid">block_uid</param>
        /// <returns>JsonString</returns>
        //加入黑名单 actionType: 1
        //移出黑名单 actionType: 2
        public static Task<string> Block(int actionType, string bid)
        {
            string url = "https://bcy.net/apiv3/user/block";
            string data = "actionType=" + actionType.ToString();
            data += "&bid=" + bid;
            return HttpPost(url, data, true);
        }
        public static Task<string> Block_Add(string bid) => Block(1, bid);
        public static Task<string> Block_Remove(string bid) => Block(2, bid);

        //11.https://bcy.net/apiv3/user/selfPosts
        /// <summary>
        /// 用户所有发布内容
        /// </summary>
        /// <param name="uid">用户ID</param>
        /// <param name="since">"" or last_since</param>
        /// <returns>JsonString</returns>
        public static Task<string> SelfPosts(string uid, string since)
        {
            string url = "https://bcy.net/apiv3/user/selfPosts?";
            url += "uid=" + uid;
            url += "&since=" + since;
            return HttpGet(url);
        }
        public static Task<string> SelfPosts(string uid) => HttpGet("https://bcy.net/apiv3/user/selfPosts?uid=" + uid.ToString());

        //12.https://bcy.net/apiv3/user/post
        /// <summary>
        /// 用户分类发布内容
        /// </summary>
        /// <param name="mid">self_uid</param>
        /// <param name="page">1</param>
        /// <param name="ptype">note,article,ganswer,video</param>
        /// <param name="uid">用户ID</param>
        /// <returns>JsonString</returns>
        //图片 note
        //文字 article
        //问答 ganswer
        //视频 video
        public static Task<string> Post(string mid, int page, string ptype, string uid)
        {
            string url = "https://bcy.net/apiv3/user/post";
            string data = "mid=" + mid;
            data += "&page=" + page.ToString();
            data += "&ptype=" + ptype;
            data += "&uid=" + uid;
            return HttpPost(url, data, true);
        }
        public static Task<string> Post_Note(string mid, int page, string uid) => Post(mid, page, "note", uid);
        public static Task<string> Post_Article(string mid, int page, string uid) => Post(mid, page, "article", uid);
        public static Task<string> Post_Ganswer(string mid, int page, string uid) => Post(mid, page, "ganswer", uid);
        public static Task<string> Post_Video(string mid, int page, string uid) => Post(mid, page, "video", uid);

        //13.https://bcy.net/apiv3/user/favor
        /// <summary>
        /// 用户喜欢和收藏
        /// </summary>
        /// <param name="uid">用户ID</param>
        /// <param name="ptype">like，collect</param>
        /// <param name="mid">self_uid</param>
        /// <param name="since">"" or last_since</param>
        /// <param name="size"></param>
        /// <returns>JsonString</returns>
        //用户喜欢 ptype=like
        //用户收藏 ptype=collect
        public static Task<string> Favor(string uid, string ptype, string mid, string since, int size = 35)
        {
            string url = "https://bcy.net/apiv3/user/favor?";
            url += "uid=" + uid;
            url += "&ptype=" + ptype;
            url += "&mid=" + mid;
            url += "&since=" + since;
            url += "&size=" + size.ToString();
            return HttpGet(url);
        }
        public static Task<string> Favor_Like(string uid, string mid, string since, int size = 35) => Favor(uid, "like", mid, since, size);
        public static Task<string> Favor_Collect(string uid, string mid, string since, int size = 35) => Favor(uid, "collect", mid, since, size);

        //14.https://bcy.net/apiv3/user/follow
        /// <summary>
        /// 关注或取消关注
        /// </summary>
        /// <param name="fid">follow_uid</param>
        /// <param name="opt">dofollow,unfollow</param>
        /// <returns>JsonString</returns>
        //关注用户 opt: "dofollow"
        //取消关注 opt: "unfollow"
        public static Task<string> Follow(string fid, string opt)
        {
            string url = "https://bcy.net/apiv3/user/follow";
            string data = "fid=" + fid;
            data += "&opt=" + opt;
            return HttpPost(url, data, true);
        }
        public static Task<string> Follow_DoFollow(string fid) => Follow(fid, "dofollow");
        public static Task<string> Follow_UnFollow(string fid) => Follow(fid, "unfollow");


        //15.https://bcy.net/apiv3/user/sendmessage
        /// <summary>
        /// 发送私信
        /// </summary>
        /// <param name="content">内容</param>
        /// <param name="to_uid">to_uid</param>
        /// <returns>JsonString</returns>
        //意见反馈(半次元女仆长) to_uid: 1053848
        public static Task<string> SendMessage(string content, string to_uid)
        {
            string url = "https://bcy.net/apiv3/user/sendmessage";
            string data = "content=" + content;
            data += "&to_uid=" + to_uid;
            return HttpPost(url, data, true);
        }
        public static Task<string> SendMessage_FeedBackToMaid(string content) => SendMessage(content, "1053848");


        //16.
        /// <summary>
        /// 关注动态(work by cookie)
        /// </summary>
        /// <param name="since">"" or last_since</param>
        /// <returns>JsonString</returns>
        public static Task<string> FriendsFeed(string since)
        {
            string url = "https://bcy.net/apiv3/user/friendsFeed?";
            url += "since=" + since;
            return HttpGet(url);
        }
        public static Task<string> FriendsFeed() => HttpGet("https://bcy.net/apiv3/user/friendsFeed");

        //17.https://bcy.net/apiv3/common/getFeeds
        /// <summary>
        /// 获取推荐(ok)或特定频道内容(error:20200611)
        /// </summary>
        /// <param name="cid">channel_id</param>
        /// <param name="refer">feed,channel/channel_video（Just use channel is OK!）</param>
        /// <param name="direction"></param>
        /// <returns>JsonString</returns>
        //发现/推荐 refer=feed  cid=null
        //
        //COS refer=channel  cid=6618800694038102275
        //绘画 refer=channel cid=6618800650505421059
        //写作 refer=channel cid=6618800677680316675
        //问答 refer=channel cid=6618029913129615630
        //
        //热门视频 refer=channel_video cid=8103
        //动漫杂谈 refer=channel_video cid=6643217672664252680
        //游戏 refer=channel_video cid=6643218469594595598
        //MAD refer=channel_video cid=6643219568665821447
        //MMD refer=channel_video cid=6643220014159626510
        //手书 refer=channel_video cid=6643220558852915464
        //影视 refer=channel_video cid=6643220861601972493
        //音乐&配音 refer=channel_video cid=6643221212145123592
        //萌宠 refer=channel_video cid=6643222302139891976
        //生活 refer=channel_video cid=6643222867930530062
        //舞蹈 refer=channel_video cid=6643688975691153672
        public static Task<string> GetFeeds(string cid, string refer = "channel", string direction = "loadmore")
        {
            string url = "https://bcy.net/apiv3/common/getFeeds?";
            url += "refer=" + refer;
            url += "&direction=" + direction;
            if (cid != null)
            {
                url += "&cid=" + cid;
            }
            return HttpGet(url);
        }
        public static Task<string> GetFeeds() => HttpGet("https://bcy.net/apiv3/common/getFeeds?refer=feed&direction=loadmore");
        public static Task<string> GetFeeds_Cos(string direction = "loadmore") => HttpGet($"https://bcy.net/apiv3/common/getFeeds?refer=channel&cid=6618800694038102275&direction={direction}");
        public static Task<string> GetFeeds_Draw(string direction = "loadmore") => HttpGet($"https://bcy.net/apiv3/common/getFeeds?refer=channel&cid=6618800650505421059&direction={direction}");
        public static Task<string> GetFeeds_Write(string direction = "loadmore") => HttpGet($"https://bcy.net/apiv3/common/getFeeds?refer=channel&cid=6618800677680316675&direction={direction}");
        public static Task<string> GetFeeds_Answer(string direction = "loadmore") => HttpGet($"https://bcy.net/apiv3/common/getFeeds?refer=channel&cid=6618029913129615630&direction={direction}");
        public static Task<string> GetFeeds_HotVideo(string direction = "loadmore") => HttpGet($"https://bcy.net/apiv3/common/getFeeds?refer=channel_video&cid=8103&direction={direction}");
        public static Task<string> GetFeeds_AnimeTalk(string direction = "loadmore") => HttpGet($"https://bcy.net/apiv3/common/getFeeds?refer=channel_video&cid=6643217672664252680&direction={direction}");
        public static Task<string> GetFeeds_Game(string direction = "loadmore") => HttpGet($"https://bcy.net/apiv3/common/getFeeds?refer=channel_video&cid=6643218469594595598&direction={direction}");
        public static Task<string> GetFeeds_MAD(string direction = "loadmore") => HttpGet($"https://bcy.net/apiv3/common/getFeeds?refer=channel_video&cid=6643219568665821447&direction={direction}");
        public static Task<string> GetFeeds_MMD(string direction = "loadmore") => HttpGet($"https://bcy.net/apiv3/common/getFeeds?refer=channel_video&cid=6643220014159626510&direction={direction}");
        public static Task<string> GetFeeds_Tegaki(string direction = "loadmore") => HttpGet($"https://bcy.net/apiv3/common/getFeeds?refer=channel_video&cid=6643220558852915464&direction={direction}");
        public static Task<string> GetFeeds_TV(string direction = "loadmore") => HttpGet($"https://bcy.net/apiv3/common/getFeeds?refer=channel_video&cid=6643220861601972493&direction={direction}");
        public static Task<string> GetFeeds_Music(string direction = "loadmore") => HttpGet($"https://bcy.net/apiv3/common/getFeeds?refer=channel_video&cid=6643221212145123592&direction={direction}");
        public static Task<string> GetFeeds_Cat(string direction = "loadmore") => HttpGet($"https://bcy.net/apiv3/common/getFeeds?refer=channel_video&cid=6643222302139891976&direction={direction}");
        public static Task<string> GetFeeds_Life(string direction = "loadmore") => HttpGet($"https://bcy.net/apiv3/common/getFeeds?refer=channel_video&cid=6643222867930530062&direction={direction}");
        public static Task<string> GetFeeds_Dance(string direction = "loadmore") => HttpGet($"https://bcy.net/apiv3/common/getFeeds?refer=channel_video&cid=6643688975691153672&direction={direction}");

        //18.https://bcy.net/apiv3/common/hotCircleList
        /// <summary>
        /// 热门圈子
        /// </summary>
        /// <param name="offset"></param>
        /// <returns>JsonString</returns>
        //
        public static Task<string> HotCircleList(int offset)
        {
            string url = "https://bcy.net/apiv3/common/hotCircleList?";
            url += "offset=" + offset.ToString();
            return HttpGet(url);
        }
        public static Task<string> HotCircleList() => HttpGet("https://bcy.net/apiv3/common/hotCircleList");

        //19.https://bcy.net/apiv3/rank/list/itemInfo
        /// <summary>
        /// 榜单
        /// </summary>
        /// <param name="p">page</param>
        /// <param name="ttype">illust,cos,novel,video</param>
        /// <param name="sub_type"></param>
        /// <param name="duration_type"></param>
        /// <param name="date">202001</param>
        /// <returns>JsonString</returns>
        //                      周榜(sub_type=week) 日榜(sub_type=lastday) 新人榜(sub_type=newPeople)
        //1.绘画榜(ttype=illust)
        //2.COS榜(ttype=cos)
        //3.写作榜(ttype=novel)
        //
        //4.视频(ttype=video) 
        //                          日榜duration_type=lastday 三日榜duration_type=3day 周榜duration_type=week 月榜duration_type=month(ERROR)
        //top榜sub_type=sitetop date=null
        //新人榜sub_type=newPeople date=202001
        public static Task<string> RankItemInfo(int p, string ttype, string sub_type, string duration_type = null, string date = null)
        {
            string url = "https://bcy.net/apiv3/rank/list/itemInfo?";
            url += "p=" + p.ToString();
            url += "&ttype=" + ttype;
            url += "&sub_type=" + sub_type;
            if (duration_type != null)
            {
                url += "&duration_type=" + duration_type;
            }
            if (date != null)
            {
                url += "&date=" + date;
            }
            return HttpGet(url);
        }
        public static Task<string> RankItemInfo_Illust_Week(int p, string date) => HttpGet($"https://bcy.net/apiv3/rank/list/itemInfo?p={p}&ttype=illust&sub_type=week&date={date}");
        public static Task<string> RankItemInfo_Illust_LastDay(int p, string date) => HttpGet($"https://bcy.net/apiv3/rank/list/itemInfo?p={p}&ttype=illust&sub_type=lastday&date={date}");
        public static Task<string> RankItemInfo_Illust_NewPeople(int p, string date) => HttpGet($"https://bcy.net/apiv3/rank/list/itemInfo?p={p}&ttype=illust&sub_type=newPeople&date={date}");
        public static Task<string> RankItemInfo_Cos_Week(int p, string date) => HttpGet($"https://bcy.net/apiv3/rank/list/itemInfo?p={p}&ttype=cos&sub_type=week&date={date}");
        public static Task<string> RankItemInfo_Cos_LastDay(int p, string date) => HttpGet($"https://bcy.net/apiv3/rank/list/itemInfo?p={p}&ttype=cos&sub_type=lastday&date={date}");
        public static Task<string> RankItemInfo_Cos_NewPeople(int p, string date) => HttpGet($"https://bcy.net/apiv3/rank/list/itemInfo?p={p}&ttype=cos&sub_type=newPeople&date={date}");
        public static Task<string> RankItemInfo_Novel_Week(int p, string date) => HttpGet($"https://bcy.net/apiv3/rank/list/itemInfo?p={p}&ttype=novel&sub_type=week&date={date}");
        public static Task<string> RankItemInfo_Novel_LastDay(int p, string date) => HttpGet($"https://bcy.net/apiv3/rank/list/itemInfo?p={p}&ttype=novel&sub_type=lastday&date={date}");
        public static Task<string> RankItemInfo_Novel_NewPeople(int p, string date) => HttpGet($"https://bcy.net/apiv3/rank/list/itemInfo?p={p}&ttype=novel&sub_type=newPeople&date={date}");
        public static Task<string> RankItemInfo_Video_SiteTop_LastDay(int p) => HttpGet($"https://bcy.net/apiv3/rank/list/itemInfo?p={p}&ttype=video&sub_type=sitetop&duration_type=lastday");
        public static Task<string> RankItemInfo_Video_SiteTop_3Day(int p) => HttpGet($"https://bcy.net/apiv3/rank/list/itemInfo?p={p}&ttype=video&sub_type=sitetop&duration_type=3day");
        public static Task<string> RankItemInfo_Novel_SiteTop_Week(int p) => HttpGet($"https://bcy.net/apiv3/rank/list/itemInfo?p={p}&ttype=video&sub_type=sitetop&duration_type=week");
        //public static Task<string> RankItemInfo_Novel_SiteTop_Month(int p) => HttpGet($"https://bcy.net/apiv3/rank/list/itemInfo?p={p}&ttype=video&sub_type=sitetop&duration_type=month");
        public static Task<string> RankItemInfo_Novel_NewPeople_LastDay(int p, string date) => HttpGet($"https://bcy.net/apiv3/rank/list/itemInfo?p={p}&date={date}&ttype=video&sub_type=newPeople&duration_type=lastday");
        public static Task<string> RankItemInfo_Novel_NewPeople_3Day(int p, string date) => HttpGet($"https://bcy.net/apiv3/rank/list/itemInfo?p={p}&date={date}&ttype=video&sub_type=newPeople&duration_type=3day");
        public static Task<string> RankItemInfo_Novel_NewPeople_Week(int p, string date) => HttpGet($"https://bcy.net/apiv3/rank/list/itemInfo?p={p}&date={date}&ttype=video&sub_type=newPeople&duration_type=week");
        //public static Task<string> RankItemInfo_Novel_NewPeople_Month(int p, string date) => HttpGet($"https://bcy.net/apiv3/rank/list/itemInfo?p={p}&date={date}&ttype=video&sub_type=newPeople&duration_type=month");

        //20.https://bcy.net/apiv3/cmt/reply/list
        /// <summary>
        /// 评论
        /// </summary>
        /// <param name="page"></param>
        /// <param name="item_id"></param>
        /// <param name="sort">hot,time</param>
        /// <returns>JsonString</returns>
        //热度顺序 sort=hot
        //发布顺序 sort=time
        public static Task<string> ReplyList(int page, string item_id, string sort)
        {
            string url = "https://bcy.net/apiv3/cmt/reply/list?";
            url += "page=" + page.ToString();
            url += "&item_id=" + item_id;
            url += "&sort=" + sort;
            return HttpGet(url);
        }
        public static Task<string> ReplyList_Hot(int page, string item_id) => ReplyList(page, item_id, "hot");
        public static Task<string> ReplyList_Time(int page, string item_id) => ReplyList(page, item_id, "time");

        //21.https://bcy.net/apiv3/cmt/reply/publish
        /// <summary>
        /// 发布评论
        /// </summary>
        /// <param name="content"></param>
        /// <param name="item_id"></param>
        /// <returns>JsonString</returns>
        //???string[] imgList
        public static Task<string> ReplyPublish(string content, string item_id)
        {
            string url = "https://bcy.net/apiv3/cmt/reply/publish";
            string data = "content=" + content;
            data += "&item_id=" + item_id;
            return HttpPost(url, data, true);
        }

        //22.https://bcy.net/apiv3/user/collect
        /// <summary>
        /// 进行收藏
        /// </summary>
        /// <param name="item_id"></param>
        /// <returns>JsonString</returns>
        public static Task<string> Collect(string item_id)
        {
            string url = "https://bcy.net/apiv3/user/collect";
            string data = "item_id=" + item_id;
            return HttpPost(url, data, true);
        }

        //23.https://bcy.net/apiv3/user/uncollect
        /// <summary>
        /// 取消收藏
        /// </summary>
        /// <param name="item_id"></param>
        /// <returns>JsonString</returns>
        public static Task<string> UnCollect(string item_id)
        {
            string url = "https://bcy.net/apiv3/user/uncollect";
            string data = "item_id=" + item_id;
            return HttpPost(url, data, true);
        }

        //23.https://bcy.net/apiv3/common/circleFeed
        /// <summary>
        /// 圈子
        /// </summary>
        /// <param name="circle_id"></param>
        /// <param name="since"></param>
        /// <param name="sort_type"></param>
        /// <param name="grid_type"></param>
        /// <returns></returns>
        public static Task<string> CircleFeed(string circle_id, string since, int sort_type = 2, int grid_type = 10)
        {
            string url = $"https://bcy.net/apiv3/common/circleFeed?circle_id={circle_id}&since={since}&sort_type={sort_type}&grid_type={grid_type}";
            return HttpGet(url);
        }
        public static Task<string> CircleFeed(string circle_id, int sort_type = 2, int grid_type = 10)
        {
            string url = $"https://bcy.net/apiv3/common/circleFeed?circle_id={circle_id}&sort_type={sort_type}&grid_type={grid_type}";
            return HttpGet(url);
        }
    }
}
