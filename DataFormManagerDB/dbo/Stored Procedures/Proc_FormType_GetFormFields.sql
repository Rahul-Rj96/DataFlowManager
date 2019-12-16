 CREATE procedure [dbo].[Proc_FormType_GetFormFields]
 @FormName varchar(255)
 As
 begin
 select FormConfig from FormType
 where FormName = @FormName
 end