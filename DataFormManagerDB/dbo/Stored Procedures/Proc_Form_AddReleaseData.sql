CREATE PROCEDURE [dbo].[Proc_Form_AddReleaseData]
@FormData nvarchar(max)
AS 
BEGIN
Insert into Form(FormTypeId,FormData)
values(1,@FormData)
select  SCOPE_IDENTITY() as FormID;
END