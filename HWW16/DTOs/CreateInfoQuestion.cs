using HWW16.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HWW16.DTOs
{
    public class CreateInfoQuestion
    {
        public string Text { get; set; }
        public List<Option> Options { get; set; }
    }
}
