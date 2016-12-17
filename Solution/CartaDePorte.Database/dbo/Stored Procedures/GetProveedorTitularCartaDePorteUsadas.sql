CREATE PROCEDURE [dbo].[GetProveedorTitularCartaDePorteUsadas]
(
	@IdEmpresa int
)
AS
BEGIN
	select	Proveedor.* 
	from 	(select top 4 idProveedorTitularCartaDePorte,count(*) cnt from solicitudes where IdEmpresa = @IdEmpresa group by idProveedorTitularCartaDePorte order by count(*) desc) TitularCartaDePorte,
			Proveedor
	where	
		IdSapOrganizacionDeVenta in (select e.IdSapOrganizacionDeVenta from Empresa e where e.IdEmpresa = @IdEmpresa)
		and TitularCartaDePorte.idProveedorTitularCartaDePorte = Proveedor.IdProveedor

END