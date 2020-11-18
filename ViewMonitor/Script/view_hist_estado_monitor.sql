CREATE VIEW [dbo].[view_hist_estado_monitor]
AS
SELECT        he.Monitor_Estado_HistID, he.ProcesoId, m.MonitorID, m.Nombre, he.Fecha AS 'FechaError', ISNULL(hs.estado, 0) AS 'Solucion', ISNULL(hs.Fecha, '01/01/2000') AS 'FechaSolucion', he.FalsoPositivo, he.Nota
FROM            dbo.Monitor_Estado_Hists AS he INNER JOIN
                         dbo.Monitors AS m ON m.MonitorID = he.MonitorID LEFT OUTER JOIN
                         dbo.Monitor_Estado_Hists AS hs ON hs.MonitorID = he.MonitorID AND hs.ProcesoId = he.ProcesoId AND hs.estado = 1
WHERE        (he.estado = 0)