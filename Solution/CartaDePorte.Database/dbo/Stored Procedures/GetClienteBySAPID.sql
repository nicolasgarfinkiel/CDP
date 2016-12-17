CREATE PROCEDURE [dbo].[GetClienteBySAPID]
(
	@Id nvarchar(50),
	@IdOrganizacionDeVenta nvarchar(50)
)
AS
BEGIN

	SET NOCOUNT ON;
	SELECT * 
	FROM 
		Cliente 
	WHERE 
		IdCliente = @Id
		AND IdSapOrganizacionDeVenta = @IdOrganizacionDeVenta


END