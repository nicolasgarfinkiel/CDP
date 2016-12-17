CREATE PROCEDURE [dbo].[GetCliente]
(
	@Id int,
	@IdEmpresa int
)
AS
BEGIN

	SET NOCOUNT ON;

	IF @Id = 0
	BEGIN


		if (@IdEmpresa = 0)
		begin

			SELECT * 
			FROM Cliente 
			where IdCliente not in (2000151,3000352)

		end
		else
		begin

			SELECT * 
			FROM 
				Cliente 
			where 
				IdSapOrganizacionDeVenta in (select e.IdSapOrganizacionDeVenta from Empresa e where e.IdEmpresa = @IdEmpresa)
				and IdCliente not in (2000151,3000352)

		end			
	END
	ELSE
	BEGIN
		SELECT * FROM Cliente WHERE IdCliente = @Id
	END


END