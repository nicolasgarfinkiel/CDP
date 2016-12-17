CREATE PROCEDURE [dbo].[GetSolicitudRecibida]
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
			SolicitudesRecibidas 
		WHERE
			IdEmpresa = @IdEmpresa
		ORDER BY 1 desc

	END
	ELSE
	BEGIN

		SELECT * FROM SolicitudesRecibidas WHERE IdSolicitudRecibida = @Id

	END
	
END

