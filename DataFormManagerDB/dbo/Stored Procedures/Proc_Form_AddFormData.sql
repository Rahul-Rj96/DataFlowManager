CREATE PROCEDURE [dbo].[Proc_Form_AddFormData]
@FormData nvarchar(max),
@FormTypeName varchar(255)
AS 
BEGIN
declare @formTypeId int;
select @formTypeId = FormtypeId from FormType where FormName=@FormTypeName
Insert into Form(FormTypeId,FormData)
values(@formTypeId,@FormData)
select  SCOPE_IDENTITY() as FormID;
END