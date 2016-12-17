CREATE PROCEDURE [dbo].[GetCosechaByCodigo]
(
	@Id varchar(100)
)
AS
BEGIN

	SET NOCOUNT ON;
	SELECT * FROM Cosecha WHERE Codigo = @Id

END
