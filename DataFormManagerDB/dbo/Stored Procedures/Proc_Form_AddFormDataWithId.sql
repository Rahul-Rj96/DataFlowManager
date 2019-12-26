CREATE PROCEDURE [dbo].[Proc_Form_AddFormDataWithId]
@FormData nvarchar(max),
@FormId int
AS 
BEGIN
Update Form
Set FormData = @FormData
where Form.FormId=@FormId
END