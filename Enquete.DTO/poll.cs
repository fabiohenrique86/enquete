using System.Collections.Generic;

namespace Enquete.DTO
{
    public class poll
    {
        public poll()
        {
            options = new List<optionDTO>();
        }

        public long poll_id { get; set; }
        public string poll_description { get; set; }
        public short views { get; set; }

        public List<optionDTO> options { get; set; }
    }
}
