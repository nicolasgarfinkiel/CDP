CREATE PROCEDURE [dbo].[getIdSapProveedorProspecto]
(
	@IdEmpresa int
	)
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @id numeric

	select @id = Max(CONVERT(numeric,ISNULL(Sap_Id,0))) + 1 
	from 
		Proveedor 
	where 
		IdSapOrganizacionDeVenta in (select e.IdSapOrganizacionDeVenta from Empresa e where e.IdEmpresa = @IdEmpresa)
		and	esprospecto = 1

	IF @id is null or @id < 9300000000 
	BEGIN
		select 9300000000 AS ID
	END
	ELSE
	BEGIN
		select @id AS ID
	END

END