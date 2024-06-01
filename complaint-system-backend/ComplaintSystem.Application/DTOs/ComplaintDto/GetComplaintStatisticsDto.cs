using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplaintSystem.Application.DTOs.ComplaintDto;
public class GetComplaintStatisticsDto
{
    public int TotalComplaints { get; set; }
    public int ResolvedComplaints { get; set; }
    public int PendingComplaints { get; set; }
    public int RejectedComplaints { get; set; }

}
