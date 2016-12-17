CREATE PROCEDURE [dbo].[EliminarCartasDePorteDisponibles]
(
	@IdLoteCartasDePorte   INT,
	@Usuario varchar(150)
)

AS
BEGIN

	SET NOCOUNT ON;
    

	DECLARE @UTILIZADO_HASTA INT;


	--busca ultimo utilizado
	SELECT 
		@UTILIZADO_HASTA = MAX(NumeroCartaDePorte) 
	FROM 
		CartasDePorte
	WHERE 
		IdLoteLoteCartasDePorte = @IdLoteCartasDePorte
		AND Estado = 1;

	
	if (@UTILIZADO_HASTA > 0)
	BEGIN

		--elimina no utilizado
		DELETE CartasDePorte 
		WHERE 
			IdLoteLoteCartasDePorte = @IdLoteCartasDePorte
			AND Estado = 0;


		--actualiza hasta del lote
		UPDATE LoteCartasDePorte
		SET Hasta = @UTILIZADO_HASTA
		WHERE IdLoteCartasDePorte = @IdLoteCartasDePorte


		INSERT [LogOperaciones] ([Tabla],[Accion],[Id],[Fecha],[Usuario])
		VALUES ('CartasDePorte', 'DELETE DISP', @IdLoteCartasDePorte, GETDATE(), @Usuario)

		INSERT [LogOperaciones] ([Tabla],[Accion],[Id],[Fecha],[Usuario])
		VALUES ('LoteCartasDePorte', 'DELETE DISP', @IdLoteCartasDePorte, GETDATE(), @Usuario)

	END	ELSE
	BEGIN

		--NO SE UTILIZO EL RANGO => ELIMINA EL LOTE COMPLETO		
		DELETE CartasDePorte 
		WHERE 
			IdLoteLoteCartasDePorte = @IdLoteCartasDePorte
			AND Estado = 0;

		DELETE LoteCartasDePorte
		WHERE 
			IdLoteCartasDePorte = @IdLoteCartasDePorte

	END
END