using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotusInterview.Domain.Entities;
public class Colour
{
    public ColourEnum ColourId { get; set; }
    public required string ColourName { get; set; }
    public required string ColourHex { get; set; }
}
