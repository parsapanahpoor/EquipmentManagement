using EquipmentManagement.Application.CQRS.SiteSide.SelfService.Query.ReceiveFoodListReceipt;
using EquipmentManagement.Domain.DTO.SiteSide.Role;
using EquipmentManagement.Domain.Entities.Account;
using EquipmentManagement.Domain.IRepositories.Role;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace EquipmentManagement.Application.CQRS.SiteSide.Role.Query;

public record LoadTemplateEmployeeQueryHandler : IRequestHandler<LoadTemplateEmployeeQuery, string>
{


    public async Task<string> Handle(LoadTemplateEmployeeQuery request, CancellationToken cancellationToken)
    {



        var sb = new StringBuilder();

        sb.AppendLine(@"<!DOCTYPE html>
<html lang=""fa"" dir=""rtl"">
<head>
  <meta charset=""UTF-8"" />
  <title>لیست کارمندان</title>
  <style>
    @font-face {
      font-family: 'Vazirmatn';
      src: url('https://cdn.fontcdn.ir/Font/Persian/Vazirmatn/Vazirmatn-Regular.ttf') format('truetype');
      font-weight: normal;
      font-style: normal;
    }
    body {
      font-family: 'Vazirmatn', Tahoma, Arial, sans-serif;
      background-color: #ffffff;
      margin: 0;
      padding: 20px;
      direction: rtl;
    }
    h1 {
      text-align: center;
      font-size: 24px;
      font-weight: bold;
      color: #333;
      margin-bottom: 20px;
    }
    table {
      width: 100%;
      border-collapse: collapse;
      margin: 0 auto;
      font-size: 14px;
    }
    th, td {
      border: 1px solid #999;
      padding: 10px 12px;
      text-align: center;
      vertical-align: middle;
    }
    th {
      background-color: #f5f5f5;
      color: #333;
      font-weight: bold;
    }
    tr:hover {
      background-color: #fafafa;
    }
    @media print {
      body {
        padding: 0;
      }
      table {
        font-size: 12px;
      }
    }
  </style>
</head>
<body>

  <h1>لیست کارمندان</h1>

  <table>
    <thead>
      <tr>
        <th>نام</th>
        <th>نام خانوادگی</th>
        <th>کد پرسنلی</th>
        <th>موبایل</th>
      </tr>
    </thead>
    <tbody>");

        foreach (var emp in request.List)
        {
            sb.AppendLine($@"      <tr>
        <td>{(emp.FirstName ?? "—")}</td>
        <td>{(emp.LastName ?? "—")}</td>
        <td style=""font-family: monospace;"">{(emp.PersonnelCode ?? "—")}</td>
        <td style=""font-family: monospace; direction: ltr;"">{(emp.Mobile ?? "—")}</td>
      </tr>");
        }

        sb.AppendLine(@"    </tbody>
  </table>

</body>
</html>");

        return sb.ToString();

    }
}
