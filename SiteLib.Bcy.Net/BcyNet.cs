using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

//半次元JsonAPI
namespace SiteLib.Bcy.Net
{
    public static class BcyNet
    {
        /// <summary>
        /// 查询用户信息
        /// </summary>
        public static async Task<UserInfoData> Info(string uid)
            => InfoJson.FromJson(await BcyJson.Info(uid)).Data;

        /// <summary>
        /// 我的消息
        /// </summary>
        /// <param name="isPre">"n"</param>
        /// <param name="page">1</param>
        /// <param name="pageType">"notify" or "remind" or "itemsetupdate"</param>
        /// <returns></returns>
        private static async Task<NotifiesItem[]> Message(string isPre, int page, string pageType)
            => MessageJson.FromJson(await BcyJson.Message(isPre, page, pageType)).Data.Notifies;

        public static async Task<NotifiesItem[]> Message_Notify(int page)
            => MessageJson.FromJson(await BcyJson.Message_Notify(page)).Data.Notifies;

        public static async Task<NotifiesItem[]> Message_Remind(int page)
            => MessageJson.FromJson(await BcyJson.Message_Remind(page)).Data.Notifies;

        public static async Task<NotifiesItem[]> Message_ItemSetUpdate(int page)
            => MessageJson.FromJson(await BcyJson.Message_ItemSetUpdate(page)).Data.Notifies;


        /// <summary>
        /// 公告
        /// </summary>
        public static async Task<PcAnnounceJson[]> PcAnnounce(string since, int limit)
            => PcAnnounceJson.FromJson(await BcyJson.PcAnnounce(since, limit));

        public static async Task<PcAnnounceJson[]> PcAnnounce()
            => PcAnnounceJson.FromJson(await BcyJson.PcAnnounce());

        /// <summary>
        /// 我创建的合集
        /// </summary>
        public static async Task<CollectionElement[]> GetMyCollectionList(string since, int sort = 0)
            => GetMyCollectionListJson.FromJson(await BcyJson.GetMyCollectionList(since, sort)).Data.Collections;

        public static async Task<CollectionElement[]> GetMyCollectionList()
            => GetMyCollectionListJson.FromJson(await BcyJson.GetMyCollectionList()).Data.Collections;

        /// <summary>
        /// 我关注的合集
        /// </summary>
        public static async Task<CollectionElement[]> GetSubscribeCollectionList(string since, int sort = 0)
            => GetMyCollectionListJson.FromJson(await BcyJson.GetSubscribeCollectionList(since, sort)).Data.Collections;

        public static async Task<CollectionElement[]> GetSubscribeCollectionList()
            => GetMyCollectionListJson.FromJson(await BcyJson.GetSubscribeCollectionList()).Data.Collections;

        /// <summary>
        /// 关注、粉丝、关注的圈子
        /// </summary>
        /// <param name="uid">self_uid</param>
        /// <param name="page">1</param>
        /// <param name="follow_type">0-3</param>
        /// <returns></returns>
        //我的关注 follow_type=0
        //我的粉丝 follow_type=1
        //互相关注 follow_type=2
        //我关注的圈子 follow_type=3
        public static async Task<UserFollowInfo[]> FollowList(string uid, int page, int follow_type)
            => FollowListJson.FromJson(await BcyJson.FollowList(uid, page, follow_type)).Data.UserFollowInfo;

        /// <summary>
        /// 黑名单列表
        /// </summary>
        /// <param name="uid">self_uid</param>
        public static async Task<UserHash[]> BlockList(string uid, int page)
            => BlockListJson.FromJson(await BcyJson.BlockList(uid, page)).Data.UserHash;

        public static async Task<UserHash[]> AllBlockList(string uid)
        {
            int page = 1;

            string s = await BcyJson.BlockList(uid, page++);
            var r = BlockListJson.FromJson(s);

            s = await BcyJson.BlockList(uid, page++);
            while (s.Length > BcyJson.MinValidJsonLen)
            {
                r.Data.UserHash = r.Data.UserHash.Union(BlockListJson.FromJson(s).Data.UserHash).ToArray();
                s = await BcyJson.BlockList(uid, page++);
            }
            return r.Data.UserHash;
        }


        /// <summary>
        /// 加入或移出黑名单
        /// </summary>
        /// <param name="actionType">1 or 2</param>
        /// <param name="bid">block_uid</param>
        /// <returns></returns>
        //加入黑名单 actionType: 1
        //移出黑名单 actionType: 2
        public static async Task<bool> Block(int actionType, string bid)
            => (await BcyJson.Block(actionType, bid)).Length > BcyJson.MinValidJsonLen;


        //11.https://BcyJson.net/apiv3/user/selfPosts
        /// <summary>
        /// 用户所有发布内容
        /// </summary>
        public static async Task<Item[]> SelfPosts(string uid, string since)
            => SelfPostJson.FromJson(await BcyJson.SelfPosts(uid, since)).Data.Items;

        public static async Task<Item[]> SelfPosts(string uid)
            => SelfPostJson.FromJson(await BcyJson.SelfPosts(uid)).Data.Items;

        /// <summary>
        /// 用户分类发布内容
        /// </summary>
        public static async Task<Item[]> Post_Note(string mid, int page, string uid)
            => PostJson.FromJson(await BcyJson.Post_Note(mid, page, uid)).Data.Items;

        public static async Task<Item[]> Post_Article(string mid, int page, string uid)
            => PostJson.FromJson(await BcyJson.Post_Article(mid, page, uid)).Data.Items;

        public static async Task<Item[]> Post_Ganswer(string mid, int page, string uid)
            => PostJson.FromJson(await BcyJson.Post_Article(mid, page, uid)).Data.Items;

        public static async Task<Item[]> Post_Video(string mid, int page, string uid)
            => PostJson.FromJson(await BcyJson.Post_Article(mid, page, uid)).Data.Items;

        /// <summary>
        /// 用户喜欢和收藏
        /// </summary>
        public static async Task<Item[]> Favor_Like(string uid, string mid, string since, int size = 35)
            => PostJson.FromJson(await BcyJson.Favor_Like(uid, mid, since, size)).Data.Items;

        public static async Task<Item[]> Favor_Collect(string uid, string mid, string since, int size = 35)
            => PostJson.FromJson(await BcyJson.Favor_Collect(uid, mid, since, size)).Data.Items;

        /// <summary>
        /// 关注或取消关注
        /// </summary>
        public static async Task<bool> Follow_DoFollow(string fid)
            => (await BcyJson.Follow_DoFollow(fid)).Length > BcyJson.MinValidJsonLen;

        public static async Task<bool> Follow_UnFollow(string fid)
            => (await BcyJson.Follow_UnFollow(fid)).Length > BcyJson.MinValidJsonLen;

        /// <summary>
        /// 发送私信
        /// </summary>
        public static async Task<bool> SendMessage(string content, string to_uid)
            => (await BcyJson.SendMessage(content, to_uid)).Length > BcyJson.MinValidJsonLen;

        public static async Task<bool> SendMessage_FeedBackToMaid(string content)
            => (await BcyJson.SendMessage_FeedBackToMaid(content)).Length > BcyJson.MinValidJsonLen;


        /// <summary>
        /// 关注动态(work by cookie)
        /// </summary>
        public static async Task<Item[]> FriendsFeed(string since)
            => FriendsFeedJson.FromJson(await BcyJson.FriendsFeed(since)).Data.ItemInfo;

        public static async Task<Item[]> FriendsFeed()
            => FriendsFeedJson.FromJson(await BcyJson.FriendsFeed()).Data.ItemInfo;

        /// <summary>
        /// 获取推荐(ok)或特定频道内容(error:20200611)
        /// </summary>
        /// <param name="cid">channel_id</param>
        /// <param name="refer">feed,channel/channel_video（Just use channel is OK!）</param>
        /// <param name="direction"></param>
        public static async Task<Item[]> GetFeeds(string cid, string refer = "channel", string direction = "loadmore")
            => GetFeedsJson.FromJson(await BcyJson.GetFeeds(cid, refer, direction)).Data.ItemInfo;

        public static async Task<Item[]> GetFeeds()
            => GetFeedsJson.FromJson(await BcyJson.GetFeeds()).Data.ItemInfo;

        public static async Task<Item[]> GetFeeds_Cos(string direction = "loadmore")
            => GetFeedsJson.FromJson(await BcyJson.GetFeeds_Cos(direction)).Data.ItemInfo;

        public static async Task<Item[]> GetFeeds_Draw(string direction = "loadmore")
            => GetFeedsJson.FromJson(await BcyJson.GetFeeds_Draw(direction)).Data.ItemInfo;

        public static async Task<Item[]> GetFeeds_Write(string direction = "loadmore")
            => GetFeedsJson.FromJson(await BcyJson.GetFeeds_Write(direction)).Data.ItemInfo;

        public static async Task<Item[]> GetFeeds_Answer(string direction = "loadmore")
            => GetFeedsJson.FromJson(await BcyJson.GetFeeds_Answer(direction)).Data.ItemInfo;

        public static async Task<Item[]> GetFeeds_HotVideo(string direction = "loadmore")
            => GetFeedsJson.FromJson(await BcyJson.GetFeeds_HotVideo(direction)).Data.ItemInfo;

        public static async Task<Item[]> GetFeeds_AnimeTalk(string direction = "loadmore")
            => GetFeedsJson.FromJson(await BcyJson.GetFeeds_AnimeTalk(direction)).Data.ItemInfo;

        public static async Task<Item[]> GetFeeds_Game(string direction = "loadmore")
            => GetFeedsJson.FromJson(await BcyJson.GetFeeds_Game(direction)).Data.ItemInfo;

        public static async Task<Item[]> GetFeeds_MAD(string direction = "loadmore")
            => GetFeedsJson.FromJson(await BcyJson.GetFeeds_MAD(direction)).Data.ItemInfo;

        public static async Task<Item[]> GetFeeds_MMD(string direction = "loadmore")
            => GetFeedsJson.FromJson(await BcyJson.GetFeeds_MMD(direction)).Data.ItemInfo;

        public static async Task<Item[]> GetFeeds_Tegaki(string direction = "loadmore")
            => GetFeedsJson.FromJson(await BcyJson.GetFeeds_Tegaki(direction)).Data.ItemInfo;

        public static async Task<Item[]> GetFeeds_TV(string direction = "loadmore")
            => GetFeedsJson.FromJson(await BcyJson.GetFeeds_TV(direction)).Data.ItemInfo;

        public static async Task<Item[]> GetFeeds_Music(string direction = "loadmore")
            => GetFeedsJson.FromJson(await BcyJson.GetFeeds_Music(direction)).Data.ItemInfo;

        public static async Task<Item[]> GetFeeds_Cat(string direction = "loadmore")
            => GetFeedsJson.FromJson(await BcyJson.GetFeeds_Cat(direction)).Data.ItemInfo;

        public static async Task<Item[]> GetFeeds_Life(string direction = "loadmore")
            => GetFeedsJson.FromJson(await BcyJson.GetFeeds_Life(direction)).Data.ItemInfo;

        public static async Task<Item[]> GetFeeds_Dance(string direction = "loadmore")
            => GetFeedsJson.FromJson(await BcyJson.GetFeeds_Dance(direction)).Data.ItemInfo;

        /// <summary>
        /// 热门圈子
        /// </summary>
        public static async Task<InfosItem[]> HotCircleList(int offset)
            => HotCircleListJson.FromJson(await BcyJson.HotCircleList(offset)).Data.Infos;

        public static async Task<InfosItem[]> HotCircleList()
            => HotCircleListJson.FromJson(await BcyJson.HotCircleList()).Data.Infos;

        /// <summary>
        /// 榜单
        /// </summary>
        public static async Task<TopListItemInfoItem[]> RankItemInfo_Illust_Week(int p, string date)
            => RankItemInfoJson.FromJson(await BcyJson.RankItemInfo_Illust_Week(p, date)).Data.TopListItemInfo;

        public static async Task<TopListItemInfoItem[]> RankItemInfo_Illust_LastDay(int p, string date)
            => RankItemInfoJson.FromJson(await BcyJson.RankItemInfo_Illust_LastDay(p, date)).Data.TopListItemInfo;

        public static async Task<TopListItemInfoItem[]> RankItemInfo_Illust_NewPeople(int p, string date)
            => RankItemInfoJson.FromJson(await BcyJson.RankItemInfo_Illust_NewPeople(p, date)).Data.TopListItemInfo;

        public static async Task<TopListItemInfoItem[]> RankItemInfo_Cos_Week(int p, string date)
            => RankItemInfoJson.FromJson(await BcyJson.RankItemInfo_Cos_Week(p, date)).Data.TopListItemInfo;

        public static async Task<TopListItemInfoItem[]> RankItemInfo_Cos_LastDay(int p, string date)
            => RankItemInfoJson.FromJson(await BcyJson.RankItemInfo_Cos_LastDay(p, date)).Data.TopListItemInfo;

        public static async Task<TopListItemInfoItem[]> RankItemInfo_Cos_NewPeople(int p, string date)
            => RankItemInfoJson.FromJson(await BcyJson.RankItemInfo_Cos_NewPeople(p, date)).Data.TopListItemInfo;

        public static async Task<TopListItemInfoItem[]> RankItemInfo_Novel_Week(int p, string date)
            => RankItemInfoJson.FromJson(await BcyJson.RankItemInfo_Novel_Week(p, date)).Data.TopListItemInfo;

        public static async Task<TopListItemInfoItem[]> RankItemInfo_Novel_LastDay(int p, string date)
            => RankItemInfoJson.FromJson(await BcyJson.RankItemInfo_Novel_LastDay(p, date)).Data.TopListItemInfo;

        public static async Task<TopListItemInfoItem[]> RankItemInfo_Novel_NewPeople(int p, string date)
            => RankItemInfoJson.FromJson(await BcyJson.RankItemInfo_Novel_NewPeople(p, date)).Data.TopListItemInfo;

        public static async Task<TopListItemInfoItem[]> RankItemInfo_Video_SiteTop_LastDay(int p)
            => RankItemInfoJson.FromJson(await BcyJson.RankItemInfo_Video_SiteTop_LastDay(p)).Data.TopListItemInfo;

        public static async Task<TopListItemInfoItem[]> RankItemInfo_Video_SiteTop_3Day(int p)
            => RankItemInfoJson.FromJson(await BcyJson.RankItemInfo_Video_SiteTop_3Day(p)).Data.TopListItemInfo;

        public static async Task<TopListItemInfoItem[]> RankItemInfo_Novel_SiteTop_Week(int p)
            => RankItemInfoJson.FromJson(await BcyJson.RankItemInfo_Novel_SiteTop_Week(p)).Data.TopListItemInfo;


        public static async Task<TopListItemInfoItem[]> RankItemInfo_Novel_NewPeople_LastDay(int p, string date)
            => RankItemInfoJson.FromJson(await BcyJson.RankItemInfo_Novel_NewPeople_LastDay(p, date)).Data.TopListItemInfo;

        public static async Task<TopListItemInfoItem[]> RankItemInfo_Novel_NewPeople_3Day(int p, string date)
            => RankItemInfoJson.FromJson(await BcyJson.RankItemInfo_Novel_NewPeople_3Day(p, date)).Data.TopListItemInfo;

        public static async Task<TopListItemInfoItem[]> RankItemInfo_Novel_NewPeople_Week(int p, string date)
            => RankItemInfoJson.FromJson(await BcyJson.RankItemInfo_Novel_NewPeople_Week(p, date)).Data.TopListItemInfo;

        /// <summary>
        /// 评论
        /// </summary>
        public static async Task<ReplyData[]> ReplyList_Hot(int page, string item_id)
            => ReplyListJson.FromJson(await BcyJson.ReplyList(page, item_id, "hot")).Data.Data;

        public static async Task<ReplyData[]> ReplyList_Time(int page, string item_id)
            => ReplyListJson.FromJson(await BcyJson.ReplyList(page, item_id, "time")).Data.Data;

        /// <summary>
        /// 发布评论
        /// </summary>
        public static async Task<bool> ReplyPublish(string content, string item_id)
            => (await BcyJson.ReplyPublish(content, item_id)).Length > BcyJson.MinValidJsonLen;

        /// <summary>
        /// 进行收藏
        /// </summary>
        public static async Task<bool> Collect(string item_id)
            => (await BcyJson.Collect(item_id)).Length > BcyJson.MinValidJsonLen;

        /// <summary>
        /// 取消收藏
        /// </summary>
        public static async Task<bool> UnCollect(string item_id)
            => (await BcyJson.Collect(item_id)).Length > BcyJson.MinValidJsonLen;

        public static async Task<Item[]> CircleFeed(string circle_id, int sort_type = 2, int grid_type = 10)
            => CircleFeedJson.FromJson(await BcyJson.CircleFeed(circle_id, sort_type, grid_type)).Data.Items;
    }
}
