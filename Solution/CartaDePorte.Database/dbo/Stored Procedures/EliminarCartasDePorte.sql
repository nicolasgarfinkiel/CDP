CREATE PROCEDURE [dbo].[EliminarCartasDePorte]
(
	@IdLoteCartasDePorte   INT,
	@Usuario varchar(150)
)

AS
BEGIN

	SET NOCOUNT ON;
    
	
	DELETE CartasDePorte 
	WHERE IdLoteLoteCartasDePorte = @IdLoteCartasDePorte
    
	DELETE LoteCartasDePorte
	WHERE IdLoteCartasDePorte = @IdLoteCartasDePorte


	INSERT [LogOperaciones] ([Tabla],[Accion],[Id],[Fecha],[Usuario])
	VALUES ('CartasDePorte', 'DELETE', @IdLoteCartasDePorte, GETDATE(), @Usuario)

	INSERT [LogOperaciones] ([Tabla],[Accion],[Id],[Fecha],[Usuario])
	VALUES ('LoteCartasDePorte', 'DELETE', @IdLoteCartasDePorte, GETDATE(), @Usuario)

END