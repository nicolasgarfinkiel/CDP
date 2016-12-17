CREATE PROCEDURE [dbo].[getIdClienteProspecto]
(
	@IdEmpresa int
)
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @id int

	select @id = Max(idCliente) + 1 
	from cliente 
	where 
		IdSapOrganizacionDeVenta in (select e.IdSapOrganizacionDeVenta from Empresa e where e.IdEmpresa = @IdEmpresa)
		and esprospecto = 1

	IF @id is null or @id  < 9300000 
	BEGIN
		select 9300000 AS ID
	END
	ELSE
	BEGIN
		select @id AS ID
	END

END