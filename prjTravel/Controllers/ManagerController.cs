using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using prjTravel.Models;
using System.Security.Claims;

namespace prjTravel.Controllers
{
    public class ManagerController : Controller
    {

        private TravelDbContext _dbContext;
        private string _Path;

        public ManagerController(TravelDbContext dbContext, IWebHostEnvironment hostEnvironment)
        {
            _dbContext = dbContext;
            _Path = $"{hostEnvironment.WebRootPath}\\pictures";
        }

        //Manager主頁
        [Authorize(Roles ="Admin,Manager")]
        public IActionResult Index()
        {
            var classifyItem = _dbContext.Classifies
                .OrderByDescending(m => m.Cid)
                .ToList();

            return View(classifyItem);
        }

        //刪除分類方法
        //public IActionResult ClassifyDelete(int Cid)
        //{
        //    var classify = _dbContext.Classifies.FirstOrDefault(m => m.Cid == Cid)!;
        //    var folders = _dbContext.Folders.Where(m => m.Fcid == Cid)!;

        //    //依序刪除該分類照片
        //    foreach (var item in folders)
        //    {
        //        System.IO.File.Delete($"{_Path}\\{item.Fpicture}");
        //    }

        //    _dbContext.Folders.RemoveRange(folders);
        //    _dbContext.Classifies.Remove(classify);
        //    _dbContext.SaveChanges();

        //    TempData["Success"] = "成功刪除分類";
        //    return RedirectToAction("Index");
        //}

        //新增分類頁面
        //public IActionResult ClassifyCreate()
        //{
        //    return View();
        //}

        //新增分類方法
        //[HttpPost]
        //public IActionResult ClassifyCreate(Classify classify)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            classify.Cstatus = 1;    //創建時狀態預設給1
        //            _dbContext.Classifies.Add(classify);

        //            _dbContext.SaveChanges();
        //            TempData["Success"] = "新增分類成功";
        //            return RedirectToAction("Index");
        //        }
        //        catch (Exception)
        //        {
        //            TempData["Error"] = "新增分類失敗";
        //        }
        //    }

        //    return View();
        //}

        //修改分類顯示狀態
        public IActionResult ClassifyStatus(int Cid)
        {
            var classify = _dbContext.Classifies.FirstOrDefault(m => m.Cid == Cid)!;

            if (classify.Cstatus == 1)
            {
                classify.Cstatus = 0;
                _dbContext.SaveChanges();
                TempData["Success"] = $"已成功關閉『{classify.Cname}』分類";
            }
            else if (classify.Cstatus == 0)
            {
                classify.Cstatus = 1;
                _dbContext.SaveChanges();
                TempData["Success"] = $"已成功開啟『{classify.Cname}』分類";
            }

            return RedirectToAction("Index");
        }

        //修改類別名稱頁面
        public IActionResult ClassifyEdit(int Cid)
        {
            var classify = _dbContext.Classifies.FirstOrDefault(m => m.Cid == Cid);

            return View(classify);
        }

        //修改類別名稱方法
        [HttpPost]
        public IActionResult ClassifyEdit(Classify classify)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    int Cid = classify.Cid;
                    var temp = _dbContext.Classifies.FirstOrDefault(m => m.Cid == Cid)!;

                    temp.Cname = classify.Cname;
                    _dbContext.SaveChanges();

                    TempData["Success"] = "相簿分類名稱修改成功";
                    return RedirectToAction("Index");
                }
                catch (Exception)
                {
                    TempData["Success"] = "相簿分類名稱修改失敗";
                }
            }
            return View(classify);
        }

        //依相簿分類編號取得該分類的所有照片
        public IActionResult FolderClassify(int Cid = 1)
        {
            ViewBag.Classify = _dbContext.Classifies.FirstOrDefault(m => m.Cid == Cid)!.Cname;

            var Folders = _dbContext.Folders
                .Where(m => m.Fcid == Cid)
                .ToList();

            return View(Folders);
        }

        //修改資料顯示狀態
        public IActionResult FolderStatus(string Fid)
        {
            var folder = _dbContext.Folders.FirstOrDefault(m => m.FfolderId == Fid)!;

            if (folder.Fstatus == 1)
            {
                folder.Fstatus = 0;
                _dbContext.SaveChanges();
                TempData["Success"] = $"已成功關閉『{folder.Ftitle}』資料";
            }
            else if (folder.Fstatus == 0)
            {
                folder.Fstatus = 1;
                _dbContext.SaveChanges();
                TempData["Success"] = $"已成功開啟『{folder.Ftitle}』資料";
            }

            return RedirectToAction("FolderClassify", new { Cid = folder.Fcid });
        }

        //刪除資料
        public IActionResult FolderDelete(string Fid)
        {
            var folder = _dbContext.Folders.FirstOrDefault(m => m.FfolderId == Fid)!;
            System.IO.File.Delete($"{_Path}\\{folder.Fpicture}");

            _dbContext.Folders.Remove(folder);
            _dbContext.SaveChanges();
            TempData["Success"] = "照片刪除成功";
            return RedirectToAction("FolderClassify", new { Cid = folder.Fcid });
        }

        //顯示會員列表
        public IActionResult MemberList()
        {
            var MemberItem = _dbContext.Members
                .OrderBy(m => m.Mrole)
                .Where(m => m.Mrole != "Admin");

            return View(MemberItem);
        }

        //會員列表改變帳號狀態
        [HttpPost]
        public IActionResult MemberList(Member member)
        {
            try
            {
                string uid = member.Muid;
                var memberItem = _dbContext.Members.FirstOrDefault(m => m.Muid == uid)!;

                memberItem.Mstatus = member.Mstatus;

                TempData["Success"] = $"已成功修改{member.Muid}權限";
                _dbContext.SaveChanges();
            }
            catch (Exception)
            {
                TempData["Error"] = $"已成功修改{member.Muid}權限";
            }

            return View(member);
        }

        //修改會員狀態
        public IActionResult MemberStatus(string Uid)
        {
            var folder = _dbContext.Members.FirstOrDefault(m => m.Muid == Uid)!;

            if (folder.Mstatus == 1)
            {
                folder.Mstatus = 0;
                _dbContext.SaveChanges();
                TempData["Success"] = $"已成功停用『{folder.Muid}』帳號";
            }
            else if (folder.Mstatus == 0)
            {
                folder.Mstatus = 1;
                _dbContext.SaveChanges();
                TempData["Success"] = $"已成功啟用『{folder.Muid}』帳號";
            }

            return RedirectToAction("MemberList");
        }

        //修改會員資料畫面
        public IActionResult MemberEdit(string Uid)
        {
            var memberEdit = _dbContext.Members.FirstOrDefault(m => m.Muid == Uid);

            return View(memberEdit);
        }

        //修改會員資料方法
        [HttpPost]
        public IActionResult MemberEdit(Member member)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    string uid = member.Muid;
                    var memberTemp = _dbContext.Members.FirstOrDefault(m => m.Muid == uid)!;

                    memberTemp.Mpwd = member.Mpwd;
                    memberTemp.Mname = member.Mname;
                    memberTemp.Mmail = member.Mmail;
                    memberTemp.Mrole = member.Mrole;

                    _dbContext.SaveChanges();
                    TempData["Success"] = $"{member.Mname}會員資料修改成功";
                    return RedirectToAction("MemberList");
                }
                catch (Exception)
                {
                    TempData["Error"] = "會員修改失敗";
                }
            }

            return View(member);
        }

        //刪除會員方法
        //public IActionResult MemberDelete(string Uid)
        //{
        //    try
        //    {
        //        var MemberItem = _dbContext.Members.FirstOrDefault(m => m.Muid == Uid)!;

        //        _dbContext.Members.Remove(MemberItem);
        //        _dbContext.SaveChanges();

        //        TempData["Success"] = "會員刪除成功";
        //        return RedirectToAction("MemberList");
        //    }
        //    catch (Exception)
        //    {
        //        TempData["Error"] = "會員刪除失敗";
        //    }

        //    return View();
        //}

        //新增會員頁面
        public IActionResult MemberCreate()
        {
            return View();
        }

        //新增會員方法
        [HttpPost]
        public IActionResult MemberCreate(Member member)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    member.Mstatus = 1;
                    _dbContext.Members.Add(member);
                    _dbContext.SaveChanges();

                    TempData["Success"] = "新用戶建立成功";
                    return RedirectToAction("MemberList");
                }
                catch (Exception)
                {
                    TempData["Success"] = "新用戶建立失敗, 也許是帳號重複";
                }
            }

            return View();
        }

        //新增照片頁面
        public IActionResult FolderUpload()
        {
            return View();
        }

        //新增資料方法
        [HttpPost]
        public async Task<IActionResult> FolderUpload(Folder folder, IFormFile formFile)
        {
            string Errormsg;
            string fileName;
            string savePath = "";

            if (ModelState.IsValid)
            {
                if (formFile != null && formFile.Length > 0)
                {
                    try
                    {
                        /*圖片檔案名稱給予隨機值、設定路徑，並使用非同步方式存檔。*/
                        //fileName = $"{Guid.NewGuid()}.jpg"; 我要直接從DB洗資料進去，如果圖檔變成隨機碼我認不出來先取消。
                        fileName = $"{folder.Fcid}_{folder.FfolderId}.jpg";
                        savePath = $"{_Path}\\{fileName}";
                        using (var steam = new FileStream(savePath, FileMode.Create))
                        {
                            await formFile.CopyToAsync(steam);
                        }

                        /*存入檔案名稱以及建立時間*/
                        folder.Fpicture = fileName;
                        folder.FcreateUser = User.FindFirst(ClaimTypes.Sid)!.Value; // 紀錄創建者ID
                        folder.FcreateTime = DateTime.Now;
                        folder.FeditUser = User.FindFirst(ClaimTypes.Sid)!.Value;   // 紀錄創建者ID
                        folder.FeditTime = DateTime.Now;

                        _dbContext.Folders.Add(folder);
                        _dbContext.SaveChanges();
                        TempData["Success"] = "資料上傳";
                        return RedirectToAction("FolderClassify", new { Cid = folder.Fcid });
                    }
                    catch (Exception)
                    {
                        Errormsg = "可能是編號重複";
                        System.IO.File.Delete($"{savePath}");   //建立失敗就刪掉圖檔
                    }
                }
                else
                {
                    Errormsg = "上傳圖片格式有誤";
                }
            }
            else
            {
                Errormsg = "資料驗證失敗";
            }

            TempData["Error"] = $"資料上傳失敗, {Errormsg}";
            return View();
        }
    }
}
