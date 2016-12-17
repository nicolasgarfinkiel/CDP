CREATE PROCEDURE [dbo].[GetMisReservas] 
(
	@UsuarioReserva nvarchar(100),
	@IdGrupoEmpresa int,
	@IdEmpresa int
)
AS
BEGIN

	SET NOCOUNT ON;
	
	IF @UsuarioReserva = ''
	BEGIN
		select	s.* 
		from	CartasDePorte c, solicitudes s	
		where	c.UsuarioReserva is not null
		and		s.ctg is null
		and		c.NumeroCartaDePorte = s.NumeroCartaDePorte
		and		s.IdEmpresa = @IdEmpresa
		and		c.IdGrupoEmpresa = @IdGrupoEmpresa
	END
	ELSE
	BEGIN
		select	s.* 
		from	CartasDePorte c, solicitudes s	
		where	c.UsuarioReserva = @UsuarioReserva 
		and		s.ctg is null
		and		c.NumeroCartaDePorte = s.NumeroCartaDePorte			
		and		s.IdEmpresa = @IdEmpresa
		and		c.IdGrupoEmpresa = @IdGrupoEmpresa
	END




END
