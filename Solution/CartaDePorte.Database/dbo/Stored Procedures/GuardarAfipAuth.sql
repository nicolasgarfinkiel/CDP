CREATE PROCEDURE [dbo].[GuardarAfipAuth]
(
	@Token nvarchar(1000),
	@Sign nvarchar(1000),
	@GenerationTime datetime,
	@ExpirationTime datetime,
	@Service nvarchar(100),
	@UniqueID nvarchar(100)
)
AS
BEGIN

	SET NOCOUNT ON;

	UPDATE	AfipAuth
	SET		Token = @Token,
			[Sign] = @Sign,
			GenerationTime = @GenerationTime,
			ExpirationTime = @ExpirationTime,
			[Service] = @Service,
			UniqueID = @UniqueID
	WHERE	IdAfipAuth = 1


END
