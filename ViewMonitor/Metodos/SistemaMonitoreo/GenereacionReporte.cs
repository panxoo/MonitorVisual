using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System.Collections.Generic;
using ViewMonitor.Data;
using ViewMonitor.Models;

namespace ViewMonitor.Metodos.SistemaMonitoreo
{
    public class GenereacionReporte
    {
        private ApplicationDbContext _context;

        public GenereacionReporte(ApplicationDbContext context)
        {
            _context = context;
        }

        public IWorkbook GeneracionExcelHistoricoEstado(List<ViewHistEstadoMonitor> _model)
        {
            IWorkbook workbook;
            workbook = new XSSFWorkbook();
            ISheet excelSheet = workbook.CreateSheet("Historico Estado");

            IDataFormat newDataFormat = workbook.CreateDataFormat();

            ICellStyle styleFecha = workbook.CreateCellStyle();
            styleFecha.BorderBottom = BorderStyle.Thin;
            styleFecha.BorderLeft = BorderStyle.Thin;
            styleFecha.BorderRight = BorderStyle.Thin;
            styleFecha.DataFormat = newDataFormat.GetFormat("MM/dd/yyyy HH:mm:ss");

            ICellStyle styleCell = workbook.CreateCellStyle();
            styleCell.BorderBottom = BorderStyle.Thin;
            styleCell.BorderLeft = BorderStyle.Thin;
            styleCell.BorderRight = BorderStyle.Thin;

            ICellStyle styleTitulo = workbook.CreateCellStyle();
            styleTitulo.BorderBottom = BorderStyle.Thin;
            styleTitulo.BorderLeft = BorderStyle.Thin;
            styleTitulo.BorderRight = BorderStyle.Thin;
            styleTitulo.BorderTop = BorderStyle.Thin;
            styleTitulo.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.LightBlue.Index;
            styleTitulo.FillPattern = FillPattern.SolidForeground;

            IRow row = excelSheet.CreateRow(0);

            row.CreateCell(1).SetCellValue("Monitor");
            row.Cells[0].CellStyle = styleTitulo;

            row.CreateCell(2).SetCellValue("Fecha Error");
            row.Cells[1].CellStyle = styleTitulo;

            row.CreateCell(3).SetCellValue("Fecha Solución");
            row.Cells[2].CellStyle = styleTitulo;

            row.CreateCell(4).SetCellValue("Falso Positivo");
            row.Cells[3].CellStyle = styleTitulo;

            row.CreateCell(5).SetCellValue("Nota");
            row.Cells[4].CellStyle = styleTitulo;

            int currentRow = 1;

            foreach (ViewHistEstadoMonitor dt in _model)
            {
                row = excelSheet.CreateRow(currentRow);

                row.CreateCell(1).SetCellValue(dt.Nombre);
                row.Cells[0].CellStyle = styleCell;

                row.CreateCell(2).SetCellValue(dt.FechaError);
                row.Cells[1].CellStyle = styleFecha;

                row.CreateCell(3).SetCellValue(dt.FechaSolucion);
                row.Cells[2].CellStyle = styleFecha;

                row.CreateCell(4).SetCellValue(dt.FalsoPositivo);
                row.Cells[3].CellStyle = styleCell;

                row.CreateCell(5).SetCellValue(dt.Nota);
                row.Cells[4].CellStyle = styleCell;

                currentRow++;
            }

            excelSheet.AutoSizeColumn(1);
            excelSheet.AutoSizeColumn(2);
            excelSheet.AutoSizeColumn(3);
            excelSheet.AutoSizeColumn(4);
            excelSheet.AutoSizeColumn(5);

            return workbook;
        }
    }
}