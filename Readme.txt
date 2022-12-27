-- add Db, Members table, EFModels, RegisterVm
[V] add BookStore.Site mvc project
[V]Create BookStore db(bookstoreLogin, 123), Members table
	Id, int, PK, not null, identity
	Account, NVarchar(30), not null, unique
	EncryptedPassword, varchar(70), not null
	Email, nvarchar(256), not null
	Name, nvarchar(30), not null
	Mobile, varchar(10), allow null
	IsConfirmed, bit, allow null
	ConfirmCode, varchar(50), allow null
[V]add EFModels, AppDbContext, connection string='AppDbContext'
[V]add ViewModels/RegisterVM, DTOs/RegisterDto class
	add 擴充方法 RegisterDto ToRequestDto(this RegisterVM source)
[V]add MembersController, Register action, View Page: Register.cshtml

-- 實作 MemberService.CreateNewMember
目前功能只會新增新會員,但不會發確認信
[V]add /Models/Infrastructures/HashUtility.cs 用來將密碼加密
[V]modify RegisterDto
	add EncryptedPassword readonly property,存放加密後的密碼
	add ConfirmCode property,用來生成新會員確認信url時使用
[V] add /Models/Services/Interfaces/IMemberRepository.cs
[V] add /Models/Infrastructures/Repositories/MemberRepository.cs
[V]add /Models/Services/MemberService 
	method : (bool IsSuccess, string ErrorMessage) CreateNewMember(RegisterDto dto)
[V] add /Views/Members/RegisterConfirm.cshtml (empty 範本)
[v]modify MembersController
	add HttpPost Register action
[V]modify _Layout page
	加入'註冊新會員'連結

-- 實作 新會員 Email確認功能
正式啟用會員資格 , url template /members/activeRegister?memberId=99&confirmCode=xxx
[V] add /Models/DTOs/MemberDto.cs
[V]modify IMemberRepository
	add MemberDto Load(int memberId);
	add void ActiveRegister(int memberId);
[V]modify MemberRepository
	add MemberDto Load(int memberId);
	add void ActiveRegister(int memberId);
[V]modify MembersController
	add ActiveRegister action
[V]add /Views/Members/ActiveRegister.csthml

-- 實作 登入/登出網站
		只有帳密正確而且已正式開通的會員才允許登入, 實作之前, 請先各別建立一個已/未開通的會員記錄,方便測試
[V] modify web.config, add Authenthcation node
[V] 登入功能
	add /Models/ViewModels/LoginVM.cs
	modify MembersController add Login() action
	add  "Login" view page(使用 Create 範本)
	add /Models/Infrastructures/ExtensionMethods/MemberExts.cs (static class), add 擴充方法 MemberDto ToDto(this Member entity) 
	modify IMemberRepository , add GetByAccount(string account)
	modify MemberRepository, add GetByAccount(string account)
	modify RegisterDto, 將SALT 宣告成常數
	modify MemberService, add Login method
	modify MembersController, add HttpPost Login action(使用表單認證,寫入 cookie), private ProcessLogin method
[V] modify MemberController.About, add "Authorize" attribute
	若沒登入過,會自動導向到/Members/Login
	modify MembersController, add HttpPost Logout action
[V] modify _Layout page, add "Login/Logout" links


-- 實作 修改個人基本資料
[V]建立會員中心頁
	modify MembersController, add Index action
	add Views/Members/Index.cshtml(空白範本), 填入二個超連結 : ""修改個人基本資料", "重設密碼"
	改 web.config, form節點 defaultUrl="/Members/Index/"
[V]實作 修改個人基本資料
	modify MembersController,在class 加入Field :MemberService service,供各method共用
	add DTOs/UpdateProfileDto.cs
	add EditProfileVM.cs(作為稍後 EditProfile.cshtml 的model,由於不允許修改帳號,所以這類別裡沒有Account), 在此cs加入add MemberDtoExts class

	modify IMemberRepository, add void Update(MemberDto entity);
		modify MemberRepository, add void Update(MemberDto entity)
	modify MemberService, add UpdateProfile() method
	modify MembersController, add EditProfile actions(HttpGet, HttpPost二個 action)
	add "EditProfile" view page(使用 Edit 範本)

-- 實作 變更密碼
[V] add EditPasswordVM.cs
[V]add Models/DTOs/ChangePasswordRequest.cs
[V] add 擴充方法class EditPasswordVMExts,寫在EditPasswordVM.cs
[V]modify MembersController, add EditPassword action(HttpGet action)
[V] add EditPassword view page(用create 範本)
[V]modify MembersController, add HttpPost EditPassword action

[V]modify IMemberRepository, add void UpdatePassword(int memberId, string newEncryptedPassword);
		modify MemberRepository add UpdatePassword method
[V]modify MemberService, add ChangePassword()
[V]modify /Views/Members/Index, => <a href="/Members/EditPassword/">修改密碼</a>

-- 實作 忘記密碼/重設密碼
[working on]modify MemberExts.ToDto() 如果是null,就傳回null

[working on]modify Views/Members/Login.cshtml, 加入 '忘記密碼'超連結
[working on]add Views/Members/ConfirmForgetPassword.cshtml, 用空白範本
[working on]add /Models/ViewModels/ForgetPasswordVM.cs

[working on]add Models/Infrastructures/EmailHelper class, 撰寫寄信的功能
		建立 ~/files/ folder,用來放寄信的測試內容

[working on]modify MemberService add RequestResetPassword method

[working on]modify MembersController , add ForgetPassword action(HttpGet action)
[working on]add ForgetPassword view page(用Create 範本)
[working on]modify MembersController , add ForgetPassword action(HttpPost action)
[working on]modify MemerService add ResetPassword method

[working on]add Models/ViewModels/ResetPasswordVM
[working on]modify MembersController , add ResetPassword action(httpget action)
		add /Views/Members/ResetPassword.cshtml(create 範本)
		modify MembersController , add ResetPassword action(httpPost action)
		add ResetPasswordConfirm.csthml(空白範本)