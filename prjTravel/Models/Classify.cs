using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace prjTravel.Models;

public partial class Classify
{
    [Display(Name = "編號")]
    public int Cid { get; set; }
    [Display(Name = "名稱")]
    [Required(ErrorMessage = "必填")]
    public string? Cname { get; set; }
    [Display(Name = "顯示狀態")]
    public int Cstatus { get; set; }
}
