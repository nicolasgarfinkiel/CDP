CREATE PROCEDURE [dbo].[GetClienteByCuit]
(
	@texto nvarchar(500),
	@IdEmpresa int
)
AS
BEGIN

	SET NOCOUNT ON;

	select * from cliente 
	where 
		IdSapOrganizacionDeVenta in (select e.IdSapOrganizacionDeVenta from Empresa e where e.IdEmpresa = @IdEmpresa)
		and cuit = @texto 

END

