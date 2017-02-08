using System.ComponentModel.DataAnnotations;

namespace Clubber.Backend.Models.ParameterModels
{
    public class NodeData
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "The begin node id is required")]
        public string idNodeBegin { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "The end node id is required")]
        public string idNodeEnd { get; set; }
    }
}
