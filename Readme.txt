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
	add �X�R��k RegisterDto ToRequestDto(this RegisterVM source)
[V]add MembersController, Register action, View Page: Register.cshtml

-- ��@ MemberService.CreateNewMember
�ثe�\��u�|�s�W�s�|��,�����|�o�T�{�H
[V]add /Models/Infrastructures/HashUtility.cs �ΨӱN�K�X�[�K
[V]modify RegisterDto
	add EncryptedPassword readonly property,�s��[�K�᪺�K�X
	add ConfirmCode property,�Ψӥͦ��s�|���T�{�Hurl�ɨϥ�
[V] add /Models/Services/Interfaces/IMemberRepository.cs
[V] add /Models/Infrastructures/Repositories/MemberRepository.cs
[V]add /Models/Services/MemberService 
	method : (bool IsSuccess, string ErrorMessage) CreateNewMember(RegisterDto dto)
[V] add /Views/Members/RegisterConfirm.cshtml (empty �d��)
[v]modify MembersController
	add HttpPost Register action
[V]modify _Layout page
	�[�J'���U�s�|��'�s��

-- ��@ �s�|�� Email�T�{�\��
�����ҥη|����� , url template /members/activeRegister?memberId=99&confirmCode=xxx
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

-- ��@ �n�J/�n�X����
		�u���b�K���T�ӥB�w�����}�q���|���~���\�n�J, ��@���e, �Х��U�O�إߤ@�Ӥw/���}�q���|���O��,��K����
[V] modify web.config, add Authenthcation node
[V] �n�J�\��
	add /Models/ViewModels/LoginVM.cs
	modify MembersController add Login() action
	add  "Login" view page(�ϥ� Create �d��)
	add /Models/Infrastructures/ExtensionMethods/MemberExts.cs (static class), add �X�R��k MemberDto ToDto(this Member entity) 
	modify IMemberRepository , add GetByAccount(string account)
	modify MemberRepository, add GetByAccount(string account)
	modify RegisterDto, �NSALT �ŧi���`��
	modify MemberService, add Login method
	modify MembersController, add HttpPost Login action(�ϥΪ��{��,�g�J cookie), private ProcessLogin method
[V] modify MemberController.About, add "Authorize" attribute
	�Y�S�n�J�L,�|�۰ʾɦV��/Members/Login
	modify MembersController, add HttpPost Logout action
[V] modify _Layout page, add "Login/Logout" links


-- ��@ �ק�ӤH�򥻸��
[V]�إ߷|�����߭�
	modify MembersController, add Index action
	add Views/Members/Index.cshtml(�ťսd��), ��J�G�ӶW�s�� : ""�ק�ӤH�򥻸��", "���]�K�X"
	�� web.config, form�`�I defaultUrl="/Members/Index/"
[V]��@ �ק�ӤH�򥻸��
	modify MembersController,�bclass �[�JField :MemberService service,�ѦUmethod�@��
	add DTOs/UpdateProfileDto.cs
	add EditProfileVM.cs(�@���y�� EditProfile.cshtml ��model,�ѩ󤣤��\�ק�b��,�ҥH�o���O�̨S��Account), �b��cs�[�Jadd MemberDtoExts class

	modify IMemberRepository, add void Update(MemberDto entity);
		modify MemberRepository, add void Update(MemberDto entity)
	modify MemberService, add UpdateProfile() method
	modify MembersController, add EditProfile actions(HttpGet, HttpPost�G�� action)
	add "EditProfile" view page(�ϥ� Edit �d��)

-- ��@ �ܧ�K�X
[V] add EditPasswordVM.cs
[V]add Models/DTOs/ChangePasswordRequest.cs
[V] add �X�R��kclass EditPasswordVMExts,�g�bEditPasswordVM.cs
[V]modify MembersController, add EditPassword action(HttpGet action)
[V] add EditPassword view page(��create �d��)
[V]modify MembersController, add HttpPost EditPassword action

[V]modify IMemberRepository, add void UpdatePassword(int memberId, string newEncryptedPassword);
		modify MemberRepository add UpdatePassword method
[V]modify MemberService, add ChangePassword()
[V]modify /Views/Members/Index, => <a href="/Members/EditPassword/">�ק�K�X</a>

-- ��@ �ѰO�K�X/���]�K�X
[working on]modify MemberExts.ToDto() �p�G�Onull,�N�Ǧ^null

[working on]modify Views/Members/Login.cshtml, �[�J '�ѰO�K�X'�W�s��
[working on]add Views/Members/ConfirmForgetPassword.cshtml, �Ϊťսd��
[working on]add /Models/ViewModels/ForgetPasswordVM.cs

[working on]add Models/Infrastructures/EmailHelper class, ���g�H�H���\��
		�إ� ~/files/ folder,�Ψө�H�H�����դ��e

[working on]modify MemberService add RequestResetPassword method

[working on]modify MembersController , add ForgetPassword action(HttpGet action)
[working on]add ForgetPassword view page(��Create �d��)
[working on]modify MembersController , add ForgetPassword action(HttpPost action)
[working on]modify MemerService add ResetPassword method

[working on]add Models/ViewModels/ResetPasswordVM
[working on]modify MembersController , add ResetPassword action(httpget action)
		add /Views/Members/ResetPassword.cshtml(create �d��)
		modify MembersController , add ResetPassword action(httpPost action)
		add ResetPasswordConfirm.csthml(�ťսd��)