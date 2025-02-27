using JsonBuilder.Core.Models.Messages;
using JsonBuilder.Core.Models;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonBuilder.Core.Utilities
{
    public class ExcelImporter
    {
        public PickConfirmMessage ImportDataFromExcel(string filePath)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            var pickConfirmMessage = new PickConfirmMessage();
            pickConfirmMessage._params = new PickConfirmParams();

            pickConfirmMessage._params.DivertTime = DateTime.Now.ToString("yyyyMMddHHmmss");
            pickConfirmMessage._params.Weight = "6000";
            pickConfirmMessage._params.TransferId = "26" + DateTime.Now.ToString("HHmmss");
            pickConfirmMessage._params.Uuid = Guid.NewGuid().ToString();

            // 使用 EPPlus 打开 Excel 文件
            using (var package = new ExcelPackage(new FileInfo(filePath)))
            {
                var worksheet = package.Workbook.Worksheets[0]; // 获取第一个工作表
                int rowCount = worksheet.Dimension.Rows;

                for (int row = 2; row <= rowCount; row++) // 从第2行开始（跳过标题行）
                {
                    var binCode = worksheet.Cells[row, 1].Text;
                    var orderNo = worksheet.Cells[row, 2].Text;
                    var articleId = worksheet.Cells[row, 3].Text;
                    var hostLineId = worksheet.Cells[row, 4].Text;
                    var toPickQty = worksheet.Cells[row, 5].Text;
                    var pickedQty = worksheet.Cells[row, 6].Text;
                    var boxNo = worksheet.Cells[row, 7].Text;

                    if (pickConfirmMessage._params.BoxNumber == "")
                    {
                        pickConfirmMessage._params.BoxNumber = boxNo;
                    }
                    else if (pickConfirmMessage._params.BoxNumber != boxNo)
                    {
                        throw new Exception("Box number is not consistent");
                    }

                    if (pickConfirmMessage._params.CustomerCode == "")
                    {
                        pickConfirmMessage._params.CustomerCode = orderNo;
                    }
                    else if (pickConfirmMessage._params.CustomerCode != orderNo)
                    {
                        throw new Exception("Customer code is not consistent");
                    }


                    var lineResponseMessage = new LineResponseMessage
                    {
                        _params = new LineResponseParams
                        {
                            HostLineId = hostLineId,
                            ArticleId = articleId,
                            GeoCode = binCode,
                            OrderedPackunits = toPickQty,
                            PickedPackunits = pickedQty,

                        }
                    };

                    pickConfirmMessage.NestedMessages.Add(lineResponseMessage);
                }
            }

            return pickConfirmMessage;
        }
    }
}
