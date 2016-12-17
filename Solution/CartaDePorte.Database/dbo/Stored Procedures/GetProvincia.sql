CREATE PROCEDURE [dbo].[GetProvincia]
(
	@Id int,
	@IdPais int
)
AS
BEGIN

	SET NOCOUNT ON;
	IF @Id = -1 --0 es codigo valido
	BEGIN
		SELECT * FROM Provincia WHERE IdPais = @IdPais ORDER BY Codigo
	END
	ELSE
	BEGIN
		SELECT * FROM Provincia WHERE Codigo = @Id
	END
	
END
