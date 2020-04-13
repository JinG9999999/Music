using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Music.HC.Models
{
    public class MusicModel //唱片表
    {
        [Key]
        [Display(Name = "编号")]
        [Required]
        public int Mid { get; set; }
        [Display(Name = "音乐名称")]
        [Required]
        public string CorpName { get; set; } //音乐名称
        [Display(Name = "唱片封面")]
        [Required]
        public string Cover { get; set; } //唱片封面
        [Display(Name = "唱片标签")]
        [Required]
        public string Tags { get; set; } //唱片标签
        [Display(Name = "制作人")]
        [Required]
        public string Maker { get; set; } //制作人
        [Display(Name = "演唱")]
        [Required]
        public string Singer { get; set; } //演唱
        [Display(Name = "音乐公司")]
        [Required]
        public int CorpModelCid { get; set; } //音乐公司 外键
        [Display(Name = "发布日期")]
        [Required]
        public DateTime ReleaseDate { get; set; } //发布日期
        [Display(Name = "价格")]
        [Required]
        public int Price { get; set; } //价格
        [Display(Name = "添加人姓名")]
        [Required]
        public string Adder { get; set; } //添加人姓名
        [Display(Name = "唱片详情")]
        [Required]
        public string CdDetail { get; set; } //唱片详情
        public virtual CorpModel CorpModels { get; set; }
    }
    public class CorpModel //唱片公司表
    {
        [Key]
        public int Cid { get; set; }
        [Display(Name = "音乐公司")]
        [Required]
        public string CorpName { get; set; } //音乐公司
        [Display(Name = "公司地址")]
        [Required]
        public string Location { get; set; } //公司地址
    }

    public class UserModel //登录表
    {
        [Key]
        public int Uid { get; set; }
        [Display(Name = "用户姓名")]
        [Required]
        public string UserName { get; set; }
        [Display(Name = "用户密码")]
        [Required]
        public string Password { get; set; }
    }

    public class PagedInfo
    {
        public List<MusicModel> musics { get; set; }
        public int TotalConut { get; set; }   //记录条数
        public int TotalPage { get; set; }    //总页数
        public int currentPage { get; set; }  //当前页
    }
}