using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;


namespace SiteLib.Bcy.Net
{
    public enum FollowState { Havefollow, Unfollow };

    public enum EditorStatus { All_Public, More_Public, Nomore_Public, Public };

    public enum PostTagType { Event, Tag, Team, Work };

    public enum TypeSetType { Drawer, Week };

    public enum ItemDetailType { Article, Ganswer, Note, Video };

    public enum Key { Again_Check_Info, First_Check_Info, Machine_Review, No_Modify, No_Trans, Right_Click, Video_Type, Warm_Up, Water_Mark, Login_View, Fans_View, Can_Pay, Cover, Note_Access, Intro };

    public struct IntString
    {
        public string String;
        public static implicit operator IntString(long Integer) => new IntString { String = Integer.ToString() };
        public static implicit operator IntString(string String) => new IntString { String = String };
    }

    internal static class BcyConverter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }

    public class ItemsData
    {
        [JsonProperty("items")]
        public Item[] Items { get; set; }
    }

    public class ItemsCountData
    {
        [JsonProperty("items")]
        public Item[] Items { get; set; }

        [JsonProperty("total_count")]
        public long TotalCount { get; set; }
    }

    public class ItemInfoData
    {
        [JsonProperty("item_info")]
        public Item[] ItemInfo { get; set; }
    }

    public class ListData
    {
        [JsonProperty("list")]
        public Item[] List { get; set; }
    }

    public class Item
    {
        [JsonProperty("tl_type")]
        //public TlType TlType { get; set; }
        public string TlType { get; set; }

        [JsonProperty("since")]
        public string Since { get; set; }

        [JsonProperty("item_detail")]
        public ItemDetail ItemDetail { get; set; }
    }

    public class ItemUserTop
    {
        [JsonProperty("tl_type")]
        //public TlType TlType { get; set; }
        public string TlType { get; set; }

        [JsonProperty("since")]
        public string Since { get; set; }

        [JsonProperty("item_detail")]
        public ItemDetail ItemDetail { get; set; }

        //????
        [JsonProperty("user_top", NullValueHandling = NullValueHandling.Ignore)]
        public bool? UserTop { get; set; }
    }

    public class ItemDetail
    {
        [JsonProperty("item_id")]
        public string ItemId { get; set; }

        [JsonProperty("uid")]
        public long Uid { get; set; }

        [JsonProperty("uname")]
        public string Uname { get; set; }

        [JsonProperty("avatar")]
        public Uri Avatar { get; set; }

        [JsonProperty("odin_uid")]
        public string OdinUid { get; set; }

        [JsonProperty("value_user")]
        public long ValueUser { get; set; }

        [JsonProperty("vu_description")]
        public string VuDescription { get; set; }

        [JsonProperty("follow_state")]
        public FollowState FollowState { get; set; }

        [JsonProperty("rights")]
        public Right[] Rights { get; set; }

        [JsonProperty("ctime")]
        public long Ctime { get; set; }

        [JsonProperty("type")]
        public ItemDetailType Type { get; set; }

        //
        [JsonProperty("title")]
        public string Title { get; set; }

        //
        [JsonProperty("summary")]
        public string Summary { get; set; }

        [JsonProperty("plain")]
        public string Plain { get; set; }

        [JsonProperty("word_count")]
        public long WordCount { get; set; }

        [JsonProperty("cover", NullValueHandling = NullValueHandling.Ignore)]
        public Uri Cover { get; set; }

        [JsonProperty("multi")]
        public Multi[] Multi { get; set; }

        [JsonProperty("pic_num")]
        public long PicNum { get; set; }

        [JsonProperty("work")]
        public string Work { get; set; }

        [JsonProperty("wid", NullValueHandling = NullValueHandling.Ignore)]
        public long? Wid { get; set; }

        [JsonProperty("real_name", NullValueHandling = NullValueHandling.Ignore)]
        public string RealName { get; set; }

        [JsonProperty("work_cover", NullValueHandling = NullValueHandling.Ignore)]
        public string WorkCover { get; set; }

        [JsonProperty("post_tags")]
        public PostTag[] PostTags { get; set; }

        [JsonProperty("like_count")]
        public long LikeCount { get; set; }

        [JsonProperty("user_liked")]
        public bool UserLiked { get; set; }

        [JsonProperty("reply_count")]
        public long ReplyCount { get; set; }

        [JsonProperty("share_count")]
        public long ShareCount { get; set; }

        //
        [JsonProperty("group", NullValueHandling = NullValueHandling.Ignore)]
        public Group Group { get; set; }

        [JsonProperty("props")]
        public Prop[] Props { get; set; }

        [JsonProperty("replies")]
        public object[] Replies { get; set; }

        [JsonProperty("visible_level")]
        public long VisibleLevel { get; set; }

        [JsonProperty("user_favored")]
        public bool UserFavored { get; set; }

        [JsonProperty("image_list")]
        public ImageList[] ImageList { get; set; }

        //
        [JsonProperty("top_list_detail", NullValueHandling = NullValueHandling.Ignore)]
        public TopListDetail TopListDetail { get; set; }

        [JsonProperty("extra_properties")]
        public ExtraProperties ExtraProperties { get; set; }

        [JsonProperty("selected_status")]
        public long SelectedStatus { get; set; }

        [JsonProperty("selected_comment")]
        public string SelectedComment { get; set; }

        [JsonProperty("editor_status")]
        public EditorStatus EditorStatus { get; set; }

        //
        [JsonProperty("post_in_set", NullValueHandling = NullValueHandling.Ignore)]
        public bool? PostInSet { get; set; }

        [JsonProperty("view_count")]
        public long ViewCount { get; set; }

        //
        [JsonProperty("set_data", NullValueHandling = NullValueHandling.Ignore)]
        public SetData SetData { get; set; }

        //
        [JsonProperty("video_info", NullValueHandling = NullValueHandling.Ignore)]
        public VideoInfo VideoInfo { get; set; }

        [JsonProperty("repostable")]
        public bool Repostable { get; set; }

        [JsonProperty("repost_count")]
        public long RepostCount { get; set; }

        //
        [JsonProperty("highlight_circle")]
        public HighlightCircle[] HighlightCircle { get; set; }

        //
        [JsonProperty("at_user_infos", NullValueHandling = NullValueHandling.Ignore)]
        public AtUserInfo[] AtUserInfos { get; set; }

        //
        [JsonProperty("collection", NullValueHandling = NullValueHandling.Ignore)]
        public Collection Collection { get; set; }

        [JsonProperty("visible_status")]
        public long VisibleStatus { get; set; }

        [JsonProperty("visible_status_msg")]
        public string VisibleStatusMsg { get; set; }
    }

    public class ImageList
    {
        [JsonProperty("path")]
        public Uri Path { get; set; }

        [JsonProperty("type")]
        //public ImageListType? Type { get; set; }
        public string Type { get; set; }

        [JsonProperty("mid")]
        public long Mid { get; set; }

        [JsonProperty("w")]
        public long W { get; set; }

        [JsonProperty("h")]
        public long H { get; set; }

        [JsonProperty("original_path")]
        public string OriginalPath { get; set; }

        [JsonProperty("ratio")]
        public double Ratio { get; set; }

        [JsonProperty("visible_level")]
        public string VisibleLevel { get; set; }

        [JsonProperty("origin")]
        public string Origin { get; set; }
    }

    public class ExtraProperties
    {
        [JsonProperty("item_reply_disable")]
        public bool ItemReplyDisable { get; set; }
    }

    public class Multi
    {
        [JsonProperty("path")]
        public Uri Path { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }//MultiType

        [JsonProperty("mid")]
        public long Mid { get; set; }

        [JsonProperty("w")]
        public long W { get; set; }

        [JsonProperty("h")]
        public long H { get; set; }

        [JsonProperty("original_path")]
        public string OriginalPath { get; set; }

        [JsonProperty("ratio")]
        public double Ratio { get; set; }

        [JsonProperty("visible_level")]
        public string VisibleLevel { get; set; }

        [JsonProperty("origin")]
        public string Origin { get; set; }
    }

    public class PostTag
    {
        [JsonProperty("tag_id")]
        public long TagId { get; set; }

        [JsonProperty("tag_name")]
        public string TagName { get; set; }

        [JsonProperty("type")]
        public PostTagType Type { get; set; }

        [JsonProperty("cover")]
        public Uri Cover { get; set; }

        [JsonProperty("relative_wid")]
        public long RelativeWid { get; set; }

        [JsonProperty("event_id", NullValueHandling = NullValueHandling.Ignore)]
        public long? EventId { get; set; }
    }

    public class Prop
    {
        [JsonProperty("key")]
        public Key Key { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }
    }

    public class Right
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("rid")]
        public long Rid { get; set; }

        [JsonProperty("display_name")]
        public string DisplayName { get; set; }

        [JsonProperty("type")]
        public long Type { get; set; }

        [JsonProperty("expire_time")]
        public long ExpireTime { get; set; }

        [JsonProperty("extra")]
        public Uri Extra { get; set; }

        [JsonProperty("status")]
        public long Status { get; set; }

        [JsonProperty("active")]
        public bool Active { get; set; }

        [JsonProperty("own")]
        public bool Own { get; set; }
    }

    public class SetData
    {
        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("set_post_prev")]
        public string SetPostPrev { get; set; }

        [JsonProperty("set_post_next")]
        public IntString SetPostNext { get; set; }

        [JsonProperty("post_pos")]
        public long PostPos { get; set; }

        [JsonProperty("count")]
        public long Count { get; set; }

        [JsonProperty("subscribed")]
        public bool Subscribed { get; set; }

        [JsonProperty("subscribe_num")]
        public long SubscribeNum { get; set; }

        [JsonProperty("item_set_id")]
        public long ItemSetId { get; set; }
    }

    public class TopListDetail
    {
        [JsonProperty("rank")]
        public long Rank { get; set; }

        [JsonProperty("top_list_name")]
        public string TopListName { get; set; }

        [JsonProperty("bcy_url")]
        public string BcyUrl { get; set; }
    }

    public class VideoInfo
    {
        [JsonProperty("vid")]
        public string Vid { get; set; }

        [JsonProperty("duration")]
        public long Duration { get; set; }

        [JsonProperty("min_size")]
        public long MinSize { get; set; }

        [JsonProperty("width")]
        public long Width { get; set; }

        [JsonProperty("height")]
        public long Height { get; set; }

        [JsonProperty("danmaku_total")]
        public long DanmakuTotal { get; set; }

        [JsonProperty("visible_without_watermark")]
        public long VisibleWithoutWatermark { get; set; }

        [JsonProperty("danmaku_post")]
        public bool DanmakuPost { get; set; }

        [JsonProperty("danmaku_show")]
        public bool DanmakuShow { get; set; }
    }

}


namespace SiteLib.Bcy.Net
{
    public class BlockListJson
    {
        [JsonProperty("code")]
        public long Code { get; set; }

        [JsonProperty("msg")]
        public string Msg { get; set; }

        [JsonProperty("data")]
        public UserHashData Data { get; set; }

        public static BlockListJson FromJson(string json) => JsonConvert.DeserializeObject<BlockListJson>(json, BcyConverter.Settings);
    }

    public class UserHashData
    {
        [JsonProperty("userHash")]
        public UserHash[] UserHash { get; set; }
    }

    public class UserHash
    {
        [JsonProperty("uid")]
        public long Uid { get; set; }

        [JsonProperty("uname")]
        public string Uname { get; set; }

        [JsonProperty("avatar")]
        public Uri Avatar { get; set; }

        [JsonProperty("utags")]
        public string[] Utags { get; set; }
    }
}


namespace SiteLib.Bcy.Net
{
    public class FavorJson
    {
        [JsonProperty("data")]
        public ListData Data { get; set; }

        [JsonProperty("code")]
        public long Code { get; set; }

        public static FavorJson FromJson(string json) => JsonConvert.DeserializeObject<FavorJson>(json, BcyConverter.Settings);
    }
}

namespace SiteLib.Bcy.Net
{
    public class FollowListJson
    {
        [JsonProperty("code")]
        public long Code { get; set; }

        [JsonProperty("msg")]
        public string Msg { get; set; }

        [JsonProperty("data")]
        public UserFollowInfoData Data { get; set; }

        public static FollowListJson FromJson(string json) => JsonConvert.DeserializeObject<FollowListJson>(json, BcyConverter.Settings);
    }

    public class UserFollowInfoData
    {
        [JsonProperty("user_follow_info")]
        public UserFollowInfo[] UserFollowInfo { get; set; }
    }

    public class UserFollowInfo
    {
        [JsonProperty("uid")]
        public long Uid { get; set; }

        [JsonProperty("uname")]
        public string Uname { get; set; }

        [JsonProperty("avatar")]
        public Uri Avatar { get; set; }

        [JsonProperty("value_user")]
        public long ValueUser { get; set; }

        [JsonProperty("vu_description")]
        public string VuDescription { get; set; }

        [JsonProperty("utags")]
        public string[] Utags { get; set; }

        [JsonProperty("follow_state")]
        public FollowState FollowState { get; set; }

        [JsonProperty("rights")]
        public Right[] Rights { get; set; }

        [JsonProperty("self_intro")]
        public string SelfIntro { get; set; }
    }
}


namespace SiteLib.Bcy.Net
{
    public class FollowListCircleJson
    {
        [JsonProperty("code")]
        public long Code { get; set; }

        [JsonProperty("msg")]
        public string Msg { get; set; }

        [JsonProperty("data")]
        public UserFollowCirclesData Data { get; set; }

        public static FollowListCircleJson FromJson(string json) => JsonConvert.DeserializeObject<FollowListCircleJson>(json, BcyConverter.Settings);
    }

    public class UserFollowCirclesData
    {
        [JsonProperty("user_follow_circles")]
        public UserFollowCircle[] UserFollowCircles { get; set; }
    }

    public class UserFollowCircle
    {
        [JsonProperty("circle_type")]
        public long CircleType { get; set; }

        [JsonProperty("circle_id")]
        public long CircleId { get; set; }

        [JsonProperty("circle_name")]
        public string CircleName { get; set; }

        [JsonProperty("updated_time")]
        public long UpdatedTime { get; set; }

        [JsonProperty("cover")]
        public Uri Cover { get; set; }
    }
}

namespace SiteLib.Bcy.Net
{
    public class FriendsFeedJson
    {
        [JsonProperty("code")]
        public long Code { get; set; }

        [JsonProperty("msg")]
        public string Msg { get; set; }

        [JsonProperty("data")]
        public ItemInfoData Data { get; set; }

        public static FriendsFeedJson FromJson(string json) => JsonConvert.DeserializeObject<FriendsFeedJson>(json, BcyConverter.Settings);
    }


    public class AtUserInfo
    {
        [JsonProperty("at_uname")]
        public string AtUname { get; set; }

        [JsonProperty("uid")]
        public long Uid { get; set; }

        [JsonProperty("uname")]
        public string Uname { get; set; }
    }

    public class Collection
    {
        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("collection_id")]
        public long CollectionId { get; set; }

        [JsonProperty("user")]
        public User User { get; set; }

        [JsonProperty("collection_type")]
        public string CollectionType { get; set; }
    }

    public partial class User
    {
        [JsonProperty("uid")]
        public long Uid { get; set; }
    }

    public class Group
    {
        [JsonProperty("gid")]
        public long Gid { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("post_num")]
        public long PostNum { get; set; }

        [JsonProperty("member_num")]
        public long MemberNum { get; set; }
    }



    public class Reply
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("item_id")]
        public string ItemId { get; set; }

        [JsonProperty("content")]
        public string Content { get; set; }

        [JsonProperty("multi")]
        public Multi[] Multi { get; set; }

        [JsonProperty("ctime")]
        public long Ctime { get; set; }

        [JsonProperty("like_count")]
        public long LikeCount { get; set; }

        [JsonProperty("user_liked")]
        public bool UserLiked { get; set; }

        [JsonProperty("at_users")]
        public object[] AtUsers { get; set; }

        [JsonProperty("is_end")]
        public bool IsEnd { get; set; }

        [JsonProperty("uid")]
        public long Uid { get; set; }

        [JsonProperty("uname")]
        public string Uname { get; set; }

        [JsonProperty("avatar")]
        public Uri Avatar { get; set; }

        [JsonProperty("utags")]
        public Utag[] Utags { get; set; }

        [JsonProperty("value_user")]
        public long ValueUser { get; set; }

        [JsonProperty("vu_description")]
        public string VuDescription { get; set; }

        [JsonProperty("rights")]
        public Right[] Rights { get; set; }

        [JsonProperty("comments_count")]
        public long CommentsCount { get; set; }

        [JsonProperty("comments")]
        public object[] Comments { get; set; }

        [JsonProperty("post")]
        public User Post { get; set; }
    }
}

namespace SiteLib.Bcy.Net
{
    public class GetMyCollectionListJson
    {
        [JsonProperty("code")]
        public long Code { get; set; }

        [JsonProperty("msg")]
        public string Msg { get; set; }

        [JsonProperty("data")]
        public CollectionsData Data { get; set; }

        public static GetMyCollectionListJson FromJson(string json) => JsonConvert.DeserializeObject<GetMyCollectionListJson>(json, BcyConverter.Settings);
    }

    public class GetSubscribeCollectionListJson
    {
        [JsonProperty("code")]
        public long Code { get; set; }

        [JsonProperty("msg")]
        public string Msg { get; set; }

        [JsonProperty("data")]
        public CollectionsData Data { get; set; }

        public static GetSubscribeCollectionListJson FromJson(string json) => JsonConvert.DeserializeObject<GetSubscribeCollectionListJson>(json, BcyConverter.Settings);
    }

    public class CollectionsData
    {
        [JsonProperty("collections")]
        public CollectionElement[] Collections { get; set; }

        [JsonProperty("total")]
        public long Total { get; set; }
    }

    public class CollectionElement
    {
        [JsonProperty("collection")]
        public CollectionCollection Collection { get; set; }

        [JsonProperty("tags")]
        public Tag[] Tags { get; set; }

        [JsonProperty("user")]
        public User User { get; set; }
    }

    public class CollectionCollection
    {
        [JsonProperty("collection_id")]
        public long CollectionId { get; set; }

        [JsonProperty("uid")]
        public long Uid { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("intro")]
        public string Intro { get; set; }

        [JsonProperty("cover_uri")]
        public string CoverUri { get; set; }

        [JsonProperty("pv")]
        public long Pv { get; set; }

        [JsonProperty("article_num")]
        public long ArticleNum { get; set; }

        [JsonProperty("mtime")]
        public long Mtime { get; set; }

        [JsonProperty("bSubscribed")]
        public bool BSubscribed { get; set; }

        [JsonProperty("bClosed")]
        public bool BClosed { get; set; }

        [JsonProperty("since")]
        public string Since { get; set; }

        [JsonProperty("tags")]
        public string[] Tags { get; set; }

        [JsonProperty("subscribe_num")]
        public long SubscribeNum { get; set; }

        [JsonProperty("audit_status")]
        public long AuditStatus { get; set; }
    }

    public class Tag
    {
        [JsonProperty("circle_id")]
        public long CircleId { get; set; }

        [JsonProperty("circle_name")]
        public string CircleName { get; set; }

        [JsonProperty("circle_type")]
        public long CircleType { get; set; }

        [JsonProperty("relative_wid")]
        public long RelativeWid { get; set; }

        [JsonProperty("creator_uid")]
        public long CreatorUid { get; set; }

        [JsonProperty("admin_uid")]
        public long AdminUid { get; set; }

        [JsonProperty("nickname")]
        public string Nickname { get; set; }

        [JsonProperty("intro")]
        public string Intro { get; set; }

        [JsonProperty("editor_status")]
        public long EditorStatus { get; set; }

        [JsonProperty("admin_status")]
        public long AdminStatus { get; set; }

        [JsonProperty("ctime")]
        public long Ctime { get; set; }

        [JsonProperty("rtime")]
        public long Rtime { get; set; }

        [JsonProperty("follow_count")]
        public long FollowCount { get; set; }

        [JsonProperty("follow_threshold")]
        public long FollowThreshold { get; set; }

        [JsonProperty("follow_status")]
        public bool FollowStatus { get; set; }

        [JsonProperty("post_count")]
        public long PostCount { get; set; }

        [JsonProperty("event_url")]
        public string EventUrl { get; set; }

        [JsonProperty("category")]
        public long Category { get; set; }

        [JsonProperty("area")]
        public long Area { get; set; }

        [JsonProperty("event_id")]
        public long EventId { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("stime")]
        public long Stime { get; set; }

        [JsonProperty("etime")]
        public long Etime { get; set; }

        [JsonProperty("ds_time")]
        public long DsTime { get; set; }

        [JsonProperty("de_time")]
        public long DeTime { get; set; }

        [JsonProperty("heatout_time")]
        public long HeatoutTime { get; set; }

        [JsonProperty("event_include")]
        public string EventInclude { get; set; }

        [JsonProperty("tpl_name")]
        public string TplName { get; set; }
    }

    public partial class User
    {
        [JsonProperty("uname")]
        public string Uname { get; set; }

        [JsonProperty("following")]
        public long Following { get; set; }

        [JsonProperty("follower")]
        public long Follower { get; set; }

        [JsonProperty("avatar")]
        public Uri Avatar { get; set; }
    }
}

namespace SiteLib.Bcy.Net
{
    public class RankItemInfoJson
    {
        [JsonProperty("code")]
        public long Code { get; set; }

        [JsonProperty("msg")]
        public string Msg { get; set; }

        [JsonProperty("data")]
        public TopListItemInfoData Data { get; set; }

        public static RankItemInfoJson FromJson(string json) => JsonConvert.DeserializeObject<RankItemInfoJson>(json, BcyConverter.Settings);
    }

    public class TopListItemInfoData
    {
        [JsonProperty("top_list_item_info")]
        public TopListItemInfoItem[] TopListItemInfo { get; set; }
    }

    public class TopListItemInfoItem
    {
        [JsonProperty("tl_type")]
        //public TlType TlType { get; set; }
        public string TlType { get; set; }

        [JsonProperty("since")]
        public string Since { get; set; }

        [JsonProperty("item_detail")]
        public ItemDetail ItemDetail { get; set; }

        [JsonProperty("top_list_detail")]
        public TopListItemInfoTopListDetail TopListDetail { get; set; }
    }

    public class TopListItemInfoTopListDetail
    {
        [JsonProperty("ttype_set")]
        public TypeSet TtypeSet { get; set; }

        [JsonProperty("sub_type_set")]
        public TypeSet SubTypeSet { get; set; }

        [JsonProperty("stime")]
        public long Stime { get; set; }

        [JsonProperty("count")]
        public long Count { get; set; }

        [JsonProperty("rank")]
        public long Rank { get; set; }

        [JsonProperty("wave", NullValueHandling = NullValueHandling.Ignore)]
        public string Wave { get; set; }
    }

    public class TypeSet
    {
        [JsonProperty("type")]
        public TypeSetType Type { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }

}

namespace SiteLib.Bcy.Net
{
    public class ListJson
    {
        [JsonProperty("code")]
        public long Code { get; set; }

        [JsonProperty("msg")]
        public string Msg { get; set; }

        [JsonProperty("data")]
        public DatumData Data { get; set; }

        public static ListJson FromJson(string json) => JsonConvert.DeserializeObject<ListJson>(json, BcyConverter.Settings);
    }

    public class DatumData
    {
        [JsonProperty("data")]
        public Datum[] Data { get; set; }

        [JsonProperty("items")]
        public Item[] Items { get; set; }

        [JsonProperty("total_count")]
        public long TotalCount { get; set; }
    }

    public class Datum
    {
        [JsonProperty("id")]
        public IntString Id { get; set; }

        [JsonProperty("item_id")]
        public string ItemId { get; set; }

        [JsonProperty("content")]
        public string Content { get; set; }

        [JsonProperty("multi")]
        public Multi[] Multi { get; set; }

        [JsonProperty("ctime")]
        public long Ctime { get; set; }

        [JsonProperty("like_count")]
        public long LikeCount { get; set; }

        [JsonProperty("user_liked")]
        public bool UserLiked { get; set; }

        [JsonProperty("at_users")]
        public object[] AtUsers { get; set; }

        [JsonProperty("is_end")]
        public bool IsEnd { get; set; }

        [JsonProperty("uid")]
        public long Uid { get; set; }

        [JsonProperty("uname")]
        public string Uname { get; set; }

        [JsonProperty("avatar")]
        public Uri Avatar { get; set; }

        [JsonProperty("utags")]
        public Utag[] Utags { get; set; }

        [JsonProperty("value_user")]
        public long ValueUser { get; set; }

        [JsonProperty("vu_description")]
        public string VuDescription { get; set; }

        [JsonProperty("rights")]
        public Right[] Rights { get; set; }

        [JsonProperty("comments_count")]
        public long CommentsCount { get; set; }

        [JsonProperty("comments")]
        public Comment[] Comments { get; set; }

        [JsonProperty("relation")]
        public long Relation { get; set; }
    }

    public class Comment
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("reply_id")]
        public long ReplyId { get; set; }

        [JsonProperty("item_id")]
        public string ItemId { get; set; }

        [JsonProperty("content")]
        public string Content { get; set; }

        [JsonProperty("multi")]
        public Multi[] Multi { get; set; }

        [JsonProperty("ctime")]
        public long Ctime { get; set; }

        [JsonProperty("like_count")]
        public long LikeCount { get; set; }

        [JsonProperty("user_liked")]
        public bool UserLiked { get; set; }

        [JsonProperty("at_users")]
        public object[] AtUsers { get; set; }

        [JsonProperty("uid")]
        public long Uid { get; set; }

        [JsonProperty("uname")]
        public string Uname { get; set; }

        [JsonProperty("avatar")]
        public Uri Avatar { get; set; }

        [JsonProperty("utags")]
        public string[] Utags { get; set; }

        [JsonProperty("value_user")]
        public long ValueUser { get; set; }

        [JsonProperty("vu_description")]
        public string VuDescription { get; set; }

        [JsonProperty("rights")]
        public Right[] Rights { get; set; }

        [JsonProperty("relation")]
        public long Relation { get; set; }

        [JsonProperty("comment_to_user_id")]
        public long CommentToUserId { get; set; }

        [JsonProperty("comment_to_comment_id")]
        public long CommentToCommentId { get; set; }
    }

    public class Utag
    {
        [JsonProperty("ut_name")]
        public string UtName { get; set; }
    }
}

namespace SiteLib.Bcy.Net
{
    public class PcAnnounceJson
    {
        [JsonProperty("atype")]
        public long Atype { get; set; }

        [JsonProperty("aid")]
        public long Aid { get; set; }

        [JsonProperty("headline")]
        public string Headline { get; set; }

        [JsonProperty("avatar")]
        public Uri Avatar { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("content")]
        public string Content { get; set; }

        [JsonProperty("ctime")]
        public long Ctime { get; set; }

        [JsonProperty("data")]
        public string Data { get; set; }

        [JsonProperty("readStatus")]
        public long ReadStatus { get; set; }

        public static PcAnnounceJson[] FromJson(string json) => JsonConvert.DeserializeObject<PcAnnounceJson[]>(json, BcyConverter.Settings);
    }
}

namespace SiteLib.Bcy.Net
{
    public class PostJson
    {
        [JsonProperty("code")]
        public long Code { get; set; }

        [JsonProperty("msg")]
        public string Msg { get; set; }

        [JsonProperty("data")]
        public ItemsCountData Data { get; set; }

        public static PostJson FromJson(string json) => JsonConvert.DeserializeObject<PostJson>(json, BcyConverter.Settings);
    }
}

namespace SiteLib.Bcy.Net
{
    public class SelfPostJson
    {
        [JsonProperty("code")]
        public long Code { get; set; }

        [JsonProperty("msg")]
        public string Msg { get; set; }

        [JsonProperty("data")]
        public ItemsData Data { get; set; }

        public static SelfPostJson FromJson(string json) => JsonConvert.DeserializeObject<SelfPostJson>(json, BcyConverter.Settings);
    }

}

namespace SiteLib.Bcy.Net
{
    public class CircleFeedJson
    {
        [JsonProperty("code")]
        public long Code { get; set; }

        [JsonProperty("msg")]
        public string Msg { get; set; }

        [JsonProperty("data")]
        public ItemsData Data { get; set; }

        public static CircleFeedJson FromJson(string json) => JsonConvert.DeserializeObject<CircleFeedJson>(json, BcyConverter.Settings);
    }

}

namespace SiteLib.Bcy.Net
{
    public class GetFeedsJson
    {
        [JsonProperty("code")]
        public long Code { get; set; }

        [JsonProperty("msg")]
        public string Msg { get; set; }

        [JsonProperty("data")]
        public ItemInfoData Data { get; set; }

        public static GetFeedsJson FromJson(string json) => JsonConvert.DeserializeObject<GetFeedsJson>(json, BcyConverter.Settings);
    }

    public class HighlightCircle
    {
        [JsonProperty("circle_id")]
        public long CircleId { get; set; }

        [JsonProperty("circle_name")]
        public string CircleName { get; set; }

        [JsonProperty("circle_type")]
        public long CircleType { get; set; }

        [JsonProperty("relative_wid")]
        public long RelativeWid { get; set; }

        [JsonProperty("creator_uid")]
        public long CreatorUid { get; set; }

        [JsonProperty("admin_uid")]
        public long AdminUid { get; set; }

        [JsonProperty("nickname")]
        public string Nickname { get; set; }

        [JsonProperty("intro")]
        public string Intro { get; set; }

        [JsonProperty("cover")]
        public Cover Cover { get; set; }

        [JsonProperty("editor_status")]
        public long EditorStatus { get; set; }

        [JsonProperty("admin_status")]
        public long AdminStatus { get; set; }

        [JsonProperty("ctime")]
        public long Ctime { get; set; }

        [JsonProperty("rtime")]
        public long Rtime { get; set; }

        [JsonProperty("follow_count")]
        public long FollowCount { get; set; }

        [JsonProperty("follow_threshold")]
        public long FollowThreshold { get; set; }

        [JsonProperty("follow_status")]
        public bool FollowStatus { get; set; }

        [JsonProperty("post_count")]
        public long PostCount { get; set; }

        [JsonProperty("event_url")]
        public string EventUrl { get; set; }

        [JsonProperty("announces")]
        public Announce[] Announces { get; set; }

        [JsonProperty("category")]
        public long Category { get; set; }

        [JsonProperty("area")]
        public long Area { get; set; }

        [JsonProperty("event_id")]
        public long EventId { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("stime")]
        public long Stime { get; set; }

        [JsonProperty("etime")]
        public long Etime { get; set; }

        [JsonProperty("ds_time")]
        public long DsTime { get; set; }

        [JsonProperty("de_time")]
        public long DeTime { get; set; }

        [JsonProperty("heatout_time")]
        public long HeatoutTime { get; set; }

        [JsonProperty("event_include")]
        public string EventInclude { get; set; }

        [JsonProperty("tpl_name")]
        public string TplName { get; set; }

        [JsonProperty("is_custom_cover")]
        public long IsCustomCover { get; set; }
    }

    public class Announce
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("content")]
        public string Content { get; set; }

        [JsonProperty("ctime")]
        public long Ctime { get; set; }

        [JsonProperty("circle_id")]
        public long CircleId { get; set; }

        [JsonProperty("uid")]
        public long Uid { get; set; }
    }

    public class Cover
    {
        [JsonProperty("path")]
        public string Path { get; set; }

        [JsonProperty("url")]
        public Uri Url { get; set; }
    }
}

namespace SiteLib.Bcy.Net
{
    public class InfoJson
    {
        [JsonProperty("data")]
        public UserInfoData Data { get; set; }

        [JsonProperty("code")]
        public int Code { get; set; }

        public static InfoJson FromJson(string json) => JsonConvert.DeserializeObject<InfoJson>(json, BcyConverter.Settings);
    }
    public class UtagsItem
    {
        [JsonProperty("ut_id")]
        public int UtagID { get; set; }

        [JsonProperty("ut_name")]
        public string UtagName { get; set; }
    }

    public class UserInfoData
    {
        [JsonProperty("uid")]
        public string Uid { get; set; }

        [JsonProperty("uname")]
        public string Uname { get; set; }

        [JsonProperty("self_intro")]
        public string SelfIntro { get; set; }

        [JsonProperty("following")]
        public int Following { get; set; }

        [JsonProperty("follower")]
        public int Follower { get; set; }

        [JsonProperty("value_user")]
        public int ValueUser { get; set; }

        [JsonProperty("sex")]
        public int Sex { get; set; }

        [JsonProperty("avatar")]
        public string Avatar { get; set; }

        [JsonProperty("follow_state")]
        public string FollowState { get; set; }

        [JsonProperty("show_role")]
        public string ShowRole { get; set; }

        [JsonProperty("utags")]
        public UtagsItem[] Utags { get; set; }

        [JsonProperty("show_utags")]
        public string ShowUtags { get; set; }
    }
}

namespace SiteLib.Bcy.Net
{
    public class MessageJson
    {
        [JsonProperty("code")]
        public long Code { get; set; }

        [JsonProperty("msg")]
        public string Msg { get; set; }

        [JsonProperty("data")]
        public NotifiesData Data { get; set; }

        public static MessageJson FromJson(string json) => JsonConvert.DeserializeObject<MessageJson>(json, BcyConverter.Settings);
    }

    public class NotifiesItemData
    {
        [JsonProperty("item_id")]
        public string ItemId { get; set; }

        [JsonProperty("reply_id")]
        public string ReplyId { get; set; }

        [JsonProperty("desc")]
        public string Desc { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("cover")]
        public string Cover { get; set; }

        [JsonProperty("plain")]
        public string Plain { get; set; }
    }

    public class NotifiesItem
    {
        [JsonProperty("nid")]
        public int Nid { get; set; }

        [JsonProperty("from_uid")]
        public int FromUid { get; set; }

        [JsonProperty("uname")]
        public string Uname { get; set; }

        [JsonProperty("avatar")]
        public string Avatar { get; set; }

        [JsonProperty("ntype")]
        public int Ntype { get; set; }

        [JsonProperty("data")]
        public NotifiesItemData Data { get; set; }

        [JsonProperty("ctime")]
        public int Ctime { get; set; }

        [JsonProperty("value_user")]
        public int ValueUser { get; set; }

        [JsonProperty("vu_description")]
        public string VuDescription { get; set; }

        [JsonProperty("read_status")]
        public int ReadStatus { get; set; }

        [JsonProperty("version")]
        public string Version { get; set; }

        [JsonProperty("from_uids_num")]
        public int FromUidsNum { get; set; }
    }

    public class NotifiesData
    {
        [JsonProperty("notifies")]
        public NotifiesItem[] Notifies { get; set; }

        [JsonProperty("count")]
        public int Count { get; set; }
    }
}

namespace SiteLib.Bcy.Net
{
    public class HotCircleListJson
    {
        [JsonProperty("code")]
        public int Code { get; set; }

        [JsonProperty("msg")]
        public string Msg { get; set; }

        [JsonProperty("Data")]
        public InfosData Data { get; set; }

        public static HotCircleListJson FromJson(string json) => JsonConvert.DeserializeObject<HotCircleListJson>(json, BcyConverter.Settings);
    }

    public class AffichesItem
    {
        [JsonProperty("link")]
        public string Link { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }
    }

    public class AnnouncesItem
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("content")]
        public string Content { get; set; }

        [JsonProperty("ctime")]
        public int Ctime { get; set; }

        [JsonProperty("circle_id")]
        public int CircleId { get; set; }

        [JsonProperty("uid")]
        public int Uid { get; set; }
    }

    public class Info
    {
        [JsonProperty("circle_id")]
        public int CircleId { get; set; }

        [JsonProperty("circle_name")]
        public string CircleName { get; set; }

        [JsonProperty("circle_type")]
        public int CircleType { get; set; }

        [JsonProperty("relative_wid")]
        public int RelativeWid { get; set; }

        [JsonProperty("creator_uid")]
        public int CreatorUid { get; set; }

        [JsonProperty("admin_uid")]
        public int AdminUid { get; set; }

        [JsonProperty("nickname")]
        public string Nickname { get; set; }

        [JsonProperty("intro")]
        public string Intro { get; set; }

        [JsonProperty("cover")]
        public Cover Cover { get; set; }

        [JsonProperty("editor_status")]
        public int EditorStatus { get; set; }

        [JsonProperty("admin_status")]
        public int AdminStatus { get; set; }

        [JsonProperty("ctime")]
        public int Ctime { get; set; }

        [JsonProperty("rtime")]
        public int Rtime { get; set; }

        [JsonProperty("follow_count")]
        public int FollowCount { get; set; }

        [JsonProperty("follow_threshold")]
        public int FollowThreshold { get; set; }

        [JsonProperty("follow_status")]
        public string FollowStatus { get; set; }

        [JsonProperty("post_count")]
        public int PostCount { get; set; }

        [JsonProperty("event_url")]
        public string EventUrl { get; set; }

        [JsonProperty("affiches")]
        public AffichesItem[] Affiches { get; set; }

        [JsonProperty("announces")]
        public AnnouncesItem[] Announces { get; set; }

        [JsonProperty("category")]
        public int Category { get; set; }

        [JsonProperty("area")]
        public int Area { get; set; }

        [JsonProperty("event_id")]
        public int EventId { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("stime")]
        public int Stime { get; set; }

        [JsonProperty("etime")]
        public int Etime { get; set; }

        [JsonProperty("ds_time")]
        public int DsTime { get; set; }

        [JsonProperty("de_time")]
        public int DeTime { get; set; }

        [JsonProperty("heatout_time")]
        public int HeatoutTime { get; set; }

        [JsonProperty("event_include")]
        public string EventInclude { get; set; }

        [JsonProperty("tpl_name")]
        public string TplName { get; set; }

        [JsonProperty("is_custom_cover")]
        public int IsCustomCover { get; set; }
    }

    public class InfosItem
    {
        [JsonProperty("info")]
        public Info Info { get; set; }

        [JsonProperty("user_map")]
        public object UserMap { get; set; }
    }

    public class InfosData
    {
        [JsonProperty("infos")]
        public InfosItem[] Infos { get; set; }
    }
}

namespace SiteLib.Bcy.Net
{
    public class ReplyListJson
    {
        [JsonProperty("code")]
        public int Code { get; set; }

        [JsonProperty("msg")]
        public string Msg { get; set; }

        [JsonProperty("Data")]
        public ReplyListData Data { get; set; }

        public static ReplyListJson FromJson(string json) => JsonConvert.DeserializeObject<ReplyListJson>(json, BcyConverter.Settings);
    }

    public class CommentsItem
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("reply_id")]
        public string ReplyId { get; set; }

        [JsonProperty("item_id")]
        public string ItemId { get; set; }

        [JsonProperty("content")]
        public string Content { get; set; }

        [JsonProperty("multi")]
        public string[] Multi { get; set; }

        [JsonProperty("ctime")]
        public int Ctime { get; set; }

        [JsonProperty("like_count")]
        public int LikeCount { get; set; }

        [JsonProperty("user_liked")]
        public string UserLiked { get; set; }

        [JsonProperty("at_users")]
        public string[] AtUsers { get; set; }

        [JsonProperty("uid")]
        public int Uid { get; set; }

        [JsonProperty("uname")]
        public string Uname { get; set; }

        [JsonProperty("avatar")]
        public string Avatar { get; set; }

        [JsonProperty("utags")]
        public UtagsItem[] Utags { get; set; }

        [JsonProperty("value_user")]
        public int ValueUser { get; set; }

        [JsonProperty("vu_description")]
        public string VuDescription { get; set; }

        [JsonProperty("rights")]
        public string[] Rights { get; set; }

        [JsonProperty("relation")]
        public int Relation { get; set; }

        [JsonProperty("comment_to_user_id")]
        public int CommentToUserId { get; set; }

        [JsonProperty("comment_to_comment_id")]
        public int CommentToCommentId { get; set; }
    }

    public class ReplyData
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("item_id")]
        public string ItemId { get; set; }

        [JsonProperty("content")]
        public string Content { get; set; }

        [JsonProperty("multi")]
        public string[] Multi { get; set; }

        [JsonProperty("ctime")]
        public int Ctime { get; set; }

        [JsonProperty("like_count")]
        public int LikeCount { get; set; }

        [JsonProperty("user_liked")]
        public string UserLiked { get; set; }

        [JsonProperty("at_users")]
        public string[] AtUsers { get; set; }

        [JsonProperty("is_end")]
        public string IsEnd { get; set; }

        [JsonProperty("uid")]
        public int Uid { get; set; }

        [JsonProperty("uname")]
        public string Uname { get; set; }

        [JsonProperty("avatar")]
        public string Avatar { get; set; }

        [JsonProperty("utags")]
        public UtagsItem[] Utags { get; set; }

        [JsonProperty("value_user")]
        public int ValueUser { get; set; }

        [JsonProperty("vu_description")]
        public string VuDescription { get; set; }

        [JsonProperty("rights")]
        public string[] Rights { get; set; }

        [JsonProperty("comments_count")]
        public int CommentsCount { get; set; }

        [JsonProperty("comments")]
        public CommentsItem[] Comments { get; set; }

        [JsonProperty("relation")]
        public int Relation { get; set; }
    }

    public class ReplyListData
    {
        [JsonProperty("data")]
        public ReplyData[] Data { get; set; }
    }
}