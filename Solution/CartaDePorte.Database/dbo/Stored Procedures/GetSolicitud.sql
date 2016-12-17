CREATE PROCEDURE [dbo].[GetSolicitud]
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
			Solicitudes 
		WHERE
			IdEmpresa = @IdEmpresa
		ORDER BY 1 desc
	END
	ELSE
	BEGIN
		SELECT * 
		FROM 
			Solicitudes 
		WHERE 
			IdSolicitud = @Id
	END
	
END
