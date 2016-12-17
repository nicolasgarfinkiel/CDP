CREATE PROCEDURE [dbo].[DeleteC1116ADetalle]
(
	@Id int
)

AS
BEGIN

	SET NOCOUNT ON;
	delete from c1116aDetalle where Idc1116a = @Id 


END
GO
