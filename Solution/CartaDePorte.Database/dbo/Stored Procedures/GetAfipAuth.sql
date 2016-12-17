CREATE PROCEDURE [dbo].[GetAfipAuth]
(
	@Id int	
)
AS
BEGIN

	SET NOCOUNT ON;

	SELECT * from AfipAuth WHERE IdAfipAuth = @Id
	
END
