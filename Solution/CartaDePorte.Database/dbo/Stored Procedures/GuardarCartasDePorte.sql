CREATE PROCEDURE [dbo].[GuardarCartasDePorte]
(
      @Desde varchar(50),
      @Hasta varchar(50),
      @NroCEE varchar(50),
      @FechaVencimiento datetime,
      @Usuario nvarchar(50),
      @EstablecimientoOrigen varchar(50) = null,
	  @IdGrupoEmpresa int
)

AS
BEGIN
      SET NOCOUNT ON;


	  if (@EstablecimientoOrigen <= 0)
	  BEGIN
		SET @EstablecimientoOrigen = null;
	  END


      DECLARE @loteID int
      INSERT INTO LoteCartasDePorte 
		(Desde, Hasta, Cee, FechaVencimiento,EstablecimientoOrigen,UsuarioCreacion, IdGrupoEmpresa)  
	  VALUES
		(@Desde, @Hasta, @NroCEE, @FechaVencimiento,@EstablecimientoOrigen,@Usuario, @IdGrupoEmpresa)

      SELECT @loteID = SCOPE_IDENTITY()

      
      DECLARE @desdeINT int
      DECLARE @hastaINT int
      DECLARE @cnt int

      SET @desdeINT = CONVERT(int,@Desde)
      SET @hastaINT = CONVERT(int,@Hasta)
      SET @cnt = 0

      WHILE @desdeINT <= @hastaINT
      BEGIN
        IF NOT EXISTS
        (
                  SELECT NumeroCartaDePorte
                  FROM CartasDePorte
                  WHERE NumeroCartaDePorte = CONVERT(VARCHAR,@desdeINT)
				  and IdGrupoEmpresa = @IdGrupoEmpresa
        )
        BEGIN
                  INSERT INTO CartasDePorte 
					(NumeroCartaDePorte,NumeroCee,Estado,IdLoteLoteCartasDePorte, IdGrupoEmpresa) 
				  VALUES
					(CONVERT(VARCHAR,@desdeINT),@NroCEE,0,@loteID, @IdGrupoEmpresa)

                  SET @cnt = @cnt + 1
        END

            SET @desdeINT = @desdeINT + 1  

      END

      select @cnt

END

