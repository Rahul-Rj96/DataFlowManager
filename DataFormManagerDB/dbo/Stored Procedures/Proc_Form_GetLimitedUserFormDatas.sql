

 CREATE procedure [dbo].[Proc_Form_GetLimitedUserFormDatas]
 @UserId int,
 @FormTypeName varchar(255),
 @Start int,
 @Count int
 As
	BEGIN
	declare @formTypeId int;

		IF (@FormTypeName = 'all')
		BEGIN
			select FormData from Form
			where Form.FormId in (select FormId from UserForms where UserForms.UserId=@UserId and Form.IsDeleted=0) 
			order by CreatedDateTime offset @Start rows fetch next @Count rows only
		END

		ELSE

		BEGIN
			select @formTypeId = FormTypeId from FormType where FormName=@FormTypeName
			select FormData from Form
			where Form.FormId in (select FormId from UserForms where UserForms.UserId=@UserId and Form.IsDeleted=0 and Form.FormTypeId=@formTypeId)
			order by CreatedDateTime offset @Start rows fetch next @Count rows only
		END
	END