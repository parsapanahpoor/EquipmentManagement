
namespace EquipmentManagement.Application.Service;


using DinkToPdf;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading.Tasks;



public interface IPDFService
{
    public Task<byte[]> GeneratePdf(string path, string text);
}
public class PDFService : IPDFService
{
    public async Task<byte[]> GeneratePdf(string path, string text)
    {
        var context = new CustomAssemblyLoadContext();
        context.LoadUnmanagedLibrary(Path.Combine(Directory.GetCurrentDirectory(), "libs\\64bit\\libwkhtmltox.dll"));

        var converter = new SynchronizedConverter(new PdfTools());

        var doc = new HtmlToPdfDocument()
        {
            GlobalSettings = {
                PaperSize = PaperKind.A4
            },
            Objects = {
                new ObjectSettings() {
                    HtmlContent =text,
                    WebSettings = { DefaultEncoding = "utf-8" }
                }
            }
        };
        var pdf = converter.Convert(doc);
        return pdf;
    }



}
public class CustomAssemblyLoadContext : System.Runtime.Loader.AssemblyLoadContext
{
    public IntPtr LoadUnmanagedLibrary(string absolutePath)
    {
        return LoadUnmanagedDll(absolutePath);
    }
    protected override IntPtr LoadUnmanagedDll(string unmanagedDllPath)
    {
        return LoadUnmanagedDllFromPath(unmanagedDllPath);
    }
    protected override System.Reflection.Assembly Load(System.Reflection.AssemblyName assemblyName)
    {
        return null;
    }
}