CREATE PROCEDURE [dbo].[GetProveedorTransportistaUsadas]
(
	@IdEmpresa int
)
AS
BEGIN
	select	Proveedor.* 
	from 	(select top 4 IdProveedorTransportista,count(*) cnt from solicitudes where IdEmpresa = @IdEmpresa group by IdProveedorTransportista order by count(*) desc) tabla,
			Proveedor
	where	
		IdSapOrganizacionDeVenta in (select e.IdSapOrganizacionDeVenta from Empresa e where e.IdEmpresa = @IdEmpresa)
		and tabla.IdProveedorTransportista = Proveedor.IdProveedor

--select * from solicitudes

END