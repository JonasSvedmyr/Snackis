﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SnackisAPI.Models
{
    public class CreateCommentReportModel
    {
        public string CommentId { get; set; }
        public string Reason { get; set; }
    }
}
