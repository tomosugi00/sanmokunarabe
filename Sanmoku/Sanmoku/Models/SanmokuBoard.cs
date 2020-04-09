using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sanmoku.Models
{
    /// <summary>
    /// 三目並べ用ボード
    /// </summary>
    public class SanmokuBoard
    {
        private readonly int Size = 3;
        private readonly List<List<SanmokuState>> board;

        public SanmokuBoard()
        {
            this.board = new List<List<SanmokuState>>();
            for (var row = 0; row < Size; row++)
            {
                var list = new List<SanmokuState>();
                for (var column = 0; column < Size; column++)
                {
                    list.Add(SanmokuState.Empty);
                }
                this.board.Add(list);
            }
        }

        /// <summary>
        /// マスの状態を取得する
        /// </summary>
        /// <param name="square">マスの座標(行,列)</param>
        /// <returns></returns>
        public SanmokuState GetAt((int row, int culumn) square)
        {
            //範囲判定
            if (square.row < 0 || square.row > Size - 1 || square.culumn < 0 || square.culumn > Size - 1)
            {
                throw new ArgumentOutOfRangeException(nameof(square));
            }
            return this.board[square.row][square.culumn];
        }

        /// <summary>
        /// マスの状態を変更する
        /// </summary>
        /// <param name="square">マスの座標(行,列)</param>
        /// <returns></returns>
        public SanmokuState ChangeAt((int row, int culumn) square)
        {
            var state = this.GetAt((square.row, square.culumn));
            switch (state)
            {
                case SanmokuState.Empty:
                    this.board[square.row][square.culumn] = SanmokuState.Maru;
                    return this.GetAt((square.row, square.culumn));
                case SanmokuState.Maru:
                    this.board[square.row][square.culumn] = SanmokuState.Batsu;
                    return this.GetAt((square.row, square.culumn));
                case SanmokuState.Batsu:
                    this.board[square.row][square.culumn] = SanmokuState.Empty;
                    return this.GetAt((square.row, square.culumn));
                default:
                    throw new NotImplementedException();
            }
        }

        /// <summary>
        /// ゲーム終了か
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        public bool IsFinish(out string result)
        {
            //縦
            if (IsFinishedByVertical(SanmokuState.Maru))
            {
                result = SanmokuState.Maru.ToString();
                return true;
            }
            else if (IsFinishedByVertical(SanmokuState.Batsu))
            {
                result = SanmokuState.Batsu.ToString();
                return true;
            }

            //横
            if (IsFinishedByHorizontal(SanmokuState.Maru))
            {
                result = SanmokuState.Maru.ToString();
                return true;
            }
            else if (IsFinishedByHorizontal(SanmokuState.Batsu))
            {
                result = SanmokuState.Batsu.ToString();
                return true;
            }

            //斜め
            if (IsFinishedByDiagonal(SanmokuState.Maru))
            {
                result = SanmokuState.Maru.ToString();
                return true;
            }
            else if (IsFinishedByDiagonal(SanmokuState.Batsu))
            {
                result = SanmokuState.Batsu.ToString();
                return true;
            }

            //引き分け
            if (this.IsDraw())
            {
                result = "draw";
                return true;
            }

            result = null;
            return false;
        }

        /// <summary>
        /// 縦が揃ったか
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        private bool IsFinishedByVertical(SanmokuState target)
        {
            for (var culumn = 0; culumn < Size; culumn++)
            {
                if(this.board[0][culumn] == target && this.board[1][culumn] == target && this.board[2][culumn] == target)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 横が揃ったか
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        private bool IsFinishedByHorizontal(SanmokuState target)
        {
            for (var row = 0; row < Size; row++)
            {
                if (this.board[row][0] == target && this.board[row][1] == target && this.board[row][2] == target)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 斜めが揃ったか
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        private bool IsFinishedByDiagonal(SanmokuState target)
        {
            if (this.board[0][0] == target && this.board[1][1] == target && this.board[2][2] == target)
            {
                return true;
            }
            else if (this.board[2][0] == target && this.board[1][1] == target && this.board[0][2] == target)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 引き分けか
        /// </summary>
        /// <returns></returns>
        private bool IsDraw()
        {
            for (var row = 0; row < Size; row++)
            {
                for (var column = 0; column < Size; column++)
                {
                    if (this.board[row][column] == SanmokuState.Empty)
                    {
                        return false;
                    }
                }
            }
            return true;
        }


    }
}
