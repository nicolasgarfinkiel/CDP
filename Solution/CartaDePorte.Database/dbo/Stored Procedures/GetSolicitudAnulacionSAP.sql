CREATE PROCEDURE [dbo].[GetSolicitudAnulacionSAP]
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
			Solicitudes  s
		where 
			s.IdEmpresa = @IdEmpresa
			and s.Ctg <> '' 
			and s.EstadoEnSAP = 5 
			and s.CodigoRespuestaEnvioSAP <> ''

	END
	ELSE BEGIN

		SELECT * 
		FROM 
			Solicitudes  s
		where 
			s.IdEmpresa = @IdEmpresa
			and s.EstadoEnSAP = 5 
			and s.CodigoRespuestaEnvioSAP <> ''

	END	

END

