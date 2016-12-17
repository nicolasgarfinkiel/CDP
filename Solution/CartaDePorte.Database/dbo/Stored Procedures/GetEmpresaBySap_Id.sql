CREATE PROCEDURE [dbo].[GetEmpresaBySap_Id]
(
	@Id varchar(50)
)
AS
BEGIN

	SET NOCOUNT ON;

	IF CONVERT(numeric(18), @Id) = 0
	BEGIN
		SELECT * FROM Empresa
	END
	ELSE
	BEGIN
		SELECT * FROM Empresa WHERE Sap_Id = @Id
	END


END