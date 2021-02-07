#region header

// MongrelWiki - ErrorViewModel.cs
// 
// Created by: Alistair J R Young (avatar) at 2021/02/07 2:14 PM.

#endregion

namespace ArkaneSystems.MongrelWiki.Models
{
    public class ErrorViewModel
    {
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty (value: this.RequestId);
    }
}
