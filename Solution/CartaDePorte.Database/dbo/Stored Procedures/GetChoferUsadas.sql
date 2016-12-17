CREATE PROCEDURE [dbo].[GetChoferUsadas]
(
	@IdGrupoEmpresa int
)
AS
BEGIN
	select	Chofer.* 
	from 	(select top 4 IdChofer,count(*) cnt 
				from solicitudes 
				where IdEmpresa in (select e.IdEmpresa from Empresa e where e.IdGrupoEmpresa = @IdGrupoEmpresa)
			 group by IdChofer order by count(*) desc) tabla,
			Chofer
	where	tabla.IdChofer = Chofer.IdChofer
			and Chofer.IdGrupoEmpresa = @IdGrupoEmpresa

--select * from solicitudes

END