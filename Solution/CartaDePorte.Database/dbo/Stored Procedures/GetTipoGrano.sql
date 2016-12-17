CREATE PROCEDURE [dbo].[GetTipoGrano]
(
	@Id int
)
AS
BEGIN

	SET NOCOUNT ON;

	IF @Id = 0
	BEGIN
		SELECT * FROM TipoGrano
	END
	ELSE
	BEGIN
		SELECT * FROM TipoGrano WHERE IdTipoGrano = @Id
	END


END
