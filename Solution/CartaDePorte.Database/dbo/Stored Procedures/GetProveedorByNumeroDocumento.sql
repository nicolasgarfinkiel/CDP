CREATE PROCEDURE [dbo].[GetProveedorByNumeroDocumento]
(
	@texto nvarchar(500),
	@IdEmpresa int
)
AS
BEGIN

	SET NOCOUNT ON;

	select * from Proveedor 
	where 
		IdSapOrganizacionDeVenta in (select e.IdSapOrganizacionDeVenta from Empresa e where e.IdEmpresa = @IdEmpresa)
		and NumeroDocumento = @texto 

END
