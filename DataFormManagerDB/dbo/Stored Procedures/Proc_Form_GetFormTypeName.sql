CREATE PROCEDURE [dbo].[Proc_Form_GetFormTypeName]
 @FormId int
 As
 begin
 declare @formTypeId int;
 select @formTypeId = FormTypeId from Form where FormId=@FormId
 select FormName from FormType
 where FormTypeId = @formTypeId
 end