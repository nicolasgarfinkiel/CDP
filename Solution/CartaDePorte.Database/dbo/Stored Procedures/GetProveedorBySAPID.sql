CREATE PROCEDURE [dbo].[GetProveedorBySAPID]
(
	@Id nvarchar(50),
	@IdOrganizacionDeVenta nvarchar(50)
)
AS
BEGIN

	SET NOCOUNT ON;
	SELECT * 
	FROM 
		Proveedor 
	WHERE 
		Sap_Id = @Id
		AND IdSapOrganizacionDeVenta = @IdOrganizacionDeVenta


END