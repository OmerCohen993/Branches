using System.ComponentModel.DataAnnotations;

namespace first.View_Models
{
    public record BranchUpdateItem
    {
        public string? BranchName { get; set; }
        public string? BranchAddress { get; set; }
    }
}