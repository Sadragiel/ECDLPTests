using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace WeierstrassCurveTest.UI
{
    internal abstract class UITabManager
    {
        private UserInterface form;

        public UITabManager(UserInterface form)
        {
            this.form = form;
        }

        public abstract void RunTest();

        public abstract void SetResult(string message, bool isError = false);

        protected void SetError(Exception ex)
        {
            SetResult($"Error: {ex.Message}", true);
        }
    }
}
