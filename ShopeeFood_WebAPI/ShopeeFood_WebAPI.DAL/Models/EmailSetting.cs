using System;
using System.Collections.Generic;

namespace ShopeeFood_WebAPI.DAL.Models;

public partial class EmailSetting
{
    public int EmailSettingId { get; set; }

    public string? SmtpServer { get; set; }

    public int? Port { get; set; }

    public string? SenderEmail { get; set; }

    public string? SenderName { get; set; }

    public string? UserName { get; set; }

    public string? Password { get; set; }

    public bool? EnableSsl { get; set; }
}
