--CREATE PROCEDURE [dbo].[GetEmpresaTitularCartaDePorteUsadas]
--AS
--BEGIN
--	select	Empresa.* 
--	from 	(select top 4 idEmpresaTitularCartaDePorte,count(*) cnt from solicitudes group by idEmpresaTitularCartaDePorte order by count(*) desc) TitularCartaDePorte,
--			Empresa
--	where	TitularCartaDePorte.idEmpresaTitularCartaDePorte = Empresa.IdEmpresa

--END