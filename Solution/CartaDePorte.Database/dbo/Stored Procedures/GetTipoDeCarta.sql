CREATE PROCEDURE [dbo].[GetTipoDeCarta]
(
	@Id int
)
AS
BEGIN

	SET NOCOUNT ON;

	IF @Id = 0
	BEGIN
		SELECT * FROM TipoDeCarta WHERE Activo = 1
	END
	ELSE
	BEGIN
		SELECT * FROM TipoDeCarta WHERE IdTipoDeCarta = @Id
	END


END
