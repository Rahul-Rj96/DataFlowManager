CREATE PROCEDURE [dbo].[Proc_Form_DeleteFormData]
@FormId int
AS 
BEGIN
Update Form
Set IsDeleted = 1
where Form.FormId = @FormId
END

