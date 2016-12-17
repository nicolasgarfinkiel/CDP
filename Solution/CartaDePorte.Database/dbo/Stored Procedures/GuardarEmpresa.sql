CREATE PROCEDURE [dbo].[GuardarEmpresa]
(
	@IdEmpresa int,
	@IdCliente int, 
	@Descripcion nvarchar(400),
	@IdSapOrganizacionDeVenta nvarchar(100),
	@IdSapSector nvarchar(100),
	@IdSapCanalLocal nvarchar(100),
	@IdSapCanalExpor nvarchar(100)
)
AS
BEGIN
	SET NOCOUNT ON;

	IF @IdEmpresa > 0
	BEGIN

		UPDATE	Empresa 
		SET		IdCliente = @IdCliente,
				Descripcion = @Descripcion,
				IdSapOrganizacionDeVenta = @IdSapOrganizacionDeVenta,
				IdSapSector = @IdSapSector,
				IdSapCanalLocal = @IdSapCanalLocal,
				IdSapCanalExpor = @IdSapCanalExpor
		WHERE	IdEmpresa = @IdEmpresa


	END
	ELSE
	BEGIN

		INSERT INTO Empresa (IdCliente,Descripcion,IdSapOrganizacionDeVenta,IdSapSector,IdSapCanalLocal,IdSapCanalExpor) VALUES 
		(@IdCliente,@Descripcion,@IdSapOrganizacionDeVenta,@IdSapSector,@IdSapCanalLocal,@IdSapCanalExpor)

	END

END

