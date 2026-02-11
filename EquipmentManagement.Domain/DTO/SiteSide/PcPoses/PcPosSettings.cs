using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquipmentManagement.Domain.DTO.SiteSide.PcPoses;

public class PcPosSettings
{
    public string SignalRHubUrl { get; set; } = "http://localhost:8080/signalr";
    public int DefaultConnectionType { get; set; } = 1; // 0: COM, 1: Network
    public string DefaultIp { get; set; } = "192.168.1.100";
    public string DefaultPort { get; set; } = "5000";
    public string DefaultComPort { get; set; } = "COM4";
    public int DefaultAccountType { get; set; } = 0; // 0: Single, 1: Share
    public int DefaultLanguage { get; set; } = 0; // 0: Farsi, 1: English
    public string DefaultTerminalId { get; set; } = "T123456";
    public string DefaultPurchaseId { get; set; } = "P001";
    public string MinimumAmount { get; set; } = "1000";
    public List<PosDevice> PosDevices { get; set; } = new();
}

public class PosDevice
{
    public string Name { get; set; } = string.Empty;
    public string IpAddress { get; set; } = string.Empty;
    public string Port { get; set; } = "5000";
    public string ComPort { get; set; } = "COM4";
    public string TerminalId { get; set; } = string.Empty;
}
