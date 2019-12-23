CREATE PROCEDURE [dbo].[Proc_Form_AddFormData]
@FormData nvarchar(max),
@FormTypeName varchar(255),
@CreatedBy int
AS 
BEGIN
declare @formTypeId int;
select @formTypeId = FormtypeId from FormType where FormName=@FormTypeName
Insert into Form(FormTypeId,FormData,CreatedBy)
values(@formTypeId,@FormData,@CreatedBy)
select  SCOPE_IDENTITY() as FormID;
END