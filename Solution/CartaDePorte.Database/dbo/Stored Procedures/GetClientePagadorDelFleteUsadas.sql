CREATE PROCEDURE [dbo].[GetClientePagadorDelFleteUsadas]
(
	@IdEmpresa int
)
AS
BEGIN
	select	Cliente.* 
	from 	(select top 4 IdClientePagadorDelFlete,count(*) cnt from solicitudes where IdEmpresa = @IdEmpresa group by IdClientePagadorDelFlete order by count(*) desc) tabla,
			Cliente
	where	
		IdSapOrganizacionDeVenta in (select e.IdSapOrganizacionDeVenta from Empresa e where e.IdEmpresa = @IdEmpresa)
		and tabla.IdClientePagadorDelFlete = Cliente.IdCliente
		and	IdCliente not in (2000151,3000352)

END
