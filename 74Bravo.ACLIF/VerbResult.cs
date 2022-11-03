using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACLIF
{
    public class VerbResult : ICliVerbResult
    {
        public VerbResult(bool commandHandled, string? message, int resultCode)
        {
            CommandHandled = commandHandled;
            Message = message;
            ResultCode = resultCode;
        }

        public bool CommandHandled { get; private set; } = false;

        public string? Message { get; private set; }

        public int ResultCode { get; private set; } = 0;

        public static VerbResult Success(string message = "Operation Completed Successfully")
        {
            return new VerbResult(true, message, 0);
        }

           public static VerbResult NoAction(string message = "No action was taken.  This is likely due to the command being incomplete.")
        {
            return new VerbResult(true, message, 1);
        }

        public static VerbResult Exception(Exception ex)
        {
            return new VerbResult(true, ex.Message, 2);
        }

    }
}
