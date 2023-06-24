using System.ComponentModel.DataAnnotations;

namespace first.View_Models
{
    public record BranchCreateItem
    {
        public string BranchName { get; set; }
        public string BranchAddress { get; set; }
    }
}