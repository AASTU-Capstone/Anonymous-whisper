using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace  ComplaintSystem.Infrastructure.Authentication;
public class Jwtsettings
{
    public const string SectionName = "jwtsettings";
    public string Secret {  get; set; }
    public string Issuer {  get; set; }
    public string Audience {  get; set; }
    public int ExpiryMinutes{  get; set; }
}
