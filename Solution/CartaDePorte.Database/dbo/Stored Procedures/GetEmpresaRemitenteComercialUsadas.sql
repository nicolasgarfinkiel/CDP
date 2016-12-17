--CREATE PROCEDURE [dbo].[GetEmpresaRemitenteComercialUsadas]
--AS
--BEGIN
--	select	Empresa.* 
--	from 	(select top 4 IdEmpresaRemitenteComercial,count(*) cnt from solicitudes group by IdEmpresaRemitenteComercial order by count(*) desc) tabla,
--			Empresa
--	where	tabla.IdEmpresaRemitenteComercial = Empresa.IdEmpresa

----select * from solicitudes 

--END