CREATE PROCEDURE [dbo].[GetEstablecimientoOrigenDestino]
(
	@Origen int,
	@IdEmpresa int
)
AS
BEGIN

	SET NOCOUNT ON;
	-- para Origen
	IF @Origen = 1
	BEGIN
		SELECT * FROM Establecimiento where IdEmpresa = @IdEmpresa and RecorridoEstablecimiento in (0,2)
	END
	ELSE
	BEGIN
	-- para Destino
		SELECT * FROM Establecimiento where IdEmpresa = @IdEmpresa and RecorridoEstablecimiento in (1,2)
	END
	
END
