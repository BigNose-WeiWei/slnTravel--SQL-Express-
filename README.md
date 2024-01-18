# 後端  
*  帳密授權驗證
*  使用者可建立、閱讀、修改、刪除資料庫的資料
*  限制使用者上傳檔案格式，以及檔案數量

# 前端
* Bootstrap框架
* 畫面採RWD設計

## 角色
Admin、Manager、User、訪客。   

Admin

    最高權限。  
    
Manager  

    除了不可修改Admin的基本資料外其餘跟Admin相同。

User  

    只可上傳檔案，並且只能編輯自己創建的文章。  

訪客

    只可觀看。

## 資料庫  
Member 使用者帳戶

    Muid    //帳號
    MPwd    //密碼
    MName   //名稱
    MMail   //信箱
    MRole   //角色 串Role的Rid
    MStatus //是否啟用 1 | 0  

Role 角色

    Rid     //Key
    RName   //名稱
    RStatus //是否啟用 1 | 0  

Classify 分類

    Cid      //編號
    CName    //分類別稱
    CStatus  //是否啟用 1 | 0  

Folder 文章資料

    FFolderId    //產品ID
    FCid         //屬於分類 串Classify的Cid
    FTitle       //標題
    FDescription //內容
    FPicture     //圖片Id 串FolderPicture的PFid
    FCreateUser  //創建帳號 串Member的Muid
    FCreateTime  //創建時間
    FEditUser    //最後修改帳號 串Member的Muid
    FEditTime    //最後修改時間
    FStatus      //是否啟用 1 | 0
    
Advertise 廣告資料

    AId        //文章編號
    ACid       //對應分類
    AFolderId  //對應的文章
    APicture   //圖片Id 串FolderPicture的PFid
    AStatus    //是否啟用 1 | 0
    ARow       //廣告順序

FolderPicture 圖片存放位置

    Pid              //編號
    PFid             //圖片Id
    PContentClassify //Title文章跑馬燈、Advertise廣告
    PPicture         //圖片在主機的檔案名稱
    PRow             //排序
