using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TestAsyn
{
    public class WasteTimeObject
    {
        public async Task<string> GetSlowString(int begin, int length, IProgress<int> progress, CancellationToken cancel)
        {
            StringBuilder sb = new StringBuilder();

            for (int i = begin; i < begin + length; i++)
            {
                sb.Append(WasteTime(i) + " ");

                cancel.ThrowIfCancellationRequested();

                if (progress != null)
                    progress.Report((int)((double)(i - begin + 1) * 100 / length));
            }

            return sb.ToString();
        }


        private string WasteTime(int current)
        {
            System.Threading.Thread.Sleep(1000);
            return current.ToString();
        }
    }

}
