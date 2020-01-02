CREATE PROCEDURE [dbo].[Proc_Form_GetFormsToAssign]
 As
 begin
 select FormData from Form
 where Form.FormId not in (select distinct FormId from UserForms) and Form.IsDeleted=0
 End