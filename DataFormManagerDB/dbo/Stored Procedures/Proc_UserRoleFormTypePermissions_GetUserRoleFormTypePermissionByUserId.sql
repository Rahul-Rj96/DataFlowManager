








CREATE PROCEDURE [dbo].[Proc_UserRoleFormTypePermissions_GetUserRoleFormTypePermissionByUserId]
    @UserId int
AS
BEGIN

SET NOCOUNT ON

SELECT Username,RoleName,FormName,PermisssionName
FROM RCKRUser
INNER JOIN UserRoles
ON RCKRUser.UserId = UserRoles.UserId
INNER JOIN Role
ON Role.RoleId = UserRoles.RoleId
INNER JOIN FormTypePermissions
ON Role.RoleId = formtypepermissions.RoleId
INNER JOIN FormType
ON FormType.Formtypeid = formtypepermissions.FormTypeId
INNER JOIN Permission
ON permission.PermissionId = FormTypePermissions.PermissionId
where RckrUser.userId =@UserId
	
END