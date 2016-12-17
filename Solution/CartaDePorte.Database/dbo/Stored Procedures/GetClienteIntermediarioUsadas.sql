CREATE PROCEDURE [dbo].[GetClienteIntermediarioUsadas]
(
	@IdEmpresa int
)
AS
BEGIN
	select	Cliente.* 
	from 	(select top 4 IdClienteIntermediario,count(*) cnt from solicitudes where IdEmpresa = @IdEmpresa group by IdClienteIntermediario order by count(*) desc) tabla,
			Cliente
	where	
		IdSapOrganizacionDeVenta in (select e.IdSapOrganizacionDeVenta from Empresa e where e.IdEmpresa = @IdEmpresa)
		and tabla.IdClienteIntermediario = Cliente.IdCliente
		and	IdCliente not in (2000151,3000352)
--select * from solicitudes

END