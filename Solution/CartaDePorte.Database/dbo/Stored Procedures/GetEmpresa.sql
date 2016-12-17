CREATE PROCEDURE [dbo].[GetEmpresa]
(
	@Id int
)
AS
BEGIN

	SET NOCOUNT ON;

	IF @Id = 0
	BEGIN
		SELECT * FROM Empresa
	END
	ELSE
	BEGIN
		SELECT * FROM Empresa WHERE IdEmpresa = @Id
	END


END