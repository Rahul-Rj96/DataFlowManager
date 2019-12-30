CREATE PROCEDURE [dbo].[Proc_Form_UpdateFormData]
@FormData varchar(max),
@FormId int
AS 
BEGIN
Update Form
Set FormData = @FormData , UpdatedDateTime = GETDATE()
where Form.FormId = @FormId
END