CREATE PROCEDURE [dbo].[GetClienteDestinoUsadas]
(
	@IdEmpresa int
)
AS
BEGIN
	select	Cliente.* 
	from 	(select top 4 IdClienteDestino,count(*) cnt from solicitudes where IdEmpresa = @IdEmpresa group by IdClienteDestino order by count(*) desc) tabla,
			Cliente
	where	
		IdSapOrganizacionDeVenta in (select e.IdSapOrganizacionDeVenta from Empresa e where e.IdEmpresa = @IdEmpresa)
		and tabla.IdClienteDestino = Cliente.IdCliente
		and	IdCliente not in (2000151,3000352)
--select * from solicitudes

END