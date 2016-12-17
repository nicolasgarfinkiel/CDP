CREATE PROCEDURE [dbo].[GetChofer]
(
	@Id int,
	@IdGrupoEmpresa int
)
AS
BEGIN

	SET NOCOUNT ON;

	IF @Id = 0
	BEGIN
		SELECT * FROM Chofer WHERE IdGrupoEmpresa = @IdGrupoEmpresa and Activo = 1
	END
	ELSE
	BEGIN
		SELECT * FROM Chofer WHERE IdChofer = @Id
	END


END
