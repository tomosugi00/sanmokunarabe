using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sanmoku.Models
{
    /// <summary>
    /// マスの状態
    /// </summary>
    public enum SanmokuState
    {
        /// <summary>
        /// カラの状態
        /// </summary>
        Empty,

        /// <summary>
        /// "○"の状態
        /// </summary>
        Maru,

        /// <summary>
        /// "×"の状態
        /// </summary>
        Batsu,
    }
}
