CREATE PROCEDURE [dbo].[GetClienteDestinatarioUsadas]
(
	@IdEmpresa int
)
AS
BEGIN
	select	Cliente.* 
	from 	(select top 4 IdClienteDestinatario,count(*) cnt from solicitudes where IdEmpresa = @IdEmpresa group by IdClienteDestinatario order by count(*) desc) tabla,
			Cliente
	where	
		IdSapOrganizacionDeVenta in (select e.IdSapOrganizacionDeVenta from Empresa e where e.IdEmpresa = @IdEmpresa)
		and tabla.IdClienteDestinatario = Cliente.IdCliente
		and	IdCliente not in (2000151,3000352)

--select * from solicitudes

END