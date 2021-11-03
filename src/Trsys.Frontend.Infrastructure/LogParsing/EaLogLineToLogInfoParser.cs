using System;
using System.Text.RegularExpressions;

namespace Trsys.Frontend.Infrastructure.LogParsing
{
    public class EaLogLineToLogInfoParser
    {
        private readonly Regex regex;
        private readonly Func<EaLogContext, EaLogLine, Match, ILogInfo> eventFactory;

        public EaLogLineToLogInfoParser(string pattern, Func<EaLogContext, EaLogLine, Match, ILogInfo> eventFactory)
        {
            regex = new Regex(pattern);
            this.eventFactory = eventFactory;
        }

        public bool TryConvert(EaLogContext context, EaLogLine line, out ILogInfo ev)
        {
            var match = regex.Match(line.Message);
            ev = match.Success ? eventFactory(context, line, match) : null;
            return match.Success;
        }
    }
}
