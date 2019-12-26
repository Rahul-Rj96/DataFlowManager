CREATE PROCEDURE [dbo].[Proc_Form_AddForm]
@FormTypeName varchar(255),
@CreatedBy int
AS 
BEGIN
declare @formTypeId int;
select @formTypeId = FormtypeId from FormType where FormName=@FormTypeName
Insert into Form(FormTypeId,CreatedBy)
values(@formTypeId,@CreatedBy)
select  SCOPE_IDENTITY() as FormID;
END