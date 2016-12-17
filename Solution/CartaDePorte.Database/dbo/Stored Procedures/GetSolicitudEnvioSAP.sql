CREATE PROCEDURE [dbo].[GetSolicitudEnvioSAP]
(
	@IdEmpresa int
)
AS
BEGIN

	SET NOCOUNT ON;

	IF EXISTS (SELECT * FROM Empresa e WHERE e.IdEmpresa = @IdEmpresa AND e.IdGrupoEmpresa = [dbo].[ID_GRUPO_CRESUD]())	BEGIN

		--CRESUD
		SELECT * 
		FROM 
			Solicitudes s
		WHERE
			s.IdEmpresa = @IdEmpresa
			and s.Ctg <> '' 
			and s.EstadoEnAFIP <> 3 
			and s.EstadoEnSAP in (0,8)

	END
	ELSE BEGIN

		SELECT * 
		FROM 
			Solicitudes s
		WHERE
			s.IdEmpresa = @IdEmpresa
			and s.EstadoEnSAP in (0,8)

	END	
	
END
