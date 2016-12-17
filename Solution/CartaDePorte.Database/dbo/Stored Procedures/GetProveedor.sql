CREATE PROCEDURE [dbo].[GetProveedor]
(
	@Id int,
	@IdEmpresa int
)
AS
BEGIN

	SET NOCOUNT ON;

	IF @Id = 0
	BEGIN
		SELECT * 
		FROM 
			Proveedor 
		WHERE 
			IdSapOrganizacionDeVenta in (select e.IdSapOrganizacionDeVenta from Empresa e where e.IdEmpresa = @IdEmpresa)
	END
	ELSE
	BEGIN
		SELECT * 
		FROM 
			Proveedor 
		WHERE 
			IdSapOrganizacionDeVenta in (select e.IdSapOrganizacionDeVenta from Empresa e where e.IdEmpresa = @IdEmpresa)
			and IdProveedor = @Id
	END


END