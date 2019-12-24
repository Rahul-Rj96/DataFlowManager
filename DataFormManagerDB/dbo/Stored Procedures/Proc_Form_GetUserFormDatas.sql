 create procedure [dbo].[Proc_Form_GetUserFormDatas]
 @UserId int,
 @FormTypeName varchar(255)
 As
 begin
 declare @formTypeId int;
 select @formTypeId = FormTypeId from FormType where FormName=@FormTypeName
 select FormData from Form
 where Form.FormId in (select FormId from UserForms where UserForms.UserId=@UserId and Form.IsDeleted=0 and Form.FormTypeId=@formTypeId)
 end