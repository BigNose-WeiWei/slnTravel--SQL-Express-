using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace prjTravel.Models;

public partial class Member
{
    [Display(Name = "帳號")]
    [Required(ErrorMessage = "必填")]
    public string Muid { get; set; } = null!;
    [Display(Name = "密碼")]
    [Required(ErrorMessage = "必填")]
    public string? Mpwd { get; set; }
    [Display(Name = "姓名")]
    public string? Mname { get; set; }
    [Display(Name = "信箱")]
    [Required(ErrorMessage = "必填")]
    [EmailAddress(ErrorMessage = "必須符合信箱格式")]
    public string? Mmail { get; set; }
    [Display(Name = "角色")]
    public string? Mrole { get; set; }
    [Display(Name = "成員狀態")]
    public int? Mstatus { get; set; }
}
