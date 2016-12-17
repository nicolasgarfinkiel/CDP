CREATE PROCEDURE [dbo].[GetEstablecimiento]
(
	@Id int,
	@IdEmpresa int
)
AS
BEGIN

	SET NOCOUNT ON;
	IF @Id = 0
	BEGIN
		SELECT * FROM Establecimiento WHERE IdEmpresa = @IdEmpresa and Activo = 1
	END
	ELSE
	BEGIN
		SELECT * FROM Establecimiento WHERE IdEstablecimiento = @Id
	END
	
END
