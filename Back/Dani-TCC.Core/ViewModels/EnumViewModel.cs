using System;

namespace Dani_TCC.Core.ViewModels
{
    public class EnumViewModel
    {
        public EnumViewModel(int id, string description)
        {
            Id = id;
            Description = description ?? throw new ArgumentNullException(nameof(description));
        }

        public int Id { get; set; }
        public String Description { get; set; }
    }
}