 create procedure [dbo].[Proc_Form_GetUserFormDatas]
 @UserId int
 As
 begin
 select FormData from Form
 where Form.FormId in (select FormId from UserForms where UserForms.UserId=@UserId)
 end